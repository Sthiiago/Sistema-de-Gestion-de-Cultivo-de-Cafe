using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaParcelas.Models;

public partial class Cosecha
{
    public int IdCosechas { get; set; }
    [Display(Name = "Id de la parcela")]
    public int? IdParcela { get; set; }
    [Display(Name = "Fecha de la cosecha")]
    public DateTime? FechaCosecha { get; set; }
    [Display(Name = "Cantidad de café recolectado")]
    public int? CantidadCafeRecolectado { get; set; }
    [Display(Name = "Método de procesamiento")]
    public string? MetodoProcesamiento { get; set; }
    [Display(Name = "Fecha de procesamiento")]
    public DateTime? FechaProcesamiento { get; set; }
    [Display(Name = "Observaciones")]
    public string? Observaciones { get; set; }
    [Display(Name = "Parcela asociada")]
    public virtual Parcela? IdParcelaNavigation { get; set; }
}
