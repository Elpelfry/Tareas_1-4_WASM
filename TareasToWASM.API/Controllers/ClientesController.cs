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
    public class ClientesController : ControllerBase
    {
        private readonly Contexto _context;

        public ClientesController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientesResponse>>> GetClientes()
        {
            var clientes = await _context.Clientes.ToListAsync();
            var clientesR = clientes.Select(c => new ClientesResponse
            {
                ClienteId = c.ClienteId,
                Nombre = c.Nombre,
                Telefono = c.Telefono,
                Celular = c.Celular,
                RNC = c.RNC,
                Email = c.Email,
                Direccion = c.Direccion
            }).ToList();

            return clientesR;
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientesResponse>> GetClientes(int id)
        {
            var clientes = await _context.Clientes.FindAsync(id);

            if (clientes == null)
            {
                return NotFound();
            }

            var clientesR = new ClientesResponse
            {
                ClienteId = clientes.ClienteId,
                Nombre = clientes.Nombre,
                Telefono = clientes.Telefono,
                Celular = clientes.Celular,
                RNC = clientes.RNC,
                Email = clientes.Email,
                Direccion = clientes.Direccion
            };

            return clientesR;
        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientesResponse>> PostClientes(ClientesRequest clientesR)
        {
            var clientes = new Clientes
            {
                ClienteId = 0,
                Nombre = clientesR.Nombre,
                Telefono = clientesR.Telefono,
                Celular = clientesR.Celular,
                RNC = clientesR.RNC,
                Email = clientesR.Email,
                Direccion = clientesR.Direccion
            };

            _context.Clientes.Add(clientes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientes", new { id = clientes.ClienteId }, clientes);
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientes(int id, ClientesRequest clientesR)
        {
            var clientes = new Clientes
            {
                Nombre = clientesR.Nombre,
                Telefono = clientesR.Telefono,
                Celular = clientesR.Celular,
                RNC = clientesR.RNC,
                Email = clientesR.Email,
                Direccion = clientesR.Direccion
            };
            _context.Entry(clientes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesExists(id))
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

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientes(int id)
        {
            var clientes = await _context.Clientes.FindAsync(id);
            if (clientes == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(clientes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientesExists(int id)
        {
            return _context.Clientes.Any(e => e.ClienteId == id);
        }
    }
}
