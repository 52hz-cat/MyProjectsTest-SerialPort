using System.Text;

namespace MyProjectsTest
{
    public partial class Form1 : Form
    {
        private AppSettings _settings;

        // 用 SerialPortHelper 替代原来的 SerialPort
        private SerialPortHelper _serial = new();

        public Form1()
        {
            InitializeComponent();
            // 程序启动时，检查日志是否超过 5MB
            Logger.CheckAndRotate();
            // 订阅数据到达事件 会自动调用OnSerialDataReceived
            _serial.DataReceived += OnSerialDataReceived;
            // 自动扫描电脑上所有可用串口，填充到下拉框
            String[] ports = System.IO.Ports.SerialPort.GetPortNames();
            comboBoxPorts.Items.Clear();
            comboBoxPorts.Items.AddRange(ports);
            if (ports.Length > 0)
            {
                comboBoxPorts.SelectedIndex = 0;
            }
            //编码默认选择
            comboBoxEncoding.SelectedIndex = 0;
            //从config.json读取
            _settings = ConfigHelper.Load();
            //判断是否启用配置文件
            if (_settings.EnableConfig)
            {
                // 【启用配置】恢复上次的状态
                // 端口：只有下拉框里存在这个端口，才选中它
                if (comboBoxPorts.Items.Contains(_settings.PortName))
                {
                    comboBoxPorts.SelectedItem = _settings.PortName;
                }
                else if (comboBoxPorts.Items.Count > 0)
                {
                    comboBoxPorts.SelectedIndex = 0;
                }
                //编码
                comboBoxEncoding.SelectedItem = _settings.Encoding;
                //十六进制
                chkHexSend.Checked = _settings.HexSend;
                chkHexDisplay.Checked = _settings.HexDisplay;
                comboBoxSendCheck.SelectedItem = _settings.SendCheck;
                comboBoxRecvCheck.SelectedItem = _settings.RecvCheck;
            }
            else
            {
                // 【不启用配置】用默认值（选中第一项）
                if (comboBoxPorts.Items.Count > 0)
                {
                    comboBoxPorts.SelectedIndex = 0;
                    comboBoxEncoding.SelectedIndex = 0;
                    comboBoxSendCheck.SelectedIndex = 0;
                    comboBoxRecvCheck.SelectedIndex = 0;
                }
            }
            //同步"启用配置"复选框的状态
            chkEnableConfig.Checked = _settings.EnableConfig;
            // 用户手动勾选/取消时，更新到配置对象（关闭时会保存）
            chkEnableConfig.CheckedChanged += (s, e) =>
            {
                _settings.EnableConfig = chkEnableConfig.Checked;
            };
        }

        // 串口收到数据时触发，只负责显示
        private void OnSerialDataReceived(byte[] data)
        {
            string receivedData;

            // 十六进制显示
            if (chkHexDisplay.Checked)
            {
                receivedData = BitConverter.ToString(data).Replace("-", " ").Replace(",", " ");

                // 接收校验
                string checkMethod = comboBoxRecvCheck.Text;

                if (checkMethod != "None")
                {
                    // 校验字节长度：CRC16占2字节，其他占1字节
                    int checkLen = (checkMethod == "CRC16") ? 2 : 1;
                    if (data.Length > checkLen)
                    {
                        // 分离数据部分和校验部分
                        byte[] body = new byte[data.Length - checkLen];
                        Array.Copy(data, body, body.Length);
                        byte[] recvCheck = new byte[checkLen];
                        Array.Copy(data, body.Length, recvCheck, 0, checkLen);
                        // 计算实际校验值
                        byte[] calcCheck = ChecksumHelper.Compute(checkMethod, body);
                        //比对
                        bool ok = true;
                        for (int i = 0; i < checkLen; i++)
                        {
                            if (recvCheck[i] != calcCheck[i]) { ok = false; break; }
                        }
                        string tip = ok ? "校验正确" : "校验错误";
                        receivedData += $"[{checkMethod}{tip}]";
                    }
                    else
                    {
                        receivedData += "[数据太短无法校验]";
                    }
                }
            }

            // 文本显示
            else
            {
                try
                {
                    Encoding encoding = Encoding.GetEncoding(comboBoxEncoding.Text);
                    receivedData = encoding.GetString(data);
                }
                catch
                {
                    receivedData = Encoding.GetEncoding("GBK").GetString(data);
                }
            }
            // 跨线程更新 UI
            this.BeginInvoke(new Action(() =>
            {
                textBox1.AppendText(receivedData + Environment.NewLine);
                //滚动到光标所在的位置。
                textBox1.ScrollToCaret();
            }));
        }

        // 打开/关闭串口
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (!_serial.IsOpen)
            {
                try
                {
                    _serial.Open(comboBoxPorts.Text, 9600);
                    labelStatus.Text = "串口打开成功";
                    btnOpen.Text = "关闭串口";
                    Logger.Write($"串口打开成功:{comboBoxPorts.Text},波特率:9600");
                }
                catch (Exception ex)
                {
                    labelStatus.Text = "打开失败：" + ex.Message;
                    Logger.Write($"串口打开失败+{ex.Message}");
                }
            }
            else
            {
                _serial.Close();
                labelStatus.Text = "串口已关闭";
                btnOpen.Text = "打开串口";
            }
        }

        // 发送
        private void BtnSend_Click(object sender, EventArgs e)
        {
            if (!_serial.IsOpen)
            {
                MessageBox.Show("请先打开串口");
                return;
            }

            // 十六进制发送
            if (chkHexSend.Checked)
            {
                try
                {
                    byte[] data = HexStringToBytes(textBox2.Text);
                    // 按选中的校验方式追加校验字节
                    byte[] check = ChecksumHelper.Compute(comboBoxSendCheck.Text, data);
                    byte[] full = new byte[data.Length + check.Length];
                    Array.Copy(data, 0, full, 0, data.Length);
                    Array.Copy(check, 0, full, data.Length, check.Length);
                    _serial.Send(full);
                    Logger.Write($"发送{BitConverter.ToString(full).Replace("-", " ")}");
                }
                catch
                {
                    MessageBox.Show("十六进制格式错误！示例：01 03 00 00");
                    return;
                }
            }
            // 文本发送
            else
            {
                _serial.Send(textBox2.Text);
                Logger.Write($"发送{textBox2.Text}");
            }

            textBox2.Clear();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            String[] ports = System.IO.Ports.SerialPort.GetPortNames();
            comboBoxPorts.Items.Clear();
            comboBoxPorts.Items.AddRange(ports);
            if (ports.Length > 0)
            {
                comboBoxPorts.SelectedIndex = 0;
            }
        }

        //清除接收
        private void btnClear_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        // 十六进制字符串转字节数组
        private byte[] HexStringToBytes(string hex)
        {
            hex = hex.Replace(" ", "").Replace("-", "").Replace(",", "");

            if (hex.Length % 2 != 0)
                throw new Exception("十六进制字符串长度必须是偶数");

            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return bytes;
        }

        // 窗体关闭时释放串口
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 关闭串口
            _serial.Close();
            // 把当前界面状态写进配置对象
            _settings.PortName = comboBoxPorts.Text;
            _settings.Encoding = comboBoxEncoding.Text;
            _settings.HexDisplay = chkHexDisplay.Checked;
            _settings.HexSend = chkHexSend.Checked;
            _settings.SendCheck = comboBoxSendCheck.Text;
            _settings.RecvCheck = comboBoxRecvCheck.Text;
            //保存到config.json
            ConfigHelper.Save(_settings);
            Logger.Write("程序关闭");
        }

        private void chkHexSend_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}