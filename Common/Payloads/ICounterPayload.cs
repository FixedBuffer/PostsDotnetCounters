namespace Common.Payloads
{
    public interface ICounterPayload
    {
        string GetName();
        double GetValue();
        string GetUnits();
        string GetCounterType();
    }
}
