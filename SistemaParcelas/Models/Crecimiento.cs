using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaParcelas.Models;

public partial class Crecimiento
{

    public int IdRegistro { get; set; }
    [Display(Name = "Id de la parcela")]
    public int? IdParcela { get; set; }
    [Display(Name = "Fecha de siembra")]
    public DateTime? FechaSiembra { get; set; }
    [Display(Name = "¿Tiene plagas?")]
    public bool? RegistroPlagasEnfermedades { get; set; }

    public string? Observaciones { get; set; }
    [Display(Name = "Parcela asociada")]
    public virtual Parcela? IdParcelaNavigation { get; set; }
}
