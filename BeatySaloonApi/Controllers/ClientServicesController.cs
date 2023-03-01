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
    public class ClientServicesController : ControllerBase
    {
        private readonly BeautySaloonBaseContext _context;

        public ClientServicesController(BeautySaloonBaseContext context)
        {
            _context = context;
        }

        // GET: api/ClientServices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientService>>> GetClientServices()
        {
            return await _context.ClientServices.ToListAsync();
        }

        // GET: api/ClientServices/5
        [HttpGet("{Id}")]
        public async Task<ActionResult<ClientService>> GetClientService(int Id)
        {
            var clientService = await _context.ClientServices.FindAsync(Id);

            if (clientService == null)
            {
                return NotFound();
            }

            return clientService;
        }

        // PUT: api/ClientServices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkId=2123754
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutClientService(int Id, ClientService clientService)
        {
            if (Id != clientService.Id)
            {
                return BadRequest();
            }

            _context.Entry(clientService).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientServiceExists(Id))
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

        // POST: api/ClientServices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkId=2123754
        [HttpPost]
        public async Task<ActionResult<ClientService>> PostClientService(ClientService clientService)
        {
            _context.ClientServices.Add(clientService);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientService", new { Id = clientService.Id }, clientService);
        }

        // DELETE: api/ClientServices/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteClientService(int Id)
        {
            var clientService = await _context.ClientServices.FindAsync(Id);
            if (clientService == null)
            {
                return NotFound();
            }

            _context.ClientServices.Remove(clientService);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientServiceExists(int Id)
        {
            return _context.ClientServices.Any(e => e.Id == Id);
        }
    }
}
