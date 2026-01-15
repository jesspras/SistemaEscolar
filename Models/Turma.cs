namespace SistemaEscolar.Models
{
    using System;
    using System.Collections.Generic;

    public class Turma
    {
        public int TurmaId { get; set; }
        public string Nome { get; set; }

        public string Turno { get; set; } // Manhã, Tarde, Integral
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }

        public ICollection<Professora> Professoras { get; set; }
        public ICollection<Aluno> Alunos { get; set; }
    }

}
