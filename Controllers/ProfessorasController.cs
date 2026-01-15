using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEscolar.Data;
using SistemaEscolar.Models;
namespace SistemaEscolar.Controllers
{
    public class ProfessorasController : Controller
    {
        private readonly SistemaEscolarContext _context;

        public ProfessorasController(SistemaEscolarContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Professoras.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Professora professora)
        {
            if (!ModelState.IsValid)
                return View(professora);

            _context.Professoras.Add(professora);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var professora = _context.Professoras.Find(id);
            if (professora == null) return NotFound();
            return View(professora);
        }

        [HttpPost]
        public IActionResult Edit(Professora professora)
        {
            if (!ModelState.IsValid)
                return View(professora);

            _context.Update(professora);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}