using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaParcelas.Models;

public partial class Parcela
{

    public int IdParcela { get; set; }
    [Display(Name = "Coordenas de GPS")]
    public string? CoordenadasGps { get; set; }
    [Display(Name = "Variedad de café")]
    public string? VariedadCafe { get; set; }
    [Display(Name = "Método de cúltivo")]
    public string? MetodoCultivo { get; set; }
    [Display(Name = "Superficie de la Parcela")]
    public string? SuperficieParcela { get; set; }

    public virtual ICollection<Cosecha> Cosechas { get; set; } = new List<Cosecha>();

    public virtual ICollection<Crecimiento> Crecimientos { get; set; } = new List<Crecimiento>();

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
