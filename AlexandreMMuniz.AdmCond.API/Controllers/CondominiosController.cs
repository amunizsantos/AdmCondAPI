using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlexandreMMuniz.AdmCond.API.Models.AlexandreMMunizAdmCondSQLDB;

namespace AlexandreMMuniz.AdmCond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CondominiosController : ControllerBase
    {
        private readonly AlexandreMMunizAdmCondContext _context;

        public CondominiosController(AlexandreMMunizAdmCondContext context)
        {
            _context = context;
        }

        // GET: api/Condominios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Condominios>>> GetCondominios()
        {
            return await _context.Condominios.ToListAsync();
        }

        // GET: api/Condominios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Condominios>> GetCondominios(int id)
        {
            var condominios = await _context.Condominios.FindAsync(id);

            if (condominios == null)
            {
                return NotFound();
            }

            return condominios;
        }

        // PUT: api/Condominios/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCondominios(int id, Condominios condominios)
        {
            if (id != condominios.Id)
            {
                return BadRequest();
            }

            _context.Entry(condominios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CondominiosExists(id))
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

        // POST: api/Condominios
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Condominios>> PostCondominios(Condominios condominios)
        {
            _context.Condominios.Add(condominios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCondominios", new { id = condominios.Id }, condominios);
        }

        // DELETE: api/Condominios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Condominios>> DeleteCondominios(int id)
        {
            var condominios = await _context.Condominios.FindAsync(id);
            if (condominios == null)
            {
                return NotFound();
            }

            _context.Condominios.Remove(condominios);
            await _context.SaveChangesAsync();

            return condominios;
        }

        private bool CondominiosExists(int id)
        {
            return _context.Condominios.Any(e => e.Id == id);
        }
    }
}
