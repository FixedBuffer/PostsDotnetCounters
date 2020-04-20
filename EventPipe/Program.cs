using Common.Helpers;
using Microsoft.Diagnostics.NETCore.Client;
using Microsoft.Diagnostics.Tracing;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace EventPipe
{
    internal class Program
    {
        private const int Interval = 1;
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter process Id:");
            var processId = Convert.ToInt32(Console.ReadLine());
            var arguments = new Dictionary<string, string>
            {
                {"EventCounterIntervalSec", $"{Interval}"}
            };
            var diagnosticsClient = new DiagnosticsClient(processId);
            var providers = new List<EventPipeProvider>
            {
                new EventPipeProvider("Example.Fixedbuffer", EventLevel.Informational, 0xffffffff, arguments),
                new EventPipeProvider("System.Runtime", EventLevel.Verbose, 0xffffffff, arguments)
            };

            var session = diagnosticsClient.StartEventPipeSession(providers, false, 10);
            var source = new EventPipeEventSource(session.EventStream);
            source.Dynamic.All += obj =>
            {
                if (obj.EventName.Equals("EventCounters"))
                {
                    var payloadVal = (IDictionary<string, object>)(obj.PayloadValue(0));
                    var payloadFields = (IDictionary<string, object>)(payloadVal["Payload"]);
                    var payload = PayloadHelper.GetPayload(payloadFields, Interval);
                    Console.WriteLine($"{payload.DisplayName}={payload.Value}-{payload.DisplayUnits}");
                }
            };
            source.Process();
        }
    }
}
