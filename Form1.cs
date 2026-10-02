using System.IO.Ports;
using System.Text;

namespace MyProjectsTest
{
    public partial class Form1 : Form
    {
        private bool _loadingConfig = false;
        private SerialPortHelper _serial = new();
        private AppSettings _settings;

        public Form1()
        {
            InitializeComponent();
            // 程序启动时，检查日志是否超过 5MB
            Logger.CheckAndRotate();
            // 订阅数据到达事件 会自动调用OnSerialDataReceived
            _serial.DataReceived += OnSerialDataReceived;
            //信号类型联动
            comboBoxSignalType.SelectedIndexChanged += ComboBoxSignalType_Changed;

            // 自动扫描可用串口并填充到下拉框
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            comboBoxPorts.Items.Clear();
            comboBoxPorts.Items.AddRange(ports);
            if (ports.Length > 0)
            {
                comboBoxPorts.SelectedIndex = 0;
            }

            // 波特率
            comboBoxBaudRate.Items.Clear();
            comboBoxBaudRate.Items.AddRange(new string[] { "9600", "19200", "38400", "57600", "115200" });
            comboBoxBaudRate.SelectedIndex = 0;

            // 数据位
            comboBoxDataBits.Items.Clear();
            comboBoxDataBits.Items.AddRange(new string[] { "8", "7", "6", "5" });
            comboBoxDataBits.SelectedIndex = 0;

            // 停止位
            comboBoxStopBits.Items.Clear();
            comboBoxStopBits.Items.AddRange(new string[] { "1", "1.5", "2" });
            comboBoxStopBits.SelectedIndex = 0;

            // 校验位
            comboBoxParity.Items.Clear();
            comboBoxParity.Items.AddRange(new string[] { "None", "Even", "Odd", "Mark", "Space" });
            comboBoxParity.SelectedIndex = 0;
            // 编码下拉框
            comboBoxEncoding.Items.Clear();
            comboBoxEncoding.Items.AddRange(
            [
                "GBK",
                "UTF-8",
                "ASCII"
            ]);
            comboBoxEncoding.SelectedIndex = 0;

            // 发送校验下拉框
            comboBoxSendCheck.Items.Clear();
            comboBoxSendCheck.Items.AddRange(
            [
                "None",
                "CRC16",
                "Sum",
                "Xor",
                "CRC8"
            ]);
            comboBoxSendCheck.SelectedIndex = 0;

            // 接收校验下拉框
            comboBoxRecvCheck.Items.Clear();
            comboBoxRecvCheck.Items.AddRange(
            [
                "None",
                "CRC16",
                "Sum",
                "Xor",
                "CRC8"
            ]);
            comboBoxRecvCheck.SelectedIndex = 0;

            // 信号类型下拉框
            comboBoxSignalType.Items.Clear();
            comboBoxSignalType.Items.AddRange(
            [
                "电流 (4-20mA)",
                "电流 (0-20mA)",
                "电压 (0-5V)",
                "电压 (0-10V)",
                "ADC原始值 (12位)",
                "ADC原始值 (16位)",
                "自定义"
            ]);
            comboBoxSignalType.SelectedIndex = 0;

            //从config.json读取
            _settings = ConfigHelper.Load();
            //判断是否启用配置文件
            if (_settings.EnableConfig)
            {
                // 【启用配置】恢复上次的状态 恢复标识符
                _loadingConfig = true;
                // 端口：只有下拉框里存在这个端口，才选中它
                if (comboBoxPorts.Items.Contains(_settings.PortName))
                {
                    comboBoxPorts.SelectedItem = _settings.PortName;
                }
                else if (comboBoxPorts.Items.Count > 0)
                {
                    comboBoxPorts.SelectedIndex = 0;
                }
                //波特 数据 停止 校验
                comboBoxBaudRate.SelectedItem = _settings.BaudRate;
                comboBoxDataBits.SelectedItem = _settings.DataBits;
                comboBoxStopBits.SelectedItem = _settings.StopBits;
                comboBoxParity.SelectedItem = _settings.Parity;
                //编码
                comboBoxEncoding.SelectedItem = _settings.Encoding;
                //十六进制
                chkHexSend.Checked = _settings.HexSend;
                chkHexDisplay.Checked = _settings.HexDisplay;
                comboBoxSendCheck.SelectedItem = _settings.SendCheck;
                comboBoxRecvCheck.SelectedItem = _settings.RecvCheck;
                // 工程量转换
                chkScale.Checked = _settings.ScaleEnable;
                comboBoxSignalType.SelectedItem = _settings.SignalType;
                txtRawMin.Text = _settings.RawMin;
                txtRawMax.Text = _settings.RawMax;
                txtEngMin.Text = _settings.EngMin;
                txtEngMax.Text = _settings.EngMax;
                //恢复结束 允许联动
                _loadingConfig = false;
            }
            else
            {
                // 【不启用配置】用默认值（选中第一项）
                if (comboBoxPorts.Items.Count > 0)
                {
                    comboBoxPorts.SelectedIndex = 0;
                }
                comboBoxBaudRate.SelectedIndex = 0;  // 9600
                comboBoxDataBits.SelectedIndex = 0;  // 8
                comboBoxStopBits.SelectedIndex = 0;  // 1
                comboBoxParity.SelectedIndex = 0;    // None
                comboBoxEncoding.SelectedIndex = 0;
                comboBoxSendCheck.SelectedIndex = 0;
                comboBoxRecvCheck.SelectedIndex = 0;
            }
            //同步"启用配置文件"
            chkEnableConfig.Checked = _settings.EnableConfig;
            // 手动勾选/取消时，更新到配置对象（关闭时会保存）
            chkEnableConfig.CheckedChanged += (s, e) =>
            {
                _settings.EnableConfig = chkEnableConfig.Checked;
            };
        }

        //清除接收
        private void btnClear_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        // 打开/关闭串口
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (!_serial.IsOpen)
            {
                // 检查必填项
                if (string.IsNullOrEmpty(comboBoxPorts.Text) ||
                    string.IsNullOrEmpty(comboBoxBaudRate.Text) ||
                    string.IsNullOrEmpty(comboBoxDataBits.Text) ||
                    string.IsNullOrEmpty(comboBoxStopBits.Text) ||
                    string.IsNullOrEmpty(comboBoxParity.Text))
                {
                    MessageBox.Show("请先把串口参数选择完整！");
                    return;
                }
                try
                {
                    // 从下拉框解析参数
                    string portName = comboBoxPorts.Text;
                    int baudRate = int.Parse(comboBoxBaudRate.Text);
                    int dataBits = int.Parse(comboBoxDataBits.Text);

                    // 停止位：字符串转枚举
                    StopBits stopBits;
                    switch (comboBoxStopBits.Text)
                    {
                        case "1": stopBits = StopBits.One; break;
                        case "1.5": stopBits = StopBits.OnePointFive; break;
                        case "2": stopBits = StopBits.Two; break;
                        default: stopBits = StopBits.One; break;
                    }

                    // 校验位：字符串转枚举
                    Parity parity;
                    switch (comboBoxParity.Text)
                    {
                        case "None": parity = Parity.None; break;
                        case "Even": parity = Parity.Even; break;
                        case "Odd": parity = Parity.Odd; break;
                        case "Mark": parity = Parity.Mark; break;
                        case "Space": parity = Parity.Space; break;
                        default: parity = Parity.None; break;
                    }

                    // 打开串口
                    _serial.Open(portName, baudRate, dataBits, stopBits, parity);
                    labelStatus.Text = $"串口打开成功 ({portName}, {baudRate}, {dataBits}, {comboBoxStopBits.Text}, {comboBoxParity.Text})";
                    btnOpen.Text = "关闭串口";
                    Logger.Write($"串口打开成功:{portName},{baudRate},{dataBits},{comboBoxStopBits.Text},{comboBoxParity.Text}");
                }
                catch (Exception ex)
                {
                    labelStatus.Text = "打开失败：" + ex.Message;
                    Logger.Write($"串口打开失败：{ex.Message}");
                }
            }
            else
            {
                _serial.Close();
                labelStatus.Text = "串口已关闭";
                btnOpen.Text = "打开串口";
            }
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

        //改变时触发
        private void ComboBoxSignalType_Changed(object sender, EventArgs e)
        {
            // 恢复配置期间不联动
            if (_loadingConfig)
            {
                return;
            }
            RefreshRawDefaults();
        }

        // 窗体关闭时释放串口
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 关闭串口
            _serial.Close();
            // 把当前界面状态写进配置对象
            _settings.PortName = comboBoxPorts.Text;
            _settings.BaudRate = comboBoxBaudRate.Text;
            _settings.DataBits = comboBoxDataBits.Text;
            _settings.StopBits = comboBoxStopBits.Text;
            _settings.Parity = comboBoxParity.Text;
            _settings.Encoding = comboBoxEncoding.Text;
            _settings.HexDisplay = chkHexDisplay.Checked;
            _settings.HexSend = chkHexSend.Checked;
            _settings.SendCheck = comboBoxSendCheck.Text;
            _settings.RecvCheck = comboBoxRecvCheck.Text;
            //工程转换数据
            _settings.ScaleEnable = chkScale.Checked;
            _settings.SignalType = comboBoxSignalType.Text;
            _settings.RawMin = txtRawMin.Text;
            _settings.RawMax = txtRawMax.Text;
            _settings.EngMin = txtEngMin.Text;
            _settings.EngMax = txtEngMax.Text;
            //保存到config.json
            ConfigHelper.Save(_settings);
            Logger.Write("程序关闭");
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

        // 串口收到数据时触发，只负责显示
        private void OnSerialDataReceived(byte[] data)
        {
            // 跨线程更新 UI
            this.BeginInvoke(new Action(() =>
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
                                if (recvCheck[i] != calcCheck[i])
                                {
                                    ok = false;
                                    break;
                                }
                            }
                            string tip = ok ? "校验正确" : "校验错误";
                            receivedData += $"[{checkMethod}{tip}]";
                        }
                        else
                        {
                            receivedData += "[数据太短无法校验]";
                        }
                    }
                    // 工程量转换
                    if (chkScale.Checked)
                    {
                        try
                        {
                            double rawMin = double.Parse(txtRawMin.Text);
                            double rawMax = double.Parse(txtRawMax.Text);
                            double engMin = double.Parse(txtEngMin.Text);
                            double engMax = double.Parse(txtEngMax.Text);
                            int dataStart = 3;
                            int dataEnd = data.Length - 2;
                            if (dataEnd > dataStart)
                            {
                                for (int i = dataStart; i + 1 < dataEnd; i += 2)
                                {
                                    ushort raw = (ushort)((data[i] << 8) | (data[i + 1]));
                                    double eng = ScaleHelper.Convert(
                                        raw: raw,
                                        rawMin: rawMin,
                                        rawMax: rawMax,
                                        engMin: engMin,
                                        engMax: engMax);
                                    receivedData += $"\r\n    第{(i - dataStart) / 2 + 1}路: {raw} → {eng:F2}";
                                }
                            }
                        }
                        catch
                        {
                            receivedData += "\r\n[工程量错误]";
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
                //显示
                textBox1.AppendText(receivedData + Environment.NewLine);
                //滚动到光标所在的位置。
                textBox1.ScrollToCaret();
            }));
        }

        // 根据信号类型 + 是否ADC，刷新原始上下限的默认值
        private void RefreshRawDefaults()
        {
            switch (comboBoxSignalType.Text)
            {
                case "电流 (4-20mA)":
                    txtRawMin.Text = "4";
                    txtRawMax.Text = "20";
                    break;

                case "电流 (0-20mA)":
                    txtRawMin.Text = "0";
                    txtRawMax.Text = "20";
                    break;

                case "电压 (0-5V)":
                    txtRawMin.Text = "0";
                    txtRawMax.Text = "5";
                    break;

                case "电压 (0-10V)":
                    txtRawMin.Text = "0";
                    txtRawMax.Text = "10";
                    break;

                case "ADC原始值 (12位)":
                    txtRawMin.Text = "0";
                    txtRawMax.Text = "4095";
                    break;

                case "ADC原始值 (16位)":
                    txtRawMin.Text = "0";
                    txtRawMax.Text = "65535";
                    break;

                case "自定义":
                    // 不动，让用户自己填
                    break;
            }
        }

    }
}