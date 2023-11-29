using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Tiendaweb.Data;
using Tiendaweb.Models;

namespace Tiendaweb.Controllers
{
    [Area("cliente")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context; 
        }

        public async Task<IActionResult> Index()
        {
            var cervezas = _context.cervezas.Include(c=>c.estilo);
            return View(await cervezas.ToListAsync());
        }
        public async Task<IActionResult> Detalles(int? id)
        {
            var cerveza =await _context.cervezas.Include(c => c.estilo).FirstOrDefaultAsync(c=> c.id==id);
            return View( cerveza);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}