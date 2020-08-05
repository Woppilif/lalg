using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoSendMessage;

namespace LAlg
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private Timer _timer;
        AutoSendMessage.AutoSendMessage.Message aa = new AutoSendMessage.AutoSendMessage.Message();

        public void Dispose()
        {
            _timer?.Dispose();
            //throw new NotImplementedException();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            AutoSendMessage.AutoSendMessage.Start();

            Console.WriteLine("Timed Hosted Service running.");
            _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(10),
            TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
            //throw new NotImplementedException();
        }

        private void DoWork(object state)
        {
            aa.Run();
            //aa.Main();
            //var count = Interlocked.Increment(ref executionCount);
            Console.WriteLine("Timed Hosted Service is working. Count:");
            //throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Timed Hosted Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
            //throw new NotImplementedException();
        }
    }
}
