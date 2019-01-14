using System.Collections.Generic;
using ServeUp.Database;

namespace ServeUp.Models
{
    public class WaterUsage: Document
    {
        public WaterUsageIdentifier Identity { get; set; }

        public List<DayLog> Days { get; set; } = new List<DayLog>();

        public void AddReading(int day, float milliliters)
        {
            var waterFlowLogDay = Days.Find(d => d.Day == day);

            if(waterFlowLogDay == null)
            {
                waterFlowLogDay = new DayLog{
                    Day = day
                };

                Days.Add(waterFlowLogDay);
            }

            waterFlowLogDay.Milliliters += milliliters;
        }

        public class DayLog
        {
            public int Day { get; set; }

            public float Milliliters { get; set; }
            
        }
        
    } 
}