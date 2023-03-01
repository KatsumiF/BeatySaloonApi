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
    public class ServicesController : ControllerBase
    {
        private readonly BeautySaloonBaseContext _context;

        public ServicesController(BeautySaloonBaseContext context)
        {
            _context = context;
        }

        // GET: api/Services
        [HttpGet]
        [SwaggerOperation(Summary = "Вывод сервисов", Description = "Полное описание")]

        public async Task<ActionResult<IEnumerable<Service>>> GetServices()
        {
            return await _context.Services.ToListAsync();
        }

        // GET: api/Services/5
        [HttpGet("{Id}")]
        [SwaggerOperation(Summary = "Вывод сервисов по идентификатору",Description = "Полное описание")]

        public async Task<ActionResult<Service>> GetService(int Id)
        {
            var service = await _context.Services.FindAsync(Id);

            if (service == null)
            {
                return NotFound();
            }

            return service;
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkId=2123754
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutService(int Id, Service service)
        {
            if (Id != service.Id)
            {
                return BadRequest();
            }

            _context.Entry(service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(Id))
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

        // POST: api/Services
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkId=2123754
        [HttpPost]
        public async Task<ActionResult<Service>> PostService(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetService", new { Id = service.Id }, service);
        }

        // DELETE: api/Services/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteService(int Id)
        {
            var service = await _context.Services.FindAsync(Id);
            if (service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceExists(int Id)
        {
            return _context.Services.Any(e => e.Id == Id);
        }
    }
}
