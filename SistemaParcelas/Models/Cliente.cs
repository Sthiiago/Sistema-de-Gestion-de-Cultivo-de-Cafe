using System;
using System.Collections.Generic;

namespace SistemaParcelas.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public string? Contacto { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
