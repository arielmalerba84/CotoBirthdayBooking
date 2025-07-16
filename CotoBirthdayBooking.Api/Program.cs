using CotoBirthDayBooking.Api.Middleware;
using CotoBirthDayBooking.Application;
using CotoBirthDayBooking.Application.MappingProfile;
using CotoBirthDayBooking.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Detecta si está corriendo en Docker
var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8081); // HTTP en cualquier entorno

    if (!isDocker)
    {
        // HTTPS solo fuera de Docker
        options.ListenAnyIP(7285, listenOptions => listenOptions.UseHttps());
    }
});

// Servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyección de dependencias por capa
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Mappings).Assembly);

var app = builder.Build();

// Middleware de errores
app.UseMiddleware<ErrorHandlingMiddleware>();

// Swagger siempre habilitado (en todos los entornos)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BirthdayBooking API V1");
    c.RoutePrefix = "swagger"; // para acceder desde /swagger
});

// HTTPS redirection (solo aplica si está configurado)
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

