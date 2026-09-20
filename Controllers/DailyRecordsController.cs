using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YevmiyeTakip.Api.Data;
using YevmiyeTakip.Api.Models;

namespace YevmiyeTakip.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DailyRecordsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DailyRecordsController(AppDbContext context)
        {
            _context = context;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<DailyRecord>>> GetDailyRecords()
        {
            var records = await _context.DailyRecords
                .Include(r => r.Employer)
                .Include(r => r.Worker)
                .OrderByDescending(r => r.Date)
                .ToListAsync();

            return Ok(records);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DailyRecord>> GetDailyRecord(int id)
        {
            var record = await _context.DailyRecords
                .Include(r => r.Employer)
                .Include(r => r.Worker)
                .FirstOrDefaultAsync(r => r.Id == id);

            if(record == null)
            {
                return NotFound();
            }

            return Ok(record);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDailyRecord(int id, CreateDailyRecordDto dto)
        {
            var record = await _context.DailyRecords.FindAsync(id);

            if(record == null)
            {
                return NotFound();
            }

            var employerExists = await _context.Employers.AnyAsync(e => e.Id == dto.EmployerId);
            var workerExists = await _context.Workers.AnyAsync(w => w.Id == dto.WorkerId);

            if(!employerExists || !workerExists)
            {
                return BadRequest("Belirtilen isveren veya isci bulunamadi.");
            }

            record.Date = dto.Date;
            record.EmployerId = dto.EmployerId;
            record.WorkerId = dto.WorkerId;
            record.DailyWage = dto.DailyWage;
            record.Note = dto.Note;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<DailyRecord>> CreateDailyRecord(CreateDailyRecordDto dto)
        {
            var employerExists = await _context.Employers.AnyAsync(e => e.Id == dto.EmployerId);
            var workerExists = await _context.Workers.AnyAsync(w => w.Id == dto.WorkerId);

            if (!employerExists || !workerExists)
            {
                return BadRequest("Belirtilen isveren veya isci bulunamadi.");
            }

            var record = new DailyRecord
            {
                Date = dto.Date,
                EmployerId = dto.EmployerId,
                WorkerId = dto.WorkerId,
                DailyWage = dto.DailyWage,
                Note = dto.Note
            };

            _context.DailyRecords.Add(record);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDailyRecords), new {id = record.Id}, record);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDailyRecord(int id)
        {
            var record = await _context.DailyRecords.FindAsync(id);

            if(record == null)
            {
                return NotFound();
            }

            _context.DailyRecords.Remove(record);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    } 
}
