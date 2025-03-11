using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFE2024_QUIZZ_API.Data;
using PFE2024_QUIZZ_API.models;

namespace PFE2024_QUIZZ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TentativeController : ControllerBase
    {
        private AppDbContext _dbContext;
        public TentativeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<Tentative>> GetTentative()
        {
            var result = await _dbContext.Tentatives
                .Include(b => b.Candidat)
                .ToListAsync();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Tentative>> GetTentative(int id)
        {
            var result = await _dbContext.Tentatives.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> AddTentative(Tentative tentativetoAdd)
        {
            try
            {
                var ExitedTest = await _dbContext.Users.FindAsync(tentativetoAdd.CandidatId);
                if (ExitedTest == null)
                {
                    return BadRequest();

                }

                _dbContext.Tentatives.Add(tentativetoAdd);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(AddTentative), tentativetoAdd);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
