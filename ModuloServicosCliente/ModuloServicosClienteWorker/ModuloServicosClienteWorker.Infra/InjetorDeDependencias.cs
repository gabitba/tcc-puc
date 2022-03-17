using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModuloServicosClienteWorker.Infra.Servicos;

namespace ModuloInformacoesCadastrais.Infra.IoC
{
    public static class InjetorDeDependencias
    {
        public static void ConfigurarDependencias(IServiceCollection servicos, IConfiguration configuration)
        {
            servicos.AddSingleton<ICamundaCloudClientFactory, CamundaCloudClientFactory>();
            servicos.AddSingleton<ICamundaCloudWorkerFactory, CamundaCloudWorkerFactory>();
        }
    }
}