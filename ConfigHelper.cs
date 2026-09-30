using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;

namespace MyProjectsTest
{
    /// <summary>
    /// 配置文件的读写工具
    /// 保存格式：JSON（人也能看懂）
    /// 保存位置：exe 所在目录下的 config.json
    /// </summary>
    public static class ConfigHelper
    {
        //配置文件路径
        private static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

        /// <summary>
        /// 保存配置到文件（程序关闭时调用）
        /// </summary>
        public static void Save(AppSettings settings)
        {
            try
            {
                // 把对象转成 JSON 字符串
                // WriteIndented = true 表示美化输出（有缩进、换行，方便手动查看）
                //语法糖：因为只需要用一次，所以没有必要单独起个变量名
                string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(ConfigPath, json);
            }
            catch { }
        }

        /// <summary>
        /// 从文件读取配置（程序启动时调用）
        /// 文件不存在或读取失败时，返回一个全默认值的配置对象
        /// </summary>
        public static AppSettings Load()
        {
            //文件存在时才读取
            if (File.Exists(ConfigPath))
            {
                try
                {
                    string json = File.ReadAllText(ConfigPath);
                    //把json字符串还原成对象
                    // ?? new AppSettings()：如果反序列化结果是 null，用默认值代替
                    return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
                }
                catch { }
            }
            //文件不存在时 返回默认配置
            return new AppSettings();
        }
    }
}