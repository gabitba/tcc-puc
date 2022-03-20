using Microsoft.OpenApi.Models;
using ModuloInformacoesCadastrais.Infra.IoC;
using ModuloServicosClienteWorker.Infra.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CamundaCloudClientOptions>(
  builder.Configuration.GetSection(CamundaCloudClientOptions.ConfigName));
builder.Services.Configure<CamundaCloudWorkerOptions>(
  builder.Configuration.GetSection(CamundaCloudWorkerOptions.ConfigName));

InjetorDeDependencias.ConfigurarDependencias(builder.Services, builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API Modulo de Serviços ao Cliente",
        Description = "Uma aplicação .NET 6 Web API para controle de processos BPMN no Camunda.",
        Contact = new OpenApiContact
        {
            Name = "Gabriela Tolentino",
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
