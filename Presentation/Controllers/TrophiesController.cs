using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Semesterprojekt.Core.Entites;
using Semesterprojekt.Persistence;

namespace Semesterprojekt.API.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrophiesController : ControllerBase
    {
        private readonly GoTrainDbContext _context;

        public TrophiesController(GoTrainDbContext context)
        {
            _context = context;
        }

        // GET: api/Trophies
        [HttpGet]
        public IEnumerable<Trophy> GetTrophies()
        {
            return _context.Trophies;
        }

        // GET: api/Trophies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrophy([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trophy = await _context.Trophies.FindAsync(id);

            if (trophy == null)
            {
                return NotFound();
            }

            return Ok(trophy);
        }

        // PUT: api/Trophies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrophy([FromRoute] int id, [FromBody] Trophy trophy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trophy.Id)
            {
                return BadRequest();
            }

            _context.Entry(trophy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrophyExists(id))
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

        // POST: api/Trophies
        [HttpPost]
        public async Task<IActionResult> PostTrophy([FromBody] Trophy trophy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Trophies.Add(trophy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrophy", new { id = trophy.Id }, trophy);
        }

        // DELETE: api/Trophies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrophy([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trophy = await _context.Trophies.FindAsync(id);
            if (trophy == null)
            {
                return NotFound();
            }

            _context.Trophies.Remove(trophy);
            await _context.SaveChangesAsync();

            return Ok(trophy);
        }

        private bool TrophyExists(int id)
        {
            return _context.Trophies.Any(e => e.Id == id);
        }
    }
}