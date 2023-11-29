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
    public class cervezasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnviroment;
        public cervezasController(ApplicationDbContext context, IWebHostEnvironment hostEnviroment)
        {
            _context = context;
            _hostEnviroment = hostEnviroment;
        }

        // GET: admin/cervezas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.cervezas.Include(c => c.estilo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: admin/cervezas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.cervezas == null)
            {
                return NotFound();
            }

            var cervezas = await _context.cervezas
                .Include(c => c.estilo)
                .FirstOrDefaultAsync(m => m.id == id);
            if (cervezas == null)
            {
                return NotFound();
            }

            return View(cervezas);
        }

        // GET: admin/cervezas/Create
        public IActionResult Create()
        {
            ViewData["idEstilo"] = new SelectList(_context.estilo, "id", "nombre");
            return View();
        }

        // POST: admin/cervezas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nombre,alcohol,idEstilo,precio")] cervezas cervezas)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostEnviroment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                if (archivos.Count>0) { 
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\cervezas");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    using (var fileStream = new FileStream(Path.Combine(subidas, nombreArchivo + extension),FileMode.Create)) 
                    {
                        archivos[0].CopyTo(fileStream);
                    }
                    cervezas.Urlimagen = @"imagenes\cervezas\" + nombreArchivo + extension;

                }
                _context.Add(cervezas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idEstilo"] = new SelectList(_context.estilo, "id", "nombre", cervezas.idEstilo);
            return View(cervezas);
        }

        // GET: admin/cervezas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.cervezas == null)
            {
                return NotFound();
            }

            var cervezas = await _context.cervezas.FindAsync(id);
            if (cervezas == null)
            {
                return NotFound();
            }
            ViewData["idEstilo"] = new SelectList(_context.estilo, "id", "nombre", cervezas.idEstilo);
            return View(cervezas);
        }

        // POST: admin/cervezas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nombre,alcohol,idEstilo,precio,UrlImagen")] cervezas cervezas)
        {
            if (id != cervezas.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string rutaPrincipal = _hostEnviroment.WebRootPath;
                    var archivos = HttpContext.Request.Form.Files;
                    if (archivos.Count > 0)
                    {
                        cervezas? cervezasbd = await _context.cervezas.FindAsync(id);
                        if (cervezasbd != null && cervezasbd.Urlimagen != null) {
                            var rutaImagenActual = Path.Combine(rutaPrincipal, cervezasbd.Urlimagen);
                            if (System.IO.File.Exists(rutaImagenActual)) {
                                System.IO.File.Delete(rutaImagenActual);
                            }
                             
                        }
                        _context.Entry(cervezasbd).State = EntityState.Detached;
                        string nombreArchivo = Guid.NewGuid().ToString();
                        var subidas = Path.Combine(rutaPrincipal, @"imagenes\cervezas");
                        var extension = Path.GetExtension(archivos[0].FileName);
                        using (var fileStream = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                        {
                            archivos[0].CopyTo(fileStream);
                        }
                        cervezas.Urlimagen = @"imagenes\cervezas\" + nombreArchivo + extension;
                      _context.Entry(cervezas).State = EntityState.Modified;

                    }
                        _context.Update(cervezas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!cervezasExists(cervezas.id))
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
            ViewData["idEstilo"] = new SelectList(_context.estilo, "id", "nombre", cervezas.idEstilo);
            return View(cervezas);
        }

        // GET: admin/cervezas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.cervezas == null)
            {
                return NotFound();
            }

            var cervezas = await _context.cervezas
                .Include(c => c.estilo)
                .FirstOrDefaultAsync(m => m.id == id);
            if (cervezas == null)
            {
                return NotFound();
            }

            return View(cervezas);
        }

        // POST: admin/cervezas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.cervezas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.cervezas'  is null.");
            }
            var cervezas = await _context.cervezas.FindAsync(id);
            if (cervezas != null)
            {
                _context.cervezas.Remove(cervezas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool cervezasExists(int id)
        {
          return (_context.cervezas?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
