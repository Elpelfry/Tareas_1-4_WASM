namespace TareasToWASM.API.ViewModels.Response;

public class TicketsDetalleResponse
{
    public int DetalleId { get; set; }
    public int TicketId { get; set; }
    public string? Emisor { get; set; }
    public string? Mensaje { get; set; }
}
