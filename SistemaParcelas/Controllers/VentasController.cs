using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaParcelas.Models;
using SistemaParcelas.Models.VentasCharts;

namespace SistemaParcelas.Controllers
{
    [Authorize]
    public class VentasController : Controller
    {
        private readonly SistemaparcelasContext _context;

        public VentasController(SistemaparcelasContext context)
        {
            _context = context;
        }

        public IActionResult VentasCharts()
        {
            return View();
        }

        public IActionResult VentasParcela()
        {
            var ventas = _context.Ventas.ToList();

            List<VentasParcela> listVentas = ventas.GroupBy(v => v.IdParcela)
                .Select(group => new VentasParcela
                {
                    ID_Parcela = group.Key,
                    VentaTotal = group.Sum(v => v.CantidadUnidades * v.PrecioUnidad)
                }).ToList();


            return StatusCode(StatusCodes.Status200OK, listVentas);
        }

        public IActionResult VentasFecha(int Parcela)
        {
            var ventas = _context.Ventas.Where(venta => venta.IdParcela == Parcela).ToList();

            List<VentasFecha> listVentas = ventas.Select(venta => new VentasFecha
            {
                ID_Parcela = venta.IdParcela,
                FechaVenta = venta.FechaVenta,
                VentaTotal = (venta.CantidadUnidades * venta.PrecioUnidad)
            }).ToList();


            return StatusCode(StatusCodes.Status200OK, listVentas);
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var sistemaparcelasContext = _context.Ventas.Include(v => v.IdClienteNavigation).Include(v => v.IdParcelaNavigation);
            return View(await sistemaparcelasContext.ToListAsync());
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.IdClienteNavigation)
                .Include(v => v.IdParcelaNavigation)
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            var clientes = _context.Clientes.ToList();
            // Crea una lista de SelectListItem con IdCliente como valor y NombreCliente como texto
            var clientesSelectList = clientes
                .Select(cliente => new SelectListItem
                {
                    Value = cliente.IdCliente.ToString(),
                    Text = cliente.Nombre
                })
                .ToList();

            // Coloca la lista en el ViewBag
            ViewBag.Clientes = clientesSelectList;

            ViewData["IdParcela"] = new SelectList(_context.Parcelas, "IdParcela", "IdParcela");
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVenta,IdParcela,FechaVenta,PrecioUnidad,CantidadUnidades,IdCliente,FechaEntrega,Observaciones")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var clientes = _context.Clientes.ToList();
            // Crea una lista de SelectListItem con IdCliente como valor y NombreCliente como texto
            var clientesSelectList = clientes
                .Select(cliente => new SelectListItem
                {
                    Value = cliente.IdCliente.ToString(),
                    Text = cliente.Nombre
                })
                .ToList();

            // Coloca la lista en el ViewBag
            ViewBag.Clientes = clientesSelectList;
            ViewData["IdParcela"] = new SelectList(_context.Parcelas, "IdParcela", "IdParcela", venta.IdParcela);
            return View(venta);
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            var clientes = _context.Clientes.ToList();
            // Crea una lista de SelectListItem con IdCliente como valor y NombreCliente como texto
            var clientesSelectList = clientes
                .Select(cliente => new SelectListItem
                {
                    Value = cliente.IdCliente.ToString(),
                    Text = cliente.Nombre
                })
                .ToList();

            // Coloca la lista en el ViewBag
            ViewBag.Clientes = clientesSelectList;
            ViewData["IdParcela"] = new SelectList(_context.Parcelas, "IdParcela", "IdParcela", venta.IdParcela);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVenta,IdParcela,FechaVenta,PrecioUnidad,CantidadUnidades,IdCliente,FechaEntrega,Observaciones")] Venta venta)
        {
            if (id != venta.IdVenta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.IdVenta))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var clientes = _context.Clientes.ToList();
            // Crea una lista de SelectListItem con IdCliente como valor y NombreCliente como texto
            var clientesSelectList = clientes
                .Select(cliente => new SelectListItem
                {
                    Value = cliente.IdCliente.ToString(),
                    Text = cliente.Nombre
                })
                .ToList();

            // Coloca la lista en el ViewBag
            ViewBag.Clientes = clientesSelectList;
            ViewData["IdParcela"] = new SelectList(_context.Parcelas, "IdParcela", "IdParcela", venta.IdParcela);
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.IdClienteNavigation)
                .Include(v => v.IdParcelaNavigation)
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ventas == null)
            {
                return Problem("Entity set 'SistemaparcelasContext.Ventas'  is null.");
            }
            var venta = await _context.Ventas.FindAsync(id);
            if (venta != null)
            {
                _context.Ventas.Remove(venta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(int id)
        {
          return (_context.Ventas?.Any(e => e.IdVenta == id)).GetValueOrDefault();
        }
    }
}
