using ModuloEmail.API;
using ModuloEmail.API.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<EmailOptions>(
    builder.Configuration.GetSection(EmailOptions.ConfigName));

builder.Services.AddHttpClient<EmailService>();

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
