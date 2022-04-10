using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModuloInformacoesCadastrais.Application.Interfaces;
using ModuloInformacoesCadastrais.Application.Services;
using ModuloInformacoesCadastrais.Domain.Interfaces;
using ModuloInformacoesCadastrais.Domain.Repositorios;
using ModuloInformacoesCadastrais.Infra.Data.Context;

namespace ModuloInformacoesCadastrais.Infra.IoC
{
    public static class InjetorDeDependencias
    {
        public static void ConfigurarDependencias(IServiceCollection servicos, IConfiguration configuration)
        {
            servicos.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
                });
            });

            servicos.AddScoped<IClientesRepositorio, ClientesRepositorio>();
            servicos.AddScoped<IClientesService, ClientesService>();
        }
    }
}