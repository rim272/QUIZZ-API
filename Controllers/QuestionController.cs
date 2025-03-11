using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFE2024_QUIZZ_API.Data;
using PFE2024_QUIZZ_API.models;
using PFE2024_QUIZZ_API.Models;

namespace PFE2024_QUIZZ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private AppDbContext _dbContext;
        public QuestionController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<Question>> GetQuestion()
        {
            var result = await _dbContext.Questions
                .Include(b=>b.Test)
                .ToListAsync();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            var result = await _dbContext.Questions.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> AddQuestion(Question questiontoAdd)
        {
            try
            {
                var ExitedTest = await _dbContext.Tests.FindAsync(questiontoAdd.TestId);
                if(ExitedTest==null)
                {
                    return BadRequest();

                }

                _dbContext.Questions.Add(questiontoAdd);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(AddQuestion), questiontoAdd);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateQuestion(int id, Question question)
        {

           var ExitedQuestion = await _dbContext.Questions.FindAsync(id);

            if (ExitedQuestion==null)
            {
                return BadRequest();
            }

            if (question==null)
            {
                return BadRequest();
            }
            if(question.Type!=null)
            { 
                ExitedQuestion.Type=question.Type;
            }
            if (question.NiveauDifficulte > 0)
            {
                ExitedQuestion.NiveauDifficulte = question.NiveauDifficulte;
            }
            if (question.TestId > 0)
            {
                ExitedQuestion.TestId = question.TestId;
            }

            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
