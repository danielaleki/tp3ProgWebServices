using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //je vais ajouter les roles
    public class VoyagesController : ControllerBase
    {
        private readonly WebApplication1Context _context;

        public VoyagesController(WebApplication1Context context)
        {
            _context = context;
        }

        // GET: api/Voyages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Voyage>>> GetAllVoyage()
        {
          if (_context.Voyages == null)
          {
              return NotFound();
          }
            return await _context.Voyages.ToListAsync();
        }

        // GET: api/Voyages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Voyage>> GetVoyage(int id)
        {
          if (_context.Voyages == null)
          {
              return NotFound();
          }
            var voyage = await _context.Voyages.FindAsync(id);

            if (voyage == null)
            {
                return NotFound();
            }

            return voyage;
        }

        // PUT: api/Voyages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoyage(int id, Voyage voyage)
        {
            if (id != voyage.Id)
            {
                return BadRequest();
            }

            _context.Entry(voyage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoyageExists(id))
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

        // POST: api/Voyages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Voyage>> PostVoyage(Voyage voyage)
        {
          if (_context.Voyages == null)
          {
              return Problem("Entity set 'WebApplication1Context.Voyage'  is null.");
          }
            _context.Voyages.Add(voyage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVoyage", new { id = voyage.Id }, voyage);
        }

        // DELETE: api/Voyages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoyage(int id)
        {
            if (_context.Voyages == null)
            {
                return NotFound();
            }
            var voyage = await _context.Voyages.FindAsync(id);
            if (voyage == null)
            {
                return NotFound();
            }

            _context.Voyages.Remove(voyage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VoyageExists(int id)
        {
            return (_context.Voyages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
