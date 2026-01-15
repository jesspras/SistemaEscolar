using Microsoft.AspNetCore.Mvc;
using SistemaEscolar.Data;
using SistemaEscolar.Models;
namespace SistemaEscolar.Controllers
{
    public class HistoricosController : Controller
    {
        private readonly SistemaEscolarContext _context;
        private readonly IWebHostEnvironment _env;

        public HistoricosController(SistemaEscolarContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost]
        public IActionResult Upload(int alunoId, int ano, IFormFile arquivo)
        {
            var pasta = Path.Combine(_env.WebRootPath, "Uploads", "Historico");
            Directory.CreateDirectory(pasta);

            var nomeArquivo = $"historico_{alunoId}_{ano}{Path.GetExtension(arquivo.FileName)}";
            var caminho = Path.Combine(pasta, nomeArquivo);

            using var stream = new FileStream(caminho, FileMode.Create);
            arquivo.CopyTo(stream);

            _context.HistoricosEscolares.Add(new HistoricoEscolar
            {
                AlunoId = alunoId,
                Ano = ano,
                CaminhoArquivo = nomeArquivo
            });

            _context.SaveChanges();
            return RedirectToAction("Index", "Alunos");
        }
    }
}