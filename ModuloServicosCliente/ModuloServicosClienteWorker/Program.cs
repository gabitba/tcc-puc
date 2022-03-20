using ModuloInformacoesCadastrais.Infra.IoC;
using ModuloServicosClienteWorker.Infra.Options;
using ModuloServicosClienteWorker.Workers;

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
		services.AddHostedService<Worker>();
	});

IHost host = builder.Build();
await host.RunAsync();
