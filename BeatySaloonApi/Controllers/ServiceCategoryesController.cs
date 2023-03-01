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
    public class ServiceCategoryesController : ControllerBase
    {
        private readonly BeautySaloonBaseContext _context;

        public ServiceCategoryesController(BeautySaloonBaseContext context)
        {
            _context = context;
        }

        // GET: api/ServiceCategoryes
        [HttpGet]
        [SwaggerOperation(Summary = "Вывод категорий", Description = "Полное описание")]
        public async Task<ActionResult<IEnumerable<ServiceCategorye>>> GetServiceCategoryes()
        {
            return await _context.ServiceCategoryes.ToListAsync();
        }

        // GET: api/ServiceCategoryes/5
        [HttpGet("{Id}")]
        [SwaggerOperation(Summary = "Вывод категорий по идентификатору", Description = "Полное описание")]
        public async Task<ActionResult<ServiceCategorye>> GetServiceCategorye(int Id)
        {
            var serviceCategorye = await _context.ServiceCategoryes.FindAsync(Id);

            if (serviceCategorye == null)
            {
                return NotFound();
            }

            return serviceCategorye;
        }

        // PUT: api/ServiceCategoryes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkId=2123754
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutServiceCategorye(int Id, ServiceCategorye serviceCategorye)
        {
            if (Id != serviceCategorye.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(serviceCategorye).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceCategoryeExists(Id))
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

        // POST: api/ServiceCategoryes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkId=2123754
        [HttpPost]
        public async Task<ActionResult<ServiceCategorye>> PostServiceCategorye(ServiceCategorye serviceCategorye)
        {
            _context.ServiceCategoryes.Add(serviceCategorye);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServiceCategorye", new { Id = serviceCategorye.CategoryId }, serviceCategorye);
        }

        // DELETE: api/ServiceCategoryes/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteServiceCategorye(int Id)
        {
            var serviceCategorye = await _context.ServiceCategoryes.FindAsync(Id);
            if (serviceCategorye == null)
            {
                return NotFound();
            }

            _context.ServiceCategoryes.Remove(serviceCategorye);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceCategoryeExists(int Id)
        {
            return _context.ServiceCategoryes.Any(e => e.CategoryId == Id);
        }
    }
}
