using ModuloServicosCliente.API.OpenApi;
using ModuloServicosCliente.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

InjetorDeDependencias.ConfigurarCamundaService(builder.Services, builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAndConfigureOpenApiSupport(builder.Configuration);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
