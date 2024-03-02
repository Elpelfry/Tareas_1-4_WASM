using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class SistemasController : ControllerBase
    {
        private readonly Contexto _context;

        public SistemasController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Sistemas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SistemasResponse>>> GetSistemas()
        {
            var sistemas = await _context.Sistemas.ToListAsync();
            var sistemasR = sistemas.Select(s => new SistemasResponse
            {
                SistemaId = s.SistemaId,
                NombreSistema = s.NombreSistema,
                DescripcionSistema = s.DescripcionSistema
            }).ToList();
            return sistemasR;
        }

        // GET: api/Sistemas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SistemasResponse>> GetSistemas(int id)
        {
            var sistemas = await _context.Sistemas.FindAsync(id);

            if (sistemas == null)
            {
                return NotFound();
            }
            var sistemasR = new SistemasResponse
            {
                SistemaId = sistemas.SistemaId,
                NombreSistema = sistemas.NombreSistema,
                DescripcionSistema = sistemas.DescripcionSistema
            };

            return sistemasR;
        }

        // POST: api/Sistemas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SistemasResponse>> PostSistemas(SistemasRequest sistemasR)
        {
            var sistemas = new Sistemas
            {
                SistemaId = 0,
                NombreSistema = sistemasR.NombreSistema,
                DescripcionSistema = sistemasR.DescripcionSistema
            };

            _context.Sistemas.Add(sistemas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSistemas", new { id = sistemas.SistemaId }, sistemas);
        }
        // PUT: api/Sistemas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSistemas(int id, SistemasRequest sistemasR)
        {

            var sistemas = new Sistemas
            {
                SistemaId = id,
                NombreSistema = sistemasR.NombreSistema,
                DescripcionSistema = sistemasR.DescripcionSistema
            };

            _context.Entry(sistemas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SistemasExists(id))
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

        // DELETE: api/Sistemas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSistemas(int id)
        {
            var sistemas = await _context.Sistemas.FindAsync(id);
            if (sistemas == null)
            {
                return NotFound();
            }

            _context.Sistemas.Remove(sistemas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SistemasExists(int id)
        {
            return _context.Sistemas.Any(e => e.SistemaId == id);
        }
    }
}
