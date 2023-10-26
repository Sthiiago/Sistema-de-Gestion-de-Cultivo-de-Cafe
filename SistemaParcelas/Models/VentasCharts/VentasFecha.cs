using System.ComponentModel.DataAnnotations;

namespace SistemaParcelas.Models.VentasCharts
{
    public class VentasFecha
    {
        public int? ID_Parcela { get; set; }
        public DateTime? FechaVenta { get; set; }
        public int? VentaTotal {  get; set; }
    }
}
