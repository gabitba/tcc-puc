using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ModuloInformacoesCadastrais.Infra.IoC
{
    public static class InjetorDeDependencias
    {
        public static void ConfigurarWorkers(IServiceCollection servicos, IConfiguration configuration)
        {
            //servicos.AddDbContext<AppDbContext>(options =>
            //{
            //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            //});

            //servicos.AddScoped<IClientesRepositorio, ClientesRepositorio>();
            //servicos.AddScoped<IClientesServico, ClientesServicos>();
        }
    }
}