namespace YevmiyeTakip.Api.Models
{
    public class DailyRecord
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }

        public int EmployerId { get; set; }
        public Employer Employer { get; set; } = null!;

        public int WorkerId { get; set; }
        public Worker Worker { get; set; } = null!;
        
        public decimal? DailyWage { get; set; }

        public string? Note { get; set; }

    }
}
