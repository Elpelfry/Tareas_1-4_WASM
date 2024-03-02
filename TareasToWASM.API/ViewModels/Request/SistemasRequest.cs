using System.ComponentModel.DataAnnotations;

namespace TareasToWASM.API.ViewModels.Request;

public class SistemasRequest
{
    [Required(ErrorMessage = "Campo Obligatorio")]
    public string? NombreSistema { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    public string? DescripcionSistema { get; set; }
}
