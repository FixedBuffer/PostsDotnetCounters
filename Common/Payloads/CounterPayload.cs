using System;
using System.Collections.Generic;

namespace Common.Payloads
{
    public class CounterPayload : ICounterPayload
    {
        private readonly string _name;
        private readonly double _value;
        private readonly string _displayName;
        private readonly string _displayUnits;

        public CounterPayload(IDictionary<string, object> payloadFields)
        {
            _name = payloadFields["Name"].ToString();
            _value = Convert.ToDouble(payloadFields["Mean"]);
            _displayName = payloadFields["DisplayName"].ToString();
            _displayUnits = payloadFields["DisplayUnits"].ToString();

            // En caso de que las propiedades no esten asignadas, añadimos un valor por defecto
            _displayName = _displayName.Length == 0 ? _name : _displayName;
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
            return "Metric";
        }
    }
}
