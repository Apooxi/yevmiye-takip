using Microsoft.EntityFrameworkCore;
using YevmiyeTakip.Api.Models;

namespace YevmiyeTakip.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {

        } 

        public DbSet<Employer> Employers { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<DailyRecord> DailyRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aynı işçi, aynı işverende, aynı gün iki kez kaydedilmesin diye
            modelBuilder.Entity<DailyRecord>()
                .HasIndex(d => new { d.EmployerId, d.WorkerId, d.Date })
                .IsUnique();
        }
    }
}
