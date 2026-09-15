using System.Text.Json.Serialization;

namespace YevmiyeTakip.Api.Models
{
    public class Employer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Phone { get; set; }

        [JsonIgnore]
        public List<DailyRecord> DailyRecords { get; set; } = new();

    }
}
