namespace SistemaEscolar.Models
{
    public class PessoaAutorizada
    {
        public int PessoaAutorizadaId { get; set; }

        public string Nome { get; set; }
        public string Telefone { get; set; }

        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }
    }

}
