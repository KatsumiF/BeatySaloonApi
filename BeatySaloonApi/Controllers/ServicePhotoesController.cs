using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeatySaloonApi.Models;

namespace BeatySaloonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicePhotoesController : ControllerBase
    {
        private readonly BeautySaloonBaseContext _context;

        public ServicePhotoesController(BeautySaloonBaseContext context)
        {
            _context = context;
        }

        // GET: api/ServicePhotoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicePhoto>>> GetServicePhotos()
        {
            return await _context.ServicePhotos.ToListAsync();
        }

        // GET: api/ServicePhotoes/5
        [HttpGet("{Id}")]
        public async Task<ActionResult<ServicePhoto>> GetServicePhoto(int Id)
        {
            var servicePhoto = await _context.ServicePhotos.FindAsync(Id);

            if (servicePhoto == null)
            {
                return NotFound();
            }

            return servicePhoto;
        }

        // PUT: api/ServicePhotoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkId=2123754
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutServicePhoto(int Id, ServicePhoto servicePhoto)
        {
            if (Id != servicePhoto.Id)
            {
                return BadRequest();
            }

            _context.Entry(servicePhoto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicePhotoExists(Id))
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

        // POST: api/ServicePhotoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkId=2123754
        [HttpPost]
        public async Task<ActionResult<ServicePhoto>> PostServicePhoto(ServicePhoto servicePhoto)
        {
            _context.ServicePhotos.Add(servicePhoto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServicePhoto", new { Id = servicePhoto.Id }, servicePhoto);
        }

        // DELETE: api/ServicePhotoes/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteServicePhoto(int Id)
        {
            var servicePhoto = await _context.ServicePhotos.FindAsync(Id);
            if (servicePhoto == null)
            {
                return NotFound();
            }

            _context.ServicePhotos.Remove(servicePhoto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServicePhotoExists(int Id)
        {
            return _context.ServicePhotos.Any(e => e.Id == Id);
        }
    }
}
