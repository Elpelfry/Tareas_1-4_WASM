using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
    public class TicketsController : ControllerBase
    {
        private readonly Contexto _context;

        public TicketsController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketsResponse>>> GetTickets()
        {
            var tickets = await _context.Tickets.
                Include(t => t.TicketsDetalle).
                    ToListAsync();

            var ticketsR = tickets.Select(t => new TicketsResponse
            {
                TicketId = t.TicketId,
                Fecha = t.Fecha,
                ClienteId = t.ClienteId,
                PrioridadId = t.PrioridadId,
                SistemaId = t.SistemaId,
                Solicitadopor = t.Solicitadopor,
                Asunto = t.Asunto,
                Descripcion = t.Descripcion,
                TicketsDetalle = t.TicketsDetalle.Select(td => new TicketsDetalleResponse
                {
                    DetalleId = td.DetalleId,
                    TicketId = td.TicketId,
                    Emisor = td.Emisor,
                    Mensaje = td.Mensaje
                }).ToList()
            }).ToList();

            return ticketsR;
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketsResponse>> GetTickets(int id)
        {
            var tickets = await _context.Tickets.
                Include(t => t.TicketsDetalle).
                    FirstOrDefaultAsync(t => t.TicketId == id);

            if (tickets == null)
            {
                return NotFound();
            }
            var ticketsR = new TicketsResponse
            {
                TicketId = tickets.TicketId,
                Fecha = tickets.Fecha,
                ClienteId = tickets.ClienteId,
                PrioridadId = tickets.PrioridadId,
                SistemaId = tickets.SistemaId,
                Solicitadopor = tickets.Solicitadopor,
                Asunto = tickets.Asunto,
                Descripcion = tickets.Descripcion,
                TicketsDetalle = tickets.TicketsDetalle.Select(t => new TicketsDetalleResponse
                {
                    DetalleId = t.DetalleId,
                    TicketId = t.TicketId,
                    Emisor = t.Emisor,
                    Mensaje = t.Mensaje
                }).ToList()
            };

            return ticketsR;
        }

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TicketsResponse>> PostTickets(TicketsRequest ticketsR)
        {
            var tickets = new Tickets
            {
                TicketId = 0,
                Fecha = ticketsR.Fecha,
                ClienteId = ticketsR.ClienteId,
                PrioridadId = ticketsR.PrioridadId,
                SistemaId = ticketsR.SistemaId,
                Solicitadopor = ticketsR.Solicitadopor,
                Asunto = ticketsR.Asunto,
                Descripcion = ticketsR.Descripcion,
                TicketsDetalle = ticketsR.TicketsDetalle.Select(t => new TicketsDetalle
                {
                    Emisor = t.Emisor,
                    Mensaje = t.Mensaje
                }).ToList()
            };

            _context.Tickets.Add(tickets);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTickets", new { id = tickets.TicketId }, tickets);
        }

        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTickets(int id, TicketsRequest ticketsR)
        {
            var tickets = new Tickets
            {
                TicketId = id,
                Fecha = ticketsR.Fecha,
                ClienteId = ticketsR.ClienteId,
                PrioridadId = ticketsR.PrioridadId,
                SistemaId = ticketsR.SistemaId,
                Solicitadopor = ticketsR.Solicitadopor,
                Asunto = ticketsR.Asunto,
                Descripcion = ticketsR.Descripcion,
                TicketsDetalle = ticketsR.TicketsDetalle.Select(t => new TicketsDetalle
                {
                    Emisor = t.Emisor,
                    Mensaje = t.Mensaje
                }).ToList() 
            };
            
            await _context.TicketsDetalle
                .Where(t => t.TicketId == tickets.TicketId)
                    .ExecuteDeleteAsync();

            foreach (var item in tickets.TicketsDetalle)
            {
                item.DetalleId = 0;
                item.TicketId = tickets.TicketId;
                _context.TicketsDetalle.Add(item);
            }
            _context.Entry(tickets).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketsExists(id))
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

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTickets(int id)
        {
            var tickets = await _context.Tickets.FindAsync(id);
            if (tickets == null)
            {
                return NotFound();
            }
            await _context.TicketsDetalle
                .Where(t => t.TicketId == id)
                    .ExecuteDeleteAsync();

            _context.Tickets.Remove(tickets);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketsExists(int id)
        {
            return _context.Tickets.Any(e => e.TicketId == id);
        }
    }
}
