namespace MyProjectsTest
{
    // 各种校验算法工具
    // 支持：无校验、CRC16、校验和、异或、CRC8
    public class ChecksumHelper
    {
        // 根据名称返回校验字节
        // CRC16 返回2字节，Sum/Xor/CRC8 返回1字节，None 返回空
        public static byte[] Compute(string method, byte[] data)
        {
            switch (method)
            {
                case "CRC16": return Crc16(data);
                case "Sum": return new byte[] { Sum(data) };
                case "Xor": return new byte[] { Xor(data) };
                case "CRC8": return new byte[] { Crc8(data) };
                default: return new byte[0];//None
            }
        }

        // CRC16 Modbus，低字节在前，高字节在后
        public static byte[] Crc16(byte[] data)
        {
            ushort crc = 0xFFFF;
            foreach (byte b in data)
            {
                crc ^= b;
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 1) != 0) { crc >>= 1; crc ^= 0xA001; }
                    else crc >>= 1;
                }
            }
            return new byte[] { (byte)(crc & 0xFF), (byte)(crc >> 8) };
        }

        // 累加和校验（所有字节相加，取低8位）
        public static byte Sum(byte[] data)
        {
            byte sum = 0;
            foreach (byte b in data) sum += b;
            return sum;
        }

        // 异或校验（所有字节逐个异或）
        public static byte Xor(byte[] data)
        {
            byte x = 0;
            foreach (byte b in data) x ^= b;
            return x;
        }

        // CRC8 校验（多项式 0x07，初始值 0x00）
        public static byte Crc8(byte[] data)
        {
            byte crc = 0x00;
            foreach (byte b in data)
            {
                crc ^= b;
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x80) != 0) { crc = (byte)((crc << 1) ^ 0x07); }
                    else crc = (byte)(crc << 1);
                }
            }
            return crc;
        }
    }
}