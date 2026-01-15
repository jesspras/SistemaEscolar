using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEscolar.Data;
using SistemaEscolar.ViewModels;

namespace SistemaEscolar.Controllers
{
    public class HomeController : Controller
    {
        private readonly SistemaEscolarContext _context;

        public HomeController(SistemaEscolarContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var mesAtual = DateTime.Now.Month;
            var anoAtual = DateTime.Now.Year;

            var alunosRisco = _context.Frequencias
                .Include(f => f.Aluno)
                    .ThenInclude(a => a.Turma)
                .Where(f =>
                    f.Mes == mesAtual &&
                    f.Ano == anoAtual &&
                    f.PercentualPresenca < 60)
                .Select(f => new AlunoRiscoViewModel
                {
                    AlunoId = f.AlunoId,
                    Nome = f.Aluno.Nome,
                    Turma = f.Aluno.Turma.Nome,
                    PercentualPresenca = f.PercentualPresenca
                })
                .OrderBy(f => f.PercentualPresenca)
                .ToList();

            return View(alunosRisco);
        }

        public IActionResult Aluno(int id)
        {
            var aluno = _context.Alunos
                .Include(a => a.PessoasAutorizadas)
                .FirstOrDefault(a => a.AlunoId == id);

            if (aluno == null)
                return NotFound();

            return View(aluno);
        }

        [HttpGet]
        public IActionResult BuscarAluno(string nome)
        {
            var alunos = _context.Alunos
                .Where(a => a.Nome.Contains(nome))
                .Select(a => new
                {
                    a.AlunoId,
                    a.Nome
                })
                .Take(10)
                .ToList();

            return Json(alunos);
        }
    }
}
