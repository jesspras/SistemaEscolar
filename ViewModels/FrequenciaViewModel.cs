using System.ComponentModel.DataAnnotations;
namespace SistemaEscolar.ViewModels
{ 
public class FrequenciaViewModel
{
    public int TurmaId { get; set; }
    public string NomeTurma { get; set; }

    public int Mes { get; set; }
    public int Ano { get; set; }

    [Required]
    [Display(Name = "Dias Letivos")]
    public int DiasLetivos { get; set; }

    public List<FrequenciaAlunoViewModel> Alunos { get; set; } = new();
}
}