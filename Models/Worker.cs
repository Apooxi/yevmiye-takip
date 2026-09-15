using System.Text.Json.Serialization;

namespace YevmiyeTakip.Api.Models
{
    public class Worker
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public decimal? DailyWage { get; set; }

        [JsonIgnore]
        public List<DailyRecord> DailyRecords { get; set; } = new();
    }
}
