namespace SistemaEscolar.Models
{
    public class Parecer
    {
        public int ParecerId { get; set; }

        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }

        public int Ano { get; set; }
        public int Semestre { get; set; } // 1 ou 2

        public string CaminhoArquivo { get; set; }
    }

}
