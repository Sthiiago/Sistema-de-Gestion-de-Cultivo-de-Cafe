using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaParcelas.Models;

public partial class Inventario
{
    public int IdInventario { get; set; }
    [Display(Name = "Id de la parcela")]
    public int? IdParcela { get; set; }
    [Display(Name = "Tipo de café")]
    public string? TipoCafe { get; set; }
    [Display(Name = "Cantidad de café disponible")]
    public int? CantidadCafeDisponible { get; set; }
    [Display(Name = "Calidad del café")]
    public string? CalidadCafe { get; set; }
    [Display(Name = "Parcela asociada")]
    public virtual Parcela? IdParcelaNavigation { get; set; }
}
