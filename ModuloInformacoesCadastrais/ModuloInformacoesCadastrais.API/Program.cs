using ModuloInformacoesCadastrais.API.Authorization;
using ModuloInformacoesCadastrais.Infra.IoC;
using ModuloInformacoesCadastrais.API.OpenApi;

var builder = WebApplication.CreateBuilder(args);

InjetorDeDependencias.ConfigurarDependencias(builder.Services, builder.Configuration);

builder.Services.AddAndConfigureAuthorization(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAndConfigureOpenApiSupport(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins(
                "http://localhost:4201",
                "https://calm-dune-06b3f4b0f.1.azurestaticapps.net")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseSwaggerApp();

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
