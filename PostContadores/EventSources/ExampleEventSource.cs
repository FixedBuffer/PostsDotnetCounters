using System;
using System.Diagnostics.Tracing;

namespace PostContadores.EventSources
{
    public class ExampleEventSource : EventSource
    {
        public const string SourceName = "Example.Fixedbuffer";
        private readonly EventCounter _eventCounter;
        private readonly PollingCounter _poolingCounter;
        private readonly IncrementingEventCounter _incrementingCounter;
        private readonly IncrementingPollingCounter _incrementingPollingCounter;
        private ulong _executions;
        public ExampleEventSource() : base(SourceName)
        {
            _eventCounter = new EventCounter("event-counter", this)
            {
                DisplayUnits = "elements",
                DisplayName = "EventCounter"
            };

            _incrementingCounter = new IncrementingEventCounter("incrementing-counter", this)
            {
                DisplayUnits = "elements/s",
                DisplayName = "IncrementingEventCounter",
            };

            _poolingCounter = new PollingCounter("pooling-counter", this, () => _executions)
            {
                DisplayUnits = "elements",
                DisplayName = "PollingCounter"
            };

            _incrementingPollingCounter = new IncrementingPollingCounter("incrementing-pooling-counter", this, () => _executions)
            {
                DisplayUnits = "elements/s",
                DisplayName = "IncrementingPollingCounter",
            };
        }

        public void Increment()
        {
            _executions++;
            _eventCounter.WriteMetric(_executions);
            _incrementingCounter.Increment();
        }
    }
}
