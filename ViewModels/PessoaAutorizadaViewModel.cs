using System.ComponentModel.DataAnnotations;

namespace SistemaEscolar.ViewModels
{
    public class PessoaAutorizadaViewModel
    {
        public int PessoaAutorizadaId { get; set; } // 👈 ADICIONE ISSO

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Telefone { get; set; }
    }
}
