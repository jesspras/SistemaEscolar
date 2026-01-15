using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEscolar.Data;
using SistemaEscolar.Models;
using SistemaEscolar.ViewModels;

public class FrequenciasController : Controller
{
    private readonly SistemaEscolarContext _context;

    public FrequenciasController(SistemaEscolarContext context)
    {
        _context = context;
    }

    // LISTA DE TURMAS PARA ESCOLHA
    public IActionResult Index()
    {
        var turmas = _context.Turmas
            .Select(t => new
            {
                t.TurmaId,
                t.Nome,
                t.Turno
            })
            .ToList();

        return View(turmas);
    }

    // GET – REGISTRAR / EDITAR FREQUÊNCIA MENSAL
    public IActionResult Registrar(int turmaId)
    {
        var turma = _context.Turmas
            .Include(t => t.Alunos)
            .FirstOrDefault(t => t.TurmaId == turmaId);

        if (turma == null)
            return NotFound();

        var mes = DateTime.Now.Month;
        var ano = DateTime.Now.Year;

        var frequenciasExistentes = _context.Frequencias
            .Where(f => f.Mes == mes && f.Ano == ano)
            .ToList();

        var vm = new FrequenciaViewModel
        {
            TurmaId = turma.TurmaId,
            NomeTurma = turma.Nome,
            Mes = mes,
            Ano = ano,
            DiasLetivos = frequenciasExistentes.FirstOrDefault()?.DiasLetivos ?? 0,
            Alunos = turma.Alunos.Select(a =>
            {
                var freq = frequenciasExistentes.FirstOrDefault(f => f.AlunoId == a.AlunoId);

                return new FrequenciaAlunoViewModel
                {
                    AlunoId = a.AlunoId,
                    Nome = a.Nome,
                    DataNascimento = a.DataNascimento,
                    Faltas = freq?.Faltas ?? 0,
                    PercentualPresenca = freq?.PercentualPresenca ?? 0
                };
            }).ToList()
        };

        return View(vm);
    }

    // POST – SALVAR FREQUÊNCIA
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Registrar(FrequenciaViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        foreach (var aluno in vm.Alunos)
        {
            var percentual = ((decimal)(vm.DiasLetivos - aluno.Faltas) / vm.DiasLetivos) * 100;

            var frequencia = _context.Frequencias.FirstOrDefault(f =>
                f.AlunoId == aluno.AlunoId &&
                f.Mes == vm.Mes &&
                f.Ano == vm.Ano);

            if (frequencia == null)
            {
                _context.Frequencias.Add(new Frequencia
                {
                    AlunoId = aluno.AlunoId,
                    Mes = vm.Mes,
                    Ano = vm.Ano,
                    DiasLetivos = vm.DiasLetivos,
                    Faltas = aluno.Faltas,
                }

