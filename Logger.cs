using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace MyProjectsTest
{
    public static class Logger
    {
        // 日志文件路径：AppDomain.CurrentDomain.BaseDirectory 就是 exe 所在目录
        // Path.Combine 把目录和文件名拼起来，得到 "C:\...\log.txt"
        private static readonly string LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");

        // 简单的进程内同步，防止 Write 与 CheckAndRotate 并发冲突
        private static readonly Lock SyncLock = new();

        //日志写入
        public static void Write(string message)
        {
            lock (SyncLock)
            {
                try
                {
                    // 时间戳
                    string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
                    // 使用追加写入，保持简单；捕获异常并输出到 Debug，避免吞异常
                    File.AppendAllText(LogPath, line + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Logger.Write failed: {ex}");
                }
            }
        }

        /// <summary>
        /// 检查日志文件是否超过 5MB，超过就改名归档，重新开始写
        /// 每次程序启动时调用一次即可
        /// </summary>
        public static void CheckAndRotate()
        {
            lock (SyncLock)
            {
                try
                {
                    // FileInfo 用来获取文件信息（大小、创建时间等）
                    FileInfo fi = new(LogPath);
                    // fi.Exists：文件存在吗？
                    // fi.Length：文件的字节数
                    // 5 * 1024 * 1024 字节 = 5MB（1024字节=1KB，1024KB=1MB）
                    if (fi.Exists && fi.Length > 5 * 1024 * 1024)
                    {
                        // 使用不含非法字符的时间格式，包含毫秒以减少同秒冲突
                        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
                        string oldName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"log_{timestamp}.txt");
                        File.Move(LogPath, oldName);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Logger.CheckAndRotate failed: {ex}");
                }
            }
        }
    }
}