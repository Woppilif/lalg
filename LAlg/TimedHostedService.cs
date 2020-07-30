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
        AutoSendMessage.AutoSendMessage aa = new AutoSendMessage.AutoSendMessage();

        public void Dispose()
        {
            _timer?.Dispose();
            //throw new NotImplementedException();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            aa.Start();

            Console.WriteLine("Timed Hosted Service running.");
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
            //throw new NotImplementedException();
        }

        private void DoWork(object state)
        {
            aa.Main();
            //var count = Interlocked.Increment(ref executionCount);
            //Console.WriteLine($"Timed Hosted Service is working. Count: {count}");
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
