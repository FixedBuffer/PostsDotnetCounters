using System;
using System.Collections.Generic;

namespace Common.Payloads
{
    public class IncrementingCounterPayload : ICounterPayload
    {
        private readonly string _name;
        private readonly double _value;
        private readonly string _displayName;
        private readonly string _displayRateTimeScale;
        private readonly string _displayUnits;

        public IncrementingCounterPayload(IDictionary<string, object> payloadFields, int interval)
        {
            _name = payloadFields["Name"].ToString();
            _value = (double)payloadFields["Increment"];
            _displayName = payloadFields["DisplayName"].ToString();
            _displayRateTimeScale = payloadFields["DisplayRateTimeScale"].ToString();
            _displayUnits = payloadFields["DisplayUnits"].ToString();
            var timescaleInSec = _displayRateTimeScale.Length == 0 ? 1 : (int)TimeSpan.Parse(_displayRateTimeScale).TotalSeconds;
            _value *= timescaleInSec;

            // En caso de que las propiedades no esten asignadas, añadimos un valor por defecto
            _displayName = _displayName.Length == 0 ? _name : _displayName;
            _displayRateTimeScale = _displayRateTimeScale.Length == 0 ? $"{interval} sec" : $"{timescaleInSec} sec";
        }

        public string GetName()
        {
            return _name;
        }

        public double GetValue()
        {
            return _value;
        }

        public string GetUnits()
        {
            return _displayUnits;
        }

        public string GetCounterType()
        {
            return "Rate";
        }
    }
}
