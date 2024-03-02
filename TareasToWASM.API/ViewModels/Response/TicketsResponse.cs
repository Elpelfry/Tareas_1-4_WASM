namespace TareasToWASM.API.ViewModels.Response;

public class TicketsResponse
{
    public int TicketId { get; set; }
    public DateTime Fecha { get; set; }
    public int PrioridadId { get; set; }
    public int ClienteId { get; set; }
    public int SistemaId { get; set; }
    public string? Solicitadopor { get; set; }
    public string? Asunto { get; set; }
    public string? Descripcion { get; set; }
    public ICollection<TicketsDetalleResponse> TicketsDetalle { get; set; } = new List<TicketsDetalleResponse>();
}
