using System.ComponentModel.DataAnnotations;
namespace SistemaEscolar.ViewModels
{ 
public class FrequenciaAlunoViewModel
{
    public int AlunoId { get; set; }
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }

    [Range(0, 31)]
    public int Faltas { get; set; }

    public decimal PercentualPresenca { get; set; }
}
}