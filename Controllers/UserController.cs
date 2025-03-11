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
    public class UserController : ControllerBase
    {
        //dependancy injection of database 
        private AppDbContext _dbContext; 
        public UserController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<User>> GetUsers()
        {
            var result = await _dbContext.Users.ToListAsync();
            if(result==null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var result = await _dbContext.Users.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> AddUser(User usertoAdd)
        {
            _dbContext.Users.Add(usertoAdd);
            await _dbContext.SaveChangesAsync();
            //if (result == null)
            //{
            //    return NotFound();
            //}
            return CreatedAtAction(nameof(AddUser),usertoAdd);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, User user)
        {
            var ExistedUser = await _dbContext.Users.FindAsync(id);
            if (ExistedUser==null)
            {
                return BadRequest();
            }
            //_dbContext.Entry(user).State = EntityState.Modified;
            if (user==null)
            {
                return BadRequest();
            }
            if (user.nom != null)
            {
                ExistedUser.nom = user.nom;
            }
            if (user.Email != null)
            {
                ExistedUser.Email = user.Email;
            }
            if (user.mdp != null)
            {
                ExistedUser.mdp = user.mdp;
            }
            if (user.Role != null)
            {
                ExistedUser.Role = user.Role;
            }
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

    }
}
