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
    public class TestController : ControllerBase
    {
        private AppDbContext _dbContext;
        public TestController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<Test>> GetTests()
        {
            var result = await _dbContext.Tests.ToListAsync();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Test>> GetTest(int id)
        {
            var result = await _dbContext.Tests.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> AddTest(Test testtoAdd)
        {
            _dbContext.Tests.Add(testtoAdd);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(AddTest), testtoAdd);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTest(int id,Test test)
        {
           var  ExistedTest = await _dbContext.Tests.FindAsync(id);

            if (ExistedTest==null)
            {
                return BadRequest();
            }
            //_dbContext.Entry(test).State = EntityState.Modified;
            if(test==null)
            {
                return BadRequest();
            }
            if (test.Titre != null)
            {
                ExistedTest.Titre = test.Titre;
            }
            if (test.Duree > 0)
            {
                ExistedTest.Duree = test.Duree;
            }
            if (test.Description != null)
            {
                ExistedTest.Description = test.Description;
            }
            if (test.Type != null)
            {
                ExistedTest.Type = test.Type;
            }
            if (test.Etat != null)
            {
                ExistedTest.Etat = test.Etat;
            }
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

    }




    }

