namespace Common.Payloads
{
    public abstract class CounterPayloadBase
    {
        public string Name { get; protected set; }
        public double Value { get; protected set; }
        public string DisplayName { get; protected set; }
        public string DisplayUnits { get; protected set; }
        public string DisplayRateTimeScale { get; protected set; }

        public CounterPayloadBase()
        {
            Name = string.Empty;
            DisplayName = string.Empty;
            DisplayUnits = string.Empty;
            DisplayRateTimeScale = string.Empty;
        }
    }
}