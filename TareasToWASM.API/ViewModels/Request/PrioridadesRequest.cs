using System.ComponentModel.DataAnnotations;

namespace TareasToWASM.API.ViewModels.Request;

public class PrioridadesRequest
{
    [Required(ErrorMessage = "Este Campo es Obligatorio")]
    public string? Descripcion { get; set; }

    [Range(1, 31, ErrorMessage = "El valor debe estar entre 1 y 31")]
    public int DiasCompromiso { get; set; }
}
