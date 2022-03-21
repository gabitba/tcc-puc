using ModuloInformacoesCadastrais.Infra.IoC;
using ModuloServicosCliente.Workers;

IHostBuilder builder = Host.CreateDefaultBuilder(args)
	.ConfigureLogging(logging =>
	{
		logging.ClearProviders();
		logging.AddConsole();
	})
	.ConfigureServices((context, services) =>
	{
        InjetorDeDependencias.ConfigurarDependencias(services, context.Configuration);
		services.AddHostedService<ObterDadosClienteWorker>();
	});

IHost host = builder.Build();
await host.RunAsync();
