using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaParcelas.Models;

namespace SistemaParcelas.Controllers
{
    [Authorize]
    public class CosechasController : Controller
    {
        private readonly SistemaparcelasContext _context;

        public CosechasController(SistemaparcelasContext context)
        {
            _context = context;
        }

        // GET: Cosechas
        public async Task<IActionResult> Index()
        {
            var sistemaparcelasContext = _context.Cosechas.Include(c => c.IdParcelaNavigation);
            return View(await sistemaparcelasContext.ToListAsync());
        }

        // GET: Cosechas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cosechas == null)
            {
                return NotFound();
            }

            var cosecha = await _context.Cosechas
                .Include(c => c.IdParcelaNavigation)
                .FirstOrDefaultAsync(m => m.IdCosechas == id);
            if (cosecha == null)
            {
                return NotFound();
            }

            return View(cosecha);
        }

        // GET: Cosechas/Create
        public IActionResult Create()
        {
            ViewData["IdParcela"] = new SelectList(_context.Parcelas, "IdParcela", "IdParcela");
            return View();
        }

        // POST: Cosechas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCosechas,IdParcela,FechaCosecha,CantidadCafeRecolectado,MetodoProcesamiento,FechaProcesamiento,Observaciones")] Cosecha cosecha)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cosecha);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdParcela"] = new SelectList(_context.Parcelas, "IdParcela", "IdParcela", cosecha.IdParcela);
            return View(cosecha);
        }

        // GET: Cosechas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cosechas == null)
            {
                return NotFound();
            }

            var cosecha = await _context.Cosechas.FindAsync(id);
            if (cosecha == null)
            {
                return NotFound();
            }
            ViewData["IdParcela"] = new SelectList(_context.Parcelas, "IdParcela", "IdParcela", cosecha.IdParcela);
            return View(cosecha);
        }

        // POST: Cosechas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCosechas,IdParcela,FechaCosecha,CantidadCafeRecolectado,MetodoProcesamiento,FechaProcesamiento,Observaciones")] Cosecha cosecha)
        {
            if (id != cosecha.IdCosechas)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cosecha);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CosechaExists(cosecha.IdCosechas))
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
            ViewData["IdParcela"] = new SelectList(_context.Parcelas, "IdParcela", "IdParcela", cosecha.IdParcela);
            return View(cosecha);
        }

        // GET: Cosechas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cosechas == null)
            {
                return NotFound();
            }

            var cosecha = await _context.Cosechas
                .Include(c => c.IdParcelaNavigation)
                .FirstOrDefaultAsync(m => m.IdCosechas == id);
            if (cosecha == null)
            {
                return NotFound();
            }

            return View(cosecha);
        }

        // POST: Cosechas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cosechas == null)
            {
                return Problem("Entity set 'SistemaparcelasContext.Cosechas'  is null.");
            }
            var cosecha = await _context.Cosechas.FindAsync(id);
            if (cosecha != null)
            {
                _context.Cosechas.Remove(cosecha);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CosechaExists(int id)
        {
          return (_context.Cosechas?.Any(e => e.IdCosechas == id)).GetValueOrDefault();
        }
    }
}
