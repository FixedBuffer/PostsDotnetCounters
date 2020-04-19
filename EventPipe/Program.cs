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
        private static DiagnosticsClient _diagnosticsClient;
        private static EventPipeSession _session;
        private const int Interval = 1;
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter process Id:");
            var processId = Convert.ToInt32(Console.ReadLine());
            _diagnosticsClient = new DiagnosticsClient(processId);
            var arguments = new Dictionary<string, string>
            {
                {"EventCounterIntervalSec", $"{Interval}"}
            };
            var providers = new List<EventPipeProvider>
            {
                new EventPipeProvider("Example.Fixedbuffer", EventLevel.Informational, 0xffffffff, arguments),
                new EventPipeProvider("System.Runtime", EventLevel.Verbose, 0xffffffff, arguments)
            };

            _session = _diagnosticsClient.StartEventPipeSession(providers, false, 10);
            var source = new EventPipeEventSource(_session.EventStream);
            source.Dynamic.All += DynamicAllMonitor;
            source.Process();
        }

        private static void DynamicAllMonitor(TraceEvent obj)
        {
            if (obj.EventName.Equals("EventCounters"))
            {
                if (obj.ProviderName == "System.Runtime") return;

                var payloadVal = (IDictionary<string, object>)(obj.PayloadValue(0));
                var payloadFields = (IDictionary<string, object>)(payloadVal["Payload"]);
                var payload = PayloadHelper.GetPayload(payloadFields, Interval);
                Console.WriteLine($"{payload.GetName()}={payload.GetValue()}-{payload.GetUnits()}");
            }
        }
    }
}
