using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TareasToWASM.API.ViewModels.Request;

public class TicketsRequest
{
    [Required(ErrorMessage = "El campo fecha es obligatorio")]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "Requerido")]
    [ForeignKey("Prioridades")]
    public int PrioridadId { get; set; }

    [Required(ErrorMessage = "Requerido")]
    [ForeignKey("Clientes")]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "Requerido")]
    [ForeignKey("Sistemas")]
    public int SistemaId { get; set; }

    [Required(ErrorMessage = "El campo Solicitado por es obligatorio")]
    [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Solo debe contener letras.")]
    public string? Solicitadopor { get; set; }

    [Required(ErrorMessage = "El campo Asunto es obligatorio")]
    public string? Asunto { get; set; }

    [Required(ErrorMessage = "El campo Descripción es obligatorio")]
    public string? Descripcion { get; set; }

    [ForeignKey("TicketId")]
    public ICollection<TicketsDetalleRequest> TicketsDetalle { get; set; } = new List<TicketsDetalleRequest>();
}
