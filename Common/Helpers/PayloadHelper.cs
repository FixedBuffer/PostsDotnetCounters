using System.Collections.Generic;
using Common.Payloads;

namespace Common.Helpers
{
    public static class PayloadHelper
    {
        public static ICounterPayload GetPayload(IDictionary<string, object> payloadFields, int _refreshInterval = 1)
        {
            if (payloadFields["CounterType"].Equals("Sum"))
            {
                return new IncrementingCounterPayload(payloadFields, _refreshInterval);
            }
            else
            {
                return new CounterPayload(payloadFields);
            }
        }
    }
}
