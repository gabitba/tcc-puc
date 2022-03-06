using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModuloInformacoesCadastrais.Application.Interfaces;
using ModuloInformacoesCadastrais.Application.Servicos;
using ModuloInformacoesCadastrais.Domain.Interfaces;
using ModuloInformacoesCadastrais.Domain.Repositorios;
using ModuloInformacoesCadastrais.Infra.Data.Context;

namespace ModuloInformacoesCadastrais.Infrastructure
{
    public static class InjetorDeDependencias
    {
        public static void ConfigurarDependencias(IServiceCollection servicos, IConfiguration configuration)
        {
            servicos.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("InformacoesCadastrais"));
            });

            servicos.AddScoped<IClientesRepositorio, ClientesRepositorio>();
            servicos.AddScoped<IClientesServico, ClientesServicos>();
        }
    }
}