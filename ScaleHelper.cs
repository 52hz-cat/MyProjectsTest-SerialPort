namespace MyProjectsTest
{
    // 通用工程量线性换算
    public static class ScaleHelper
    {
        // 公式：工程量 = (原始值 - 原始下限) / (原始上限 - 原始下限) × (工程上限 - 工程下限) + 工程下限
        public static double Convert(double raw, double rawMin, double rawMax, double engMin, double engMax)
        {
            // 防止除零
            if (Math.Abs(rawMax - rawMin) < double.Epsilon)
                return engMin;
            return ((raw - rawMin) / (rawMax - rawMin) * (engMax - engMin) + engMin);
        }
    }
}