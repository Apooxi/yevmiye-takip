using Microsoft.AspNetCore.Mvc;
using YevmiyeTakip.Api.Data;
using YevmiyeTakip.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace YevmiyeTakip.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employer>>> GetEmployers()
        {
            var employers = await _context.Employers.ToListAsync();
            return Ok(employers);
        }

        [HttpPost]
        public async Task<ActionResult<Employer>> CreateEmployer(Employer employer)
        {
            _context.Employers.Add(employer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployer), new { id = employer.Id }, employer);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employer>> GetEmployer(int id)
        {
            var employer = await _context.Employers.FindAsync(id);

            if(employer == null)
            {
                return NotFound();
            }

            return Ok(employer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployer(int id, Employer employer)
        {
            if(id != employer.Id)
            {
                return BadRequest();
            }

            _context.Entry(employer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployer(int id)
        {
            var employer = await _context.Employers.FindAsync(id);

            if(employer == null)
            {
                return NotFound();
            }

            _context.Employers.Remove(employer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }


}
