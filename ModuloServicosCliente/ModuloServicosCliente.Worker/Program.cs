using ModuloInformacoesCadastrais.Infra.IoC;
using ModuloServicosCliente.Infra.Options;
using ModuloServicosCliente.Workers;

IHostBuilder builder = Host.CreateDefaultBuilder(args)
	.ConfigureLogging(logging =>
	{
		logging.ClearProviders();
		logging.AddConsole();
	})
	.ConfigureServices((context, services) =>
	{
		var configuration = context.Configuration;

		services.Configure<CamundaCloudClientOptions>(
		  configuration.GetSection(CamundaCloudClientOptions.ConfigName));
		services.Configure<CamundaCloudWorkerOptions>(
		  configuration.GetSection(CamundaCloudWorkerOptions.ConfigName));

		InjetorDeDependencias.ConfigurarDependencias(services, configuration);
		services.AddHostedService<ObterDadosClienteWorker>();
	});

IHost host = builder.Build();
await host.RunAsync();
