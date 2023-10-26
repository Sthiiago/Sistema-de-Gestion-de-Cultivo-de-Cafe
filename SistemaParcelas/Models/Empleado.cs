using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaParcelas.Models;

public partial class Empleado
{
    [Display(Name = "Id del empleado")]
    public int IdEmpleado { get; set; }
    [Display(Name = "Id de la parcela")]
    public int? IdParcela { get; set; }
    [Display(Name = "Nombre del empleado")]
    public string? Nombre { get; set; }
    [Display(Name = "Cargo del empleado")]
    public string? Cargo { get; set; }
    [Display(Name = "Fecha de contratación")]
    public DateTime? FechaContratacion { get; set; }
    [Display(Name = "Tareas asignadas")]
    public string? TareasAsignadas { get; set; }

    public string? Contacto { get; set; }
    [Display(Name = "Horario laboral")]
    public string? HorarioLaboral { get; set; }
    [Display(Name = "Parcela a la que pertenece")]
    public virtual Parcela? IdParcelaNavigation { get; set; }
}
