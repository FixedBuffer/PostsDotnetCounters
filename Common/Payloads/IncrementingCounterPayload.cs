using System;
using System.Collections.Generic;

namespace Common.Payloads
{
    public class IncrementingCounterPayload : CounterPayloadBase
    {
        public IncrementingCounterPayload(IDictionary<string, object> payloadFields, int interval)
        {
            Name = payloadFields["Name"].ToString();
            Value = (double)payloadFields["Increment"];
            DisplayName = payloadFields["DisplayName"].ToString();
            DisplayRateTimeScale = payloadFields["DisplayRateTimeScale"].ToString();
            DisplayUnits = payloadFields["DisplayUnits"].ToString();
            var timescaleInSec = DisplayRateTimeScale.Length == 0 ? 1 : (int)TimeSpan.Parse(DisplayRateTimeScale).TotalSeconds;
            Value *= timescaleInSec;

            // En caso de que las propiedades no esten asignadas, añadimos un valor por defecto
            DisplayName = DisplayName.Length == 0 ? Name : DisplayName;
            DisplayRateTimeScale = DisplayRateTimeScale.Length == 0 ? $"{interval} sec" : $"{timescaleInSec} sec";
        }
    }
}