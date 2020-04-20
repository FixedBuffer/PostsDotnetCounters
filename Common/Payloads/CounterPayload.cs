using System;
using System.Collections.Generic;

namespace Common.Payloads
{
    public class CounterPayload : CounterPayloadBase
    {
        public CounterPayload(IDictionary<string, object> payloadFields)
        {
            Name = payloadFields["Name"].ToString();
            Value = Convert.ToDouble(payloadFields["Mean"]);
            DisplayName = payloadFields["DisplayName"].ToString();
            DisplayUnits = payloadFields["DisplayUnits"].ToString();

            // En caso de que las propiedades no esten asignadas, añadimos un valor por defecto
            DisplayName = DisplayName.Length == 0 ? Name : DisplayName;
        }
    }
}