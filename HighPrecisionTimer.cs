using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyProjectsTest
{
    // 特点：异步实现，不会阻塞 UI 线程
    //精度：约 15ms 左右
    public class HighPrecisionTimer : IDisposable
    {
        // 内部使用的官方定时器
        private PeriodicTimer _timer;

        // 用于取消循环
        private CancellationTokenSource _cts;

        // 循环任务
        private Task _loopTask;

        // 每次触发时要执行的动作
        private Action _callback;

        // 是否正在运行
        public bool IsRunning => _loopTask != null;

        // 构造函数：传入定时间隔和回调方法
        public HighPrecisionTimer(TimeSpan interval, Action callback)
        {
            _timer = new PeriodicTimer(interval);
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        public void Start()
        {
            // 已经在运行就不重复启动
            if (_loopTask != null) return;
            _cts = new CancellationTokenSource();
            // 在后台线程循环等待
            _loopTask = Task.Run(async () =>
            {
                try
                {
                    // 每到一个周期就执行一次回调
                    while (await _timer.WaitForNextTickAsync(_cts.Token))
                    {
                        _callback?.Invoke();
                    }
                }
                catch (OperationCanceledException)
                {
                }
            }, _cts.Token);
        }

        // 停止定时器
        public void Stop()
        {
            if (_cts == null) return;
            _cts.Cancel();
            _loopTask = null;
            _cts.Dispose();
            _cts = null;
        }

        // 释放资源
        public void Dispose()
        {
            Stop();
            _timer?.Dispose();
        }
    }
}