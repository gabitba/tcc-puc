using ModuloServicosCliente.Infra.IoC;
using ModuloServicosCliente.Workers;

IHostBuilder builder = Host.CreateDefaultBuilder(args)
	.ConfigureLogging(logging =>
	{
		logging.ClearProviders();
		logging.AddConsole();
	})
	.ConfigureServices((context, services) =>
	{
        InjetorDeDependencias.ConfigurarCamundaService(services, context.Configuration);
		InjetorDeDependencias.ConfigurarAPIs(services, context.Configuration);

		services.AddHostedService<ObterDadosClienteWorker>();
		services.AddHostedService<EnviarEmailReportClienteWorker>();
	});

IHost host = builder.Build();
await host.RunAsync();
