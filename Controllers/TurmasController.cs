using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEscolar.Data;
using SistemaEscolar.Models;
using SistemaEscolar.ViewModels;
namespace SistemaEscolar.Controllers
{
    public class TurmasController : Controller
    {
        private readonly SistemaEscolarContext _context;

        public TurmasController(SistemaEscolarContext context)
        {
            _context = context;
        }

        // LISTAGEM
        public IActionResult Index()
        {
            var turmas = _context.Turmas
                .Include(t => t.Professoras)
                .ToList();

            return View(turmas);
        }

        // CREATE - GET
        public IActionResult Create()
        {
            var vm = new TurmaViewModel
            {
                Professoras = _context.Professoras
                    .Select(p => new ProfessoraItemViewModel
                    {
                        ProfessoraId = p.ProfessoraId,
                        Nome = p.Nome
                    })
                    .ToList()
            };

            return View(vm);
        }

        // CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TurmaViewModel vm)
        {
            // Regra: no máximo 2 professoras
            if (vm.ProfessorasSelecionadas.Count > 2)
            {
                ModelState.AddModelError("", "Selecione no máximo duas professoras.");
            }

            if (!ModelState.IsValid)
            {
                RecarregarProfessoras(vm);
                return View(vm);
            }

            // Regra: conflito de turno
            var conflito = _context.Turmas
                .Include(t => t.Professoras)
                .Any(t =>
                    t.Turno == vm.Turno &&
                    t.Professoras.Any(p =>
                        vm.ProfessorasSelecionadas.Contains(p.ProfessoraId)));

            if (conflito)
            {
                ModelState.AddModelError("", "Uma das professoras já está alocada em outra turma neste turno.");
                RecarregarProfessoras(vm);
                return View(vm);
            }

            // Criar entidade
            var turma = new Turma
            {
                Nome = vm.Nome,
                Turno = vm.Turno,
                HoraInicio = vm.HoraInicio,
                HoraFim = vm.HoraFim,
                Professoras = _context.Professoras
                    .Where(p => vm.ProfessorasSelecionadas.Contains(p.ProfessoraId))
                    .ToList()
            };

            _context.Turmas.Add(turma);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // MÉTODO AUXILIAR
        private void RecarregarProfessoras(TurmaViewModel vm)
        {
            vm.Professoras = _context.Professoras
                .Select(p => new ProfessoraItemViewModel
                {
                    ProfessoraId = p.ProfessoraId,
                    Nome = p.Nome,
                    Selecionada = vm.ProfessorasSelecionadas.Contains(p.ProfessoraId)
                })
                .ToList();
        }
    }
}
