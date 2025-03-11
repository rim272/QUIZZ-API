using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFE2024_QUIZZ_API.Data;
using PFE2024_QUIZZ_API.models;

namespace PFE2024_QUIZZ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyseIAController : ControllerBase
    {
        private AppDbContext _dbContext;
        public AnalyseIAController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<AnalyseIA>> GetAnalyseIA()
        {
            var result = await _dbContext.AnalysesIA
                .Include(b => b.Test)
                .ToListAsync();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AnalyseIA>> GetAnalyseIA(int id)
        {
            var result = await _dbContext.AnalysesIA.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> AddAnalyseIA(AnalyseIA analyseIAtoAdd)
        {
            try
            {
                var ExitedTest = await _dbContext.Tests.FindAsync(analyseIAtoAdd.TestId);
                if (ExitedTest == null)
                {
                    return BadRequest();

                }

                _dbContext.AnalysesIA.Add(analyseIAtoAdd);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(AddAnalyseIA), analyseIAtoAdd);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
