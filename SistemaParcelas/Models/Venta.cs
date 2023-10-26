using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaParcelas.Models;

public partial class Venta
{
    public int IdVenta { get; set; }
    [Display(Name = "Id de la parcela")]
    public int? IdParcela { get; set; }
    [Display(Name = "Fecha de la venta")]
    public DateTime? FechaVenta { get; set; }
    [Display(Name = "Precio por unidad")]
    public int? PrecioUnidad { get; set; }
    [Display(Name = "Cantidad de unidades")]
    public int? CantidadUnidades { get; set; }
    [Display(Name = "Cliente")]
    public int? IdCliente { get; set; }
    [Display(Name = "Fecha de entrega")]
    public DateTime? FechaEntrega { get; set; }
    [Display(Name = "Observaciones")]
    public string? Observaciones { get; set; }
    [Display(Name = "Cliente de la venta")]
    public virtual Cliente? IdClienteNavigation { get; set; }
    [Display(Name = "Id de la parcela")]
    public virtual Parcela? IdParcelaNavigation { get; set; }
}
