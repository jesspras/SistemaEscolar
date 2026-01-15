using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEscolar.Data;
using SistemaEscolar.Models;
using SistemaEscolar.ViewModels;
namespace SistemaEscolar.Controllers
{
    public class AlunosController : Controller
    {
        private readonly SistemaEscolarContext _context;

        public AlunosController(SistemaEscolarContext context)
        {
            _context = context;
        }

        // =========================
        // LISTAGEM
        // =========================
        public IActionResult Index()
        {
            var alunos = _context.Alunos
                .Include(a => a.Turma)
                .OrderBy(a => a.Nome)
                .ToList();

            return View(alunos);
        }

        // =========================
        // CREATE
        // =========================
        public IActionResult Create()
        {
            ViewBag.Turmas = _context.Turmas.OrderBy(t => t.Nome).ToList();

            var vm = new AlunoViewModel();

            InicializarPessoasAutorizadas(vm);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AlunoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Turmas = _context.Turmas.ToList();
                InicializarPessoasAutorizadas(vm);
                return View(vm);
            }

            var aluno = MapearAluno(vm);

            _context.Alunos.Add(aluno);
            _context.SaveChanges();

            SalvarPessoasAutorizadas(vm, aluno);

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // EDIT
        // =========================
        public IActionResult Edit(int id)
        {
            var aluno = _context.Alunos
                .Include(a => a.PessoasAutorizadas)
                .FirstOrDefault(a => a.AlunoId == id);

            if (aluno == null)
                return NotFound();

            ViewBag.Turmas = _context.Turmas.ToList();

            var vm = MapearAlunoViewModel(aluno);

            InicializarPessoasAutorizadas(vm);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AlunoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Turmas = _context.Turmas.ToList();
                InicializarPessoasAutorizadas(vm);
                return View(vm);
            }

            var aluno = _context.Alunos
                .Include(a => a.PessoasAutorizadas)
                .FirstOrDefault(a => a.AlunoId == vm.AlunoId);

            if (aluno == null)
                return NotFound();

            AtualizarAluno(aluno, vm);

            _context.PessoasAutorizadas.RemoveRange(aluno.PessoasAutorizadas);

            SalvarPessoasAutorizadas(vm, aluno);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // MÉTODOS AUXILIARES
        // =========================

        private void InicializarPessoasAutorizadas(AlunoViewModel vm)
        {
            if (vm.PessoasAutorizadas == null)
                vm.PessoasAutorizadas = new List<PessoaAutorizadaViewModel>();

            while (vm.PessoasAutorizadas.Count < 5)
                vm.PessoasAutorizadas.Add(new PessoaAutorizadaViewModel());
        }

        private Aluno MapearAluno(AlunoViewModel vm)
        {
            return new Aluno
            {
                Nome = vm.Nome,
                DataNascimento = vm.DataNascimento,
                Sexo = vm.Sexo,
                Cor = vm.Cor,
                CPF = vm.CPF,

                Rua = vm.Rua,
                Bairro = vm.Bairro,
                CEP = vm.CEP,

                NomeMae = vm.NomeMae,
                TelefoneMae = vm.TelefoneMae,
                NomePai = vm.NomePai,
                TelefonePai = vm.TelefonePai,

                TurmaId = vm.TurmaId
            };
        }

        private void AtualizarAluno(Aluno aluno, AlunoViewModel vm)
        {
            aluno.Nome = vm.Nome;
            aluno.DataNascimento = vm.DataNascimento;
            aluno.Sexo = vm.Sexo;
            aluno.Cor = vm.Cor;
            aluno.CPF = vm.CPF;

            aluno.Rua = vm.Rua;
            aluno.Bairro = vm.Bairro;
            aluno.CEP = vm.CEP;

            aluno.NomeMae = vm.NomeMae;
            aluno.TelefoneMae = vm.TelefoneMae;
            aluno.NomePai = vm.NomePai;
            aluno.TelefonePai = vm.TelefonePai;

            aluno.TurmaId = vm.TurmaId;
        }

        private void SalvarPessoasAutorizadas(AlunoViewModel vm, Aluno aluno)
        {
            foreach (var p in vm.PessoasAutorizadas
                .Where(p => !string.IsNullOrWhiteSpace(p.Nome)))
            {
                aluno.PessoasAutorizadas.Add(new PessoaAutorizada
                {
                    Nome = p.Nome,
                    Telefone = p.Telefone
                });
            }

            _context.SaveChanges();
        }

        private AlunoViewModel MapearAlunoViewModel(Aluno aluno)
        {
            return new AlunoViewModel
            {
                AlunoId = aluno.AlunoId,
                Nome = aluno.Nome,
                DataNascimento = aluno.DataNascimento,
                Sexo = aluno.Sexo,
                Cor = aluno.Cor,
                CPF = aluno.CPF,

                Rua = aluno.Rua,
                Bairro = aluno.Bairro,
                CEP = aluno.CEP,

                NomeMae = aluno.NomeMae,
                TelefoneMae = aluno.TelefoneMae,
                NomePai = aluno.NomePai,
                TelefonePai = aluno.TelefonePai,

                TurmaId = aluno.TurmaId,
                PessoasAutorizadas = aluno.PessoasAutorizadas
                    .Select(p => new PessoaAutorizadaViewModel
                    {
                        PessoaAutorizadaId = p.PessoaAutorizadaId,
                        Nome = p.Nome,
                        Telefone = p.Telefone
                    }).ToList()
            };
        }
    }
}