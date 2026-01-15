namespace SistemaEscolar.Models
{
    using System;
    using System.Collections.Generic;

    public class Professora
    {
        public int ProfessoraId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }

        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }

        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Formacao { get; set; }

        public ICollection<Turma> Turmas { get; set; }
    }

}
