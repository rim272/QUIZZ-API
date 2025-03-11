using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFE2024_QUIZZ_API.Data;
using PFE2024_QUIZZ_API.models;

namespace PFE2024_QUIZZ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveillanceController : ControllerBase
    {
        private AppDbContext _dbContext;
        public SurveillanceController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<Surveillance>> GetSurveillance()
        {
            var result = await _dbContext.Surveillances
                .Include(b => b.Tentative)
                .ToListAsync();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Surveillance>> GetSurveillance(int id)
        {
            var result = await _dbContext.Surveillances.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> AddSurveillance(Surveillance surveillancetoAdd)
        {
            try
            {
                var ExitedTest = await _dbContext.Tentatives.FindAsync(surveillancetoAdd.TentativeId);
                if (ExitedTest == null)
                {
                    return BadRequest();

                }

                _dbContext.Surveillances.Add(surveillancetoAdd);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(AddSurveillance), surveillancetoAdd);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
