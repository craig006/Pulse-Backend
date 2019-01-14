using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ServeUp.System;

namespace ServeUp.Models
{
    public class DeviceWaterFlowTelemetry
    {
        public string DeviceId { get; set; }

        public WaterFlowTelemetry Telemetry { get; set; }
    }
}