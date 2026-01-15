using Microsoft.EntityFrameworkCore;
using SistemaEscolar.Models;
namespace SistemaEscolar.Data
{
    public class SistemaEscolarContext : DbContext
    {
        public SistemaEscolarContext(DbContextOptions<SistemaEscolarContext> options)
            : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Professora> Professoras { get; set; }
        public DbSet<PessoaAutorizada> PessoasAutorizadas { get; set; }
        public DbSet<Parecer> Pareceres { get; set; }
        public DbSet<HistoricoEscolar> HistoricosEscolares { get; set; }
        public DbSet<Frequencia> Frequencias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Turma>()
                .HasMany(t => t.Professoras)
                .WithMany(p => p.Turmas)
                .UsingEntity(j => j.ToTable("TurmaProfessoras"));

            modelBuilder.Entity<Parecer>()
                .HasIndex(p => new { p.AlunoId, p.Ano, p.Semestre })
                .IsUnique();

            modelBuilder.Entity<HistoricoEscolar>()
                .HasIndex(h => new { h.AlunoId, h.Ano })
                .IsUnique();
        }
    }
}