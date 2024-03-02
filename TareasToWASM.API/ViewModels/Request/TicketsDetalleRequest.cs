using System.ComponentModel.DataAnnotations;

namespace TareasToWASM.API.ViewModels.Request;

public class TicketsDetalleRequest
{
    [Required(ErrorMessage = "Campo obligatorio")]
    [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Este campo no admite letras")]
    public string? Emisor { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    public string? Mensaje { get; set; }
}
