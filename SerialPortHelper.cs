using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace MyProjectsTest
{
    // 串口通信帮助类
    // 职责：只负责串口的打开、关闭、发送、接收字节
    // 不关心界面，不关心显示，不关心编码
    // 内置接收缓冲 + 超时判定，自动处理粘包分包
    public class SerialPortHelper
    {
        // 内部串口对象，private 表示只有本类能用，外部看不到
        private SerialPort _port = new SerialPort();

        // 接收缓冲区：暂存还没"收完"的数据
        private List<byte> _receiveBuffer = [];

        // 帧间隔超时定时器：多久没有新数据，认为一帧结束
        private System.Timers.Timer _frameTimer;

        // 帧间隔超时时间（毫秒）
        // Modbus RTU 规定 3.5 字符间隔，实际留余量
        // 9600 波特率用 20ms，115200 用 5~8ms
        private const int FrameTimeoutMs = 20;

        // 线程锁，保护缓冲区（防止 UI 线程和串口线程同时操作）
        private readonly Lock _bufferLock = new();

        // 数据到达事件
        // Action<byte[]> 是泛型委托，表示"无返回值、带一个 byte[] 参数的方法"
        // 外部（Form1）订阅这个事件后，一帧完整数据到达时通知外部
        public event Action<byte[]> DataReceived;

        // 构造函数
        // 创建对象时自动执行，把内部串口的 DataReceived 挂到自己的处理方法上
        // 同时初始化帧间隔超时定时器
        public SerialPortHelper()
        {
            _port.DataReceived += Port_DataReceived;

            // 初始化超时定时器
            _frameTimer = new System.Timers.Timer(FrameTimeoutMs)
            {
                AutoReset = false   // 只触发一次
            };
            _frameTimer.Elapsed += OnFrameTimeout;
        }

        // 只读属性：返回串口当前是否打开
        // => 是 C# 的"表达式成员"语法，等价于 { get { return _port.IsOpen; } }
        public bool IsOpen => _port.IsOpen;

        // 打开串口
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

        // 关闭串口
        // 如果串口本来就没打开，直接跳过，不报错
        public void Close()
        {
            if (_port.IsOpen) _port.Close();
            // 停止定时器
            _frameTimer.Stop();
            lock (_bufferLock)
            {
                _receiveBuffer.Clear();
            }
        }

        // 发送字节数组（用于十六进制发送）
        public void Send(byte[] data)
        {
            // 只在串口打开时才发送，防止崩溃
            if (_port.IsOpen) _port.Write(data, 0, data.Length);
        }

        // 发送文本字符串（用于文本发送）
        // WriteLine 会自动在末尾加换行符
        public void Send(string text)
        {
            if (_port.IsOpen) _port.WriteLine(text);
        }

        // 串口原始数据到达
        // 收到的可能是半帧、一帧、或多帧黏在一起，统一先扔进缓冲区
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // 串口缓冲区里现在有多少个字节可读
            int bytesToRead = _port.BytesToRead;
            if (bytesToRead <= 0) return;

            // 创建一个正好能装下这些字节的数组
            byte[] buffer = new byte[bytesToRead];

            // 从串口读数据到 buffer，返回实际读到的字节数
            // Read(数组, 从数组的第几位开始放, 最多读多少个)
            int read = _port.Read(buffer, 0, bytesToRead);

            // 加到接收缓冲区
            lock (_bufferLock)
            {
                for (int i = 0; i < read; i++)
                {
                    _receiveBuffer.Add(buffer[i]);
                }
            }

            // 重置超时定时器（每次收到新数据就重置）
            // 只要还在收数据，定时器就一直不触发
            _frameTimer.Stop();
            _frameTimer.Start();
        }

        // 超时触发：认为一帧收完了
        // 此时缓冲区里的所有数据作为一帧抛出
        private void OnFrameTimeout(object sender, System.Timers.ElapsedEventArgs e)
        {
            byte[] frame;

            lock (_bufferLock)
            {
                // 缓冲区为空直接返回
                if (_receiveBuffer.Count == 0) return;

                // 把缓冲区的所有数据作为一帧
                frame = _receiveBuffer.ToArray();
                _receiveBuffer.Clear();
            }
            try
            {
                // 抛给外部处理
                DataReceived?.Invoke(frame);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
    }
}