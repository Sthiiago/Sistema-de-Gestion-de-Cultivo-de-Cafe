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
    public class ParcelasController : Controller
    {
        private readonly SistemaparcelasContext _context;

        public ParcelasController(SistemaparcelasContext context)
        {
            _context = context;
        }

        // GET: Parcelas
        public async Task<IActionResult> Index()
        {
              return _context.Parcelas != null ? 
                          View(await _context.Parcelas.ToListAsync()) :
                          Problem("Entity set 'SistemaparcelasContext.Parcelas'  is null.");
        }

        // GET: Parcelas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Parcelas == null)
            {
                return NotFound();
            }

            var parcela = await _context.Parcelas
                .FirstOrDefaultAsync(m => m.IdParcela == id);
            if (parcela == null)
            {
                return NotFound();
            }

            return View(parcela);
        }

        // GET: Parcelas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parcelas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdParcela,CoordenadasGps,VariedadCafe,MetodoCultivo,SuperficieParcela")] Parcela parcela)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parcela);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parcela);
        }

        // GET: Parcelas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Parcelas == null)
            {
                return NotFound();
            }

            var parcela = await _context.Parcelas.FindAsync(id);
            if (parcela == null)
            {
                return NotFound();
            }
            return View(parcela);
        }

        // POST: Parcelas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdParcela,CoordenadasGps,VariedadCafe,MetodoCultivo,SuperficieParcela")] Parcela parcela)
        {
            if (id != parcela.IdParcela)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parcela);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParcelaExists(parcela.IdParcela))
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
            return View(parcela);
        }

        // GET: Parcelas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Parcelas == null)
            {
                return NotFound();
            }

            var parcela = await _context.Parcelas
                .FirstOrDefaultAsync(m => m.IdParcela == id);
            if (parcela == null)
            {
                return NotFound();
            }

            return View(parcela);
        }

        // POST: Parcelas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Parcelas == null)
            {
                return Problem("Entity set 'SistemaparcelasContext.Parcelas'  is null.");
            }
            var parcela = await _context.Parcelas.FindAsync(id);
            if (parcela != null)
            {
                _context.Parcelas.Remove(parcela);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParcelaExists(int id)
        {
          return (_context.Parcelas?.Any(e => e.IdParcela == id)).GetValueOrDefault();
        }
    }
}
