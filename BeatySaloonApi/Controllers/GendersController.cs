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
    public class GendersController : ControllerBase
    {
        private readonly BeautySaloonBaseContext _context;

        public GendersController(BeautySaloonBaseContext context)
        {
            _context = context;
        }

        // GET: api/Genders
        [HttpGet]
        [SwaggerOperation(Summary = "Вывод гендера", Description = "Полное описание")]
        public async Task<ActionResult<IEnumerable<Gender>>> GetGenders()
        {
            return await _context.Genders.ToListAsync();
        }

        // GET: api/Genders/5
        [HttpGet("{UserLogin}")]
        [SwaggerOperation(Summary = "Вывод гендера по идентификатору", Description = "Полное описание")]
        public async Task<ActionResult<Gender>> GetGender(string UserLogin)
        {
            var gender = await _context.Genders.FindAsync(UserLogin);

            if (gender == null)
            {
                return NotFound();
            }

            return gender;
        }

        // PUT: api/Genders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkUserLogin=2123754
        [HttpPut("{UserLogin}")]
        public async Task<IActionResult> PutGender(string UserLogin, Gender gender)
        {
            if (UserLogin != gender.Code)
            {
                return BadRequest();
            }

            _context.Entry(gender).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenderExists(UserLogin))
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

        // POST: api/Genders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkUserLogin=2123754
        [HttpPost]
        public async Task<ActionResult<Gender>> PostGender(Gender gender)
        {
            _context.Genders.Add(gender);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GenderExists(gender.Code))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGender", new { UserLogin = gender.Code }, gender);
        }

        // DELETE: api/Genders/5
        [HttpDelete("{UserLogin}")]
        public async Task<IActionResult> DeleteGender(string UserLogin)
        {
            var gender = await _context.Genders.FindAsync(UserLogin);
            if (gender == null)
            {
                return NotFound();
            }

            _context.Genders.Remove(gender);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GenderExists(string UserLogin)
        {
            return _context.Genders.Any(e => e.Code == UserLogin);
        }
    }
}
