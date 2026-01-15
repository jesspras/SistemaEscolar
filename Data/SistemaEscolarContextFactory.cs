using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SistemaEscolar.Data
{
    public class SistemaEscolarContextFactory
        : IDesignTimeDbContextFactory<SistemaEscolarContext>
    {
        public SistemaEscolarContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<SistemaEscolarContext>();

            optionsBuilder.UseSqlServer(
                configuration.GetConnectionString("SistemaEscolarContext")
            );

            return new SistemaEscolarContext(optionsBuilder.Options);
        }
    }
}
