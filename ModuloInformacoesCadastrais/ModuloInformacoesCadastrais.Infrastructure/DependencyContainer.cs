using Microsoft.Extensions.DependencyInjection;
using ModuloInformacoesCadastrais.Domain.Interfaces;
using ModuloInformacoesCadastrais.Domain.Repositories;

namespace ModuloInformacoesCadastrais.Infrastructure
{
    public static class DependencyContainer
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IClienteRepository, ClienteRepository>();
        }
    }
}