using System.ComponentModel.DataAnnotations;

namespace Shared.Models;

public class Sistemas
{
    [Key]
    public int SistemaId { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    public string? NombreSistema { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    public string? DescripcionSistema { get; set; }
}
