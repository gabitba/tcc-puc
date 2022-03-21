using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModuloServicosCliente.Application.Interfaces;
using ModuloServicosCliente.Infra.Interfaces;

namespace ModuloInformacoesCadastrais.Infra.IoC
{
    public static class InjetorDeDependencias
    {
        public static void ConfigurarDependencias(IServiceCollection servicos, IConfiguration configuration)
        {
            servicos.AddSingleton<IZeebeService, ZeebeService>();
        }
    }
}