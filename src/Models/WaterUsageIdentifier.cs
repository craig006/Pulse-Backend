namespace ServeUp.Models
{
    public class WaterUsageIdentifier
    {
        public string DeviceId { get; set; }
        public string SensorId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public WaterUsageIdentifier()
        {

        }

        public WaterUsageIdentifier(string deviceId, string sensorId, int month, int year)
        {
            this.DeviceId = deviceId;
            this.SensorId = sensorId;
            this.Month = month;
            this.Year = year;
        }


    }
}