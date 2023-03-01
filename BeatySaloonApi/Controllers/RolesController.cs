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
    public class RolesController : ControllerBase
    {
        private readonly BeautySaloonBaseContext _context;

        public RolesController(BeautySaloonBaseContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        [SwaggerOperation(Summary = "Вывод ролей", Description = "Полное описание")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        // GET: api/Roles/5
        [HttpGet("{Id}")]
        [SwaggerOperation(Summary = "Вывод ролей по идентификатору", Description = "Полное описание")]
        public async Task<ActionResult<Role>> GetRole(int Id)
        {
            var role = await _context.Roles.FindAsync(Id);

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkId=2123754
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutRole(int Id, Role role)
        {
            if (Id != role.Id)
            {
                return BadRequest();
            }

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(Id))
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

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkId=2123754
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRole", new { Id = role.Id }, role);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteRole(int Id)
        {
            var role = await _context.Roles.FindAsync(Id);
            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleExists(int Id)
        {
            return _context.Roles.Any(e => e.Id == Id);
        }
    }
}
