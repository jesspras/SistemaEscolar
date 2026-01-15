using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaEscolar.ViewModels
{
    public class AlunoViewModel
    {
        public int AlunoId { get; set; }
        public int TurmaId { get; set; } 

        [Required]
        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }

        public string CPF { get; set; }

        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }

        public string NomeMae { get; set; }
        public string TelefoneMae { get; set; }

        public string NomePai { get; set; }
        public string TelefonePai { get; set; }

        public string Cor { get; set; }
        public string Sexo { get; set; }

        public List<PessoaAutorizadaViewModel> PessoasAutorizadas { get; set; } = new();
    }
}
