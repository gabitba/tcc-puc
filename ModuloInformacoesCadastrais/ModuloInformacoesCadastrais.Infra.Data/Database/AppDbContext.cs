using Microsoft.EntityFrameworkCore;
using ModuloInformacoesCadastrais.Domain.Entidades;

namespace ModuloInformacoesCadastrais.Infra.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
    }
}
