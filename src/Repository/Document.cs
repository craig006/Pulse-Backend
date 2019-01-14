using Newtonsoft.Json;

namespace ServeUp.Database
{
    public class Document
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Type { get => this.GetType().Name; }
    }
}