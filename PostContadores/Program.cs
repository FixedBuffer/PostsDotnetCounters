using PostContadores.EventListeners;
using PostContadores.EventSources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Threading;

namespace PostContadores
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(Process.GetCurrentProcess().Id);
            var refreshInterval = 5;

            var exampleEventSource = new ExampleEventSource();
            var exampleListener = new ExampleEventListener(refreshInterval);
            var arguments = new Dictionary<string, string>
            {
                {"EventCounterIntervalSec", $"{refreshInterval}"}
            };
            exampleListener.EnableEvents(exampleEventSource, EventLevel.Verbose, EventKeywords.All, arguments);
            for (var i = 0; i < 3000000; i++)
            {
                exampleEventSource.Increment();
            }

            Console.Read();

            for (var i = 0; i < 3000000; i++)
            {
                exampleEventSource.Increment();
                Thread.Sleep(1);
            }
            Console.Read();
        }
    }
}
