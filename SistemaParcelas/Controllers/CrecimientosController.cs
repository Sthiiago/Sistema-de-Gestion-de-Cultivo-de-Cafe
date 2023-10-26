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
    public class CrecimientosController : Controller
    {
        private readonly SistemaparcelasContext _context;

        public CrecimientosController(SistemaparcelasContext context)
        {
            _context = context;
        }

        // GET: Crecimientos
        public async Task<IActionResult> Index()
        {
            var sistemaparcelasContext = _context.Crecimientos.Include(c => c.IdParcelaNavigation);
            return View(await sistemaparcelasContext.ToListAsync());
        }

        // GET: Crecimientos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Crecimientos == null)
            {
                return NotFound();
            }

            var crecimiento = await _context.Crecimientos
                .Include(c => c.IdParcelaNavigation)
                .FirstOrDefaultAsync(m => m.IdRegistro == id);
            if (crecimiento == null)
            {
                return NotFound();
            }

            return View(crecimiento);
        }

        // GET: Crecimientos/Create
        public IActionResult Create()
        {
            ViewData["IdParcela"] = new SelectList(_context.Parcelas, "IdParcela", "IdParcela");
            return View();
        }

        // POST: Crecimientos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRegistro,IdParcela,FechaSiembra,RegistroPlagasEnfermedades,Observaciones")] Crecimiento crecimiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(crecimiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdParcela"] = new SelectList(_context.Parcelas, "IdParcela", "IdParcela", crecimiento.IdParcela);
            return View(crecimiento);
        }

        // GET: Crecimientos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Crecimientos == null)
            {
                return NotFound();
            }

            var crecimiento = await _context.Crecimientos.FindAsync(id);
            if (crecimiento == null)
            {
                return NotFound();
            }
            ViewData["IdParcela"] = new SelectList(_context.Parcelas, "IdParcela", "IdParcela", crecimiento.IdParcela);
            return View(crecimiento);
        }

        // POST: Crecimientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRegistro,IdParcela,FechaSiembra,RegistroPlagasEnfermedades,Observaciones")] Crecimiento crecimiento)
        {
            if (id != crecimiento.IdRegistro)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(crecimiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CrecimientoExists(crecimiento.IdRegistro))
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
            ViewData["IdParcela"] = new SelectList(_context.Parcelas, "IdParcela", "IdParcela", crecimiento.IdParcela);
            return View(crecimiento);
        }

        // GET: Crecimientos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Crecimientos == null)
            {
                return NotFound();
            }

            var crecimiento = await _context.Crecimientos
                .Include(c => c.IdParcelaNavigation)
                .FirstOrDefaultAsync(m => m.IdRegistro == id);
            if (crecimiento == null)
            {
                return NotFound();
            }

            return View(crecimiento);
        }

        // POST: Crecimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Crecimientos == null)
            {
                return Problem("Entity set 'SistemaparcelasContext.Crecimientos'  is null.");
            }
            var crecimiento = await _context.Crecimientos.FindAsync(id);
            if (crecimiento != null)
            {
                _context.Crecimientos.Remove(crecimiento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CrecimientoExists(int id)
        {
          return (_context.Crecimientos?.Any(e => e.IdRegistro == id)).GetValueOrDefault();
        }
    }
}
