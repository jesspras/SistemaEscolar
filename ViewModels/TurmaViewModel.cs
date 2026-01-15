using System.ComponentModel.DataAnnotations;

public class TurmaViewModel
{
    public int TurmaId { get; set; }

    [Required(ErrorMessage = "O nome da turma é obrigatório")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O turno é obrigatório")]
    public string Turno { get; set; }

    [Required]
    [Display(Name = "Hora de Início")]
    public TimeSpan HoraInicio { get; set; }

    [Required]
    [Display(Name = "Hora de Término")]
    public TimeSpan HoraFim { get; set; }

    // Checkbox
    public List<int> ProfessorasSelecionadas { get; set; } = new();

    // Lista para renderizar na View
    public List<ProfessoraItemViewModel> Professoras { get; set; } = new();
}
