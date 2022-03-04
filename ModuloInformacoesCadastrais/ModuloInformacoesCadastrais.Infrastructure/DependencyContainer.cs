using Microsoft.Extensions.DependencyInjection;
using ModuloInformacoesCadastrais.Domain.Interfaces;
using ModuloInformacoesCadastrais.Domain.Repositories;

namespace ModuloInformacoesCadastrais.Infrastructure
{
    public static class DependencyContainer
    {
        public static void ConfigureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IClienteRepository, ClienteRepository>();
        }
    }
}