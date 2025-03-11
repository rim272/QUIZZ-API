using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFE2024_QUIZZ_API.Data;
using PFE2024_QUIZZ_API.models;

namespace PFE2024_QUIZZ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReponseController : ControllerBase
    {
        private AppDbContext _dbContext;
        public ReponseController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<Question>> GetReponse()
        {
            var result = await _dbContext.Reponses
                .Include(b => b.Question)
                .ToListAsync();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Reponse>> GetReponse(int id)
        {
            var result = await _dbContext.Reponses.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> AddReponse(Reponse reponsetoAdd)
        {
            try
            {
                var ExitedQuestion = await _dbContext.Questions.FindAsync(reponsetoAdd.QuestionId);
                if (ExitedQuestion== null)
                {
                    return BadRequest();

                }

                _dbContext.Reponses.Add(reponsetoAdd);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(AddReponse), reponsetoAdd);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
