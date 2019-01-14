using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ServeUp.System;

namespace ServeUp.Models
{
    public class WaterFlowTelemetry
    {
        [JsonProperty(PropertyName = "ml")]
        public float Milliliters { get; set; }

        [JsonProperty(PropertyName = "startTime")]
        public DateTime Start { get; set; }

        [JsonProperty(PropertyName = "endTime")]
        public DateTime End { get; set; }

        [JsonProperty(PropertyName = "sensorId")]
        public string SensorId { get; set; }
    }
}