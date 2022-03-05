using Microsoft.Extensions.DependencyInjection;
using ModuloInformacoesCadastrais.Application.Interfaces;
using ModuloInformacoesCadastrais.Application.Servicos;
using ModuloInformacoesCadastrais.Domain.Interfaces;
using ModuloInformacoesCadastrais.Domain.Repositorios;

namespace ModuloInformacoesCadastrais.Infrastructure
{
    public static class InjetorDeDependencias
    {
        public static void ConfigurarServicos(IServiceCollection servicos)
        {
            servicos.AddSingleton<IClientesRepositorio, ClientesRepositorio>();
            servicos.AddScoped<IClientesServico, ClientesServicos>();
        }
    }
}