using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models;

public class TicketsDetalle
{
    [Key]
    public int DetalleId { get; set; }

    [ForeignKey("Tickets")]
    public int TicketId { get; set; }

    [Required(ErrorMessage = "Campo obligatorio")]
    [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Este campo no admite letras")]
    public string? Emisor { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    public string? Mensaje { get; set; }
}
