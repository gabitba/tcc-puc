using ModuloInformacoesCadastrais.Infra.IoC;
using ModuloServicosClienteWorker.Infra.Options;
using ModuloServicosClienteWorker.Workers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        services.Configure<CamundaCloudClientOptions>(
            configuration.GetSection(CamundaCloudClientOptions.ConfigName));

        InjetorDeDependencias.ConfigurarDependencias(services, configuration);

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
