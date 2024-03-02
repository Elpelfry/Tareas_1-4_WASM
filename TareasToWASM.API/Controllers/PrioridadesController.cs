using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using TareasToWASM.API.DAL;
using TareasToWASM.API.ViewModels.Request;
using TareasToWASM.API.ViewModels.Response;

namespace TareasToWASM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrioridadesController : ControllerBase
    {
        private readonly Contexto _context;

        public PrioridadesController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Prioridades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrioridadesResponse>>> GetPrioridades()
        {
            var prioridades = await _context.Prioridades.ToListAsync();
            var prioridadesR = prioridades.Select(p => new PrioridadesResponse
            {
                PrioridadId = p.PrioridadId,
                Descripcion = p.Descripcion,
                DiasCompromiso = p.DiasCompromiso
            }).ToList();

            return prioridadesR;
        }

        // GET: api/Prioridades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrioridadesResponse>> GetPrioridades(int id)
        {
            var prioridades = await _context.Prioridades.FindAsync(id);

            if (prioridades == null)
            {
                return NotFound();
            }

            var prioridadesR = new PrioridadesResponse
            {
                PrioridadId = prioridades.PrioridadId,
                Descripcion = prioridades.Descripcion,
                DiasCompromiso = prioridades.DiasCompromiso
            };
            return prioridadesR;
        }

        // POST: api/Prioridades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PrioridadesResponse>> PostPrioridades(PrioridadesRequest prioridadesR)
        {
            var prioridades = new Prioridades
            {
                PrioridadId = 0,
                Descripcion = prioridadesR.Descripcion,
                DiasCompromiso = prioridadesR.DiasCompromiso
            };
            _context.Prioridades.Add(prioridades);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrioridades", new { id = prioridades.PrioridadId }, prioridades);
        }

        // PUT: api/Prioridades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrioridades(int id, PrioridadesRequest prioridadesR)
        {
            var prioridades = new Prioridades
            {
                PrioridadId = id,
                Descripcion = prioridadesR.Descripcion,
                DiasCompromiso = prioridadesR.DiasCompromiso
            };
            _context.Entry(prioridades).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrioridadesExists(id))
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

        // DELETE: api/Prioridades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrioridades(int id)
        {
            var prioridades = await _context.Prioridades.FindAsync(id);
            if (prioridades == null)
            {
                return NotFound();
            }

            _context.Prioridades.Remove(prioridades);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrioridadesExists(int id)
        {
            return _context.Prioridades.Any(e => e.PrioridadId == id);
        }
    }
}
