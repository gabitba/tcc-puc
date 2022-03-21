using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModuloServicosCliente.Application.Interfaces;
using ModuloServicosCliente.Infra.Interfaces;
using ModuloServicosCliente.Infra.Options;
using ModuloServicosCliente.Infra.Services;

namespace ModuloInformacoesCadastrais.Infra.IoC
{
    public static class InjetorDeDependencias
    {
        public static void ConfigurarDependencias(IServiceCollection servicos, IConfiguration configuration)
        {
            servicos.Configure<CamundaCloudClientOptions>(
                configuration.GetSection(CamundaCloudClientOptions.ConfigName));
            servicos.Configure<CamundaCloudWorkerOptions>(
                configuration.GetSection(CamundaCloudWorkerOptions.ConfigName));
            servicos.Configure<ClienteAPIOptions>(
                configuration.GetSection(ClienteAPIOptions.ConfigName));

            servicos.AddSingleton<IZeebeService, ZeebeService>();
            servicos.AddHttpClient<IClienteService, ClienteService>();
        }
    }
}