using Microsoft.EntityFrameworkCore;
using Shared.Models;
namespace TareasToWASM.API.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) {}

    public DbSet<Tickets> Tickets { get; set; }
    public DbSet<Prioridades> Prioridades { get; set; }
    public DbSet<Clientes> Clientes { get; set; }
    public DbSet<Sistemas> Sistemas { get; set; }
    public DbSet<TicketsDetalle> TicketsDetalle { get; set; }
}
