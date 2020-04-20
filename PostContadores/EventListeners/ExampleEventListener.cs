using PostContadores.EventSources;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Common.Helpers;
using Common.Payloads;

namespace PostContadores.EventListeners
{
    public class ExampleEventListener : EventListener
    {
        private readonly int _refreshInterval;

        public ExampleEventListener(int refreshInterval)
        {
            _refreshInterval = refreshInterval;
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            if (eventData.EventSource.Name != ExampleEventSource.SourceName)
            {
                return;
            }

            foreach (IDictionary<string, object> payloadFields in eventData.Payload)
            {
                var payload = PayloadHelper.GetPayload(payloadFields,_refreshInterval);
                Console.WriteLine($"{payload.Name}={payload.Value}-{payload.DisplayUnits}");
            }
            base.OnEventWritten(eventData);
        }
    }
}