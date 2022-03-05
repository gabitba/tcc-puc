using ModuloInformacoesCadastrais.Domain.Entidades;
using System.Data.Entity;

namespace ModuloInformacoesCadastrais.Infra.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(string connectionString) : base(connectionString){ }

        public DbSet<Cliente> Clientes { get; set; }
    }
}
