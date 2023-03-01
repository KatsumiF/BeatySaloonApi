using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeatySaloonApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace BeatySaloonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BeautySaloonBaseContext _context;

        public UsersController(BeautySaloonBaseContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        [SwaggerOperation(Summary = "Вывод пользователей", Description = "Полное описание")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
          //  return await _context.Users.Include(x =>x.Clients).ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{UserLogin}")]
        [SwaggerOperation(Summary = "Вывод пользователей по идентификатору", Description = "Полное описание")]
        public async Task<ActionResult<User>> GetUser(string UserLogin)
        {
            var user = await _context.Users.FindAsync(UserLogin);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{UserLogin}")]
        public async Task<IActionResult> PutUser(string UserLogin, User user)
        {
            if (UserLogin != user.UserLogin)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(UserLogin))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.UserLogin))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { UserLogin = user.UserLogin }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{UserLogin}")]
        public async Task<IActionResult> DeleteUser(string UserLogin)
        {
            var user = await _context.Users.FindAsync(UserLogin);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string UserLogin)
        {
            return _context.Users.Any(e => e.UserLogin == UserLogin);
        }
    }
}
