using System.IO.Ports;

namespace MyProjectsTest
{
    /// <summary>
    /// 串口通信帮助类
    /// 职责：只负责串口的打开、关闭、发送、接收字节
    /// 不关心界面，不关心显示，不关心编码
    /// </summary>
    public class SerialPortHelper
    {
        // 内部串口对象，private 表示只有本类能用，外部看不到
        private SerialPort _port = new SerialPort();

        /// <summary>
        /// 数据到达事件
        /// Action<byte[]> 是一种泛型委托，表示"没有返回值、带一个 byte[] 参数的方法"
        /// 外部（Form1）订阅这个事件后，串口一收到数据就会通知外部
        /// </summary>
        public event Action<byte[]>? DataReceived;

        /// <summary>
        /// 构造函数
        /// 创建对象时自动执行，把内部串口的 DataReceived 挂到自己写的处理方法上
        /// 这样串口一收到数据，就会触发 Port_DataReceived
        /// </summary>
        public SerialPortHelper()
        {
            _port.DataReceived += Port_DataReceived;
        }

        /// <summary>
        /// 只读属性：返回串口当前是否打开
        /// => 是 C# 的"表达式成员"语法，等价于 { get { return _port.IsOpen; } }
        /// </summary>
        public bool IsOpen => _port.IsOpen;

        /// <summary>
        /// 打开串口
        /// </summary>
        public void Open(string portName, int baudRate, int dataBits, StopBits stopBits, Parity parity)
        {
            _port.PortName = portName;
            _port.BaudRate = baudRate;
            _port.DataBits = dataBits;
            _port.StopBits = stopBits;
            _port.Parity = parity;

            // DTR 和 RTS 是串口的两个控制信号，部分 USB 转串口设备必须置 true 才能正常收发
            _port.DtrEnable = true;
            _port.RtsEnable = true;

            _port.Open();
        }

        /// <summary>
        /// 关闭串口
        /// 如果串口本来就没打开，直接跳过，不报错
        /// </summary>
        public void Close()
        {
            if (_port.IsOpen) _port.Close();
        }

        /// <summary>
        /// 发送字节数组（用于十六进制发送）
        /// </summary>
        public void Send(byte[] data)
        {
            // 只在串口打开时才发送，防止崩溃
            if (_port.IsOpen) _port.Write(data, 0, data.Length);
        }

        /// <summary>
        /// 发送文本字符串（用于文本发送）
        /// WriteLine 会自动在末尾加换行符
        /// </summary>
        public void Send(string text)
        {
            if (_port.IsOpen) _port.WriteLine(text);
        }

        /// <summary>
        /// 内部处理方法：串口收到数据时自动触发
        /// 这个方法由 SerialPort 的 DataReceived 事件调用
        /// 职责：把字节从缓冲区读出来，然后通过 DataReceived 事件抛给外部
        /// </summary>
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // 1. 问串口缓冲区里现在有多少个字节可读
            int bytesToRead = _port.BytesToRead;

            // 2. 创建一个正好能装下这些字节的数组
            byte[] buffer = new byte[bytesToRead];

            // 3. 从串口读数据到 buffer，返回实际读到的字节数
            // Read(数组, 从数组的第几位开始放, 最多读多少个)
            int read = _port.Read(buffer, 0, bytesToRead);

            // 4. 如果实际读到的字节数少于准备的数组长度，把多余的裁掉
            // （理论上 read == bytesToRead，但防止意外情况）
            byte[] actualData = new byte[read];

            // Array.Copy(源数组, 目标数组, 拷贝多少个)
            Array.Copy(buffer, actualData, read);

            // 5. 触发事件，把数据抛给外部
            // ?. 是 null 条件运算符：如果没人订阅（DataReceived == null），就跳过，不报错
            // Invoke 就是执行这个事件
            DataReceived?.Invoke(actualData);
        }
    }
}