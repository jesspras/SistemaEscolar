using Microsoft.AspNetCore.Mvc;
using SistemaEscolar.Data;
using SistemaEscolar.Models;
namespace SistemaEscolar.Controllers
{
    public class PareceresController : Controller
    {
        private readonly SistemaEscolarContext _context;
        private readonly IWebHostEnvironment _env;

        public PareceresController(SistemaEscolarContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost]
        public IActionResult Upload(int alunoId, int semestre, int ano, IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
                return BadRequest();

            var pasta = Path.Combine(_env.WebRootPath, "Uploads", "Pareceres");
            Directory.CreateDirectory(pasta);

            var nomeArquivo = $"{alunoId}_{semestre}_{ano}{Path.GetExtension(arquivo.FileName)}";
            var caminho = Path.Combine(pasta, nomeArquivo);

            using var stream = new FileStream(caminho, FileMode.Create);
            arquivo.CopyTo(stream);

            _context.Pareceres.Add(new Parecer
            {
                AlunoId = alunoId,
                Ano = ano,
                Semestre = semestre,
                CaminhoArquivo = nomeArquivo
            });

            _context.SaveChanges();
            return RedirectToAction("Index", "Alunos");
        }
    }
}