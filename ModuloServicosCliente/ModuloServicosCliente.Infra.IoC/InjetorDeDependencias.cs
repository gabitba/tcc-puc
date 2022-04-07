using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModuloServicosCliente.Application.Interfaces;
using ModuloServicosCliente.Infra.Options;
using ModuloServicosCliente.Infra.Services;

namespace ModuloServicosCliente.Infra.IoC
{
    public static class InjetorDeDependencias
    {
        public static void ConfigurarCamundaService(IServiceCollection servicos, IConfiguration configuration)
        {
            servicos.Configure<CamundaCloudClientOptions>(
                configuration.GetSection(CamundaCloudClientOptions.ConfigName));
            servicos.Configure<CamundaCloudWorkerOptions>(
                configuration.GetSection(CamundaCloudWorkerOptions.ConfigName));

            bool isDevelopment = configuration.GetValue<bool>("IsDevelopment");
            if (isDevelopment)
            {
                servicos.AddSingleton<IZeebeService, ZeebeServiceLocal>();
            }
            else
            {
                servicos.AddSingleton<IZeebeService, ZeebeServiceCloud>();
            }
        }

        public static void ConfigurarAPIs(IServiceCollection servicos, IConfiguration configuration)
        {
            servicos.Configure<ClienteAPIOptions>(
                configuration.GetSection(ClienteAPIOptions.ConfigName));
            servicos.AddHttpClient<IClienteService, ClienteService>();

            servicos.Configure<EmailAPIOptions>(
                configuration.GetSection(EmailAPIOptions.ConfigName));
            servicos.AddHttpClient<IEmailService, EmailService>();
        }
    }
}