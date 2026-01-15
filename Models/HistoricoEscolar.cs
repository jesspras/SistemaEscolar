namespace SistemaEscolar.Models
{
    public class HistoricoEscolar
    {
        public int HistoricoEscolarId { get; set; }

        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }

        public int Ano { get; set; }
        public string CaminhoArquivo { get; set; }
    }

}
