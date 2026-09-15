namespace YevmiyeTakip.Api.Models
{
    public class CreateDailyRecordDto
    {
        public DateOnly Date { get; set; }
        public int EmployerId { get; set; }
        public int WorkerId { get; set; }
        public string? Note { get; set; }

    }
}
