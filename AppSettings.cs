using System;
using System.Collections.Generic;
using System.Text;

namespace MyProjectsTest
{
    /// <summary>
    /// 程序配置类
    /// 存储需要"下次启动时恢复"的所有设置
    /// 每个属性后面的 = xxx 是默认值（配置文件不存在时用）
    /// </summary>
    public class AppSettings
    {
        //文件是否启用
        public bool EnableConfig { get; set; } = true;

        //上次选择的端口
        public string PortName { get; set; } = "COM1";

        //上次选择的波特率
        public int BaudRate { get; set; } = 9600;

        //上次选择的编码
        public string Encoding { get; set; } = "GBK";

        //上次16进制显示
        public bool HexDisplay { get; set; } = false;

        //上次16进制接收
        public bool HexSend { get; set; } = false;

        //上次校验
        public string RecvCheck { get; set; } = "None";

        public string SendCheck { get; set; } = "None";
    }
}