using System.Collections.Generic;

namespace ServeUp.Database
{
    public class QueryResult<T> : List<T>
    {
        public double RequestCharge { get; set; }         
    }
}