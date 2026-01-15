namespace SistemaEscolar.Models
{
    using System;

    public class Frequencia
    {
        public int FrequenciaId { get; set; }

        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }

        public int Mes { get; set; }
        public int Ano { get; set; }

        public int DiasLetivos { get; set; }
        public int Faltas { get; set; }

        public decimal PercentualPresenca { get; set; }
    }

}
