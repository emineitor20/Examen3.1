using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tiendaweb.Data;
using Tiendaweb.Models;

namespace Tiendaweb.Areas.admin.Controllers
{

    [Authorize(Roles = "admin")]
    [Area("admin")]
    public class estiloController : Controller
    {
        private readonly ApplicationDbContext _context;

        public estiloController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: admin/estilo
        public async Task<IActionResult> Index()
        {
              return _context.estilo != null ? 
                          View(await _context.estilo.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.estilo'  is null.");
        }

        // GET: admin/estilo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.estilo == null)
            {
                return NotFound();
            }

            var estilo = await _context.estilo
                .FirstOrDefaultAsync(m => m.id == id);
            if (estilo == null)
            {
                return NotFound();
            }

            return View(estilo);
        }

        // GET: admin/estilo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/estilo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nombre")] estilo estilo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estilo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estilo);
        }

        // GET: admin/estilo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.estilo == null)
            {
                return NotFound();
            }

            var estilo = await _context.estilo.FindAsync(id);
            if (estilo == null)
            {
                return NotFound();
            }
            return View(estilo);
        }

        // POST: admin/estilo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nombre")] estilo estilo)
        {
            if (id != estilo.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estilo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!estiloExists(estilo.id))
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
            return View(estilo);
        }

        // GET: admin/estilo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.estilo == null)
            {
                return NotFound();
            }

            var estilo = await _context.estilo
                .FirstOrDefaultAsync(m => m.id == id);
            if (estilo == null)
            {
                return NotFound();
            }

            return View(estilo);
        }

        // POST: admin/estilo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.estilo == null)
            {
                return Problem("Entity set 'ApplicationDbContext.estilo'  is null.");
            }
            var estilo = await _context.estilo.FindAsync(id);
            if (estilo != null)
            {
                _context.estilo.Remove(estilo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool estiloExists(int id)
        {
          return (_context.estilo?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
