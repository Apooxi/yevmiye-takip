using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YevmiyeTakip.Api.Data;
using YevmiyeTakip.Api.Models;

namespace YevmiyeTakip.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorkersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Worker>>> GetWorkers()
        {
            var workers = await _context.Workers.ToListAsync();
            return Ok(workers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Worker>> GetWorker(int id)
        {
            var worker = await _context.Workers.FindAsync(id);

            if(worker == null)
            {
                return NotFound();
            }

            return Ok(worker);
        }

        [HttpPost]
        public async Task<ActionResult<Worker>> CreateWorker(Worker worker)
        {
            _context.Add(worker);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetWorker), new {id = worker.Id}, worker);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorker(int id, Worker worker)
        {
            if(id != worker.Id)
            {
                return BadRequest();
            }

            _context.Entry(worker).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker(int id)
        {
            var worker = await _context.Workers.FindAsync(id);

            if( worker == null)
            {
                return NotFound();
            }

            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
