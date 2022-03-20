using Microsoft.OpenApi.Models;
using ModuloInformacoesCadastrais.Infra.IoC;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

InjetorDeDependencias.ConfigurarDependencias(builder.Services, builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API Modulo de Informações Cadastrais",
        Description = "Uma aplicação .NET 6 Web API para acesso aos dados cadastrais.",
        Contact = new OpenApiContact
        {
            Name = "Gabriela Tolentino",
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
