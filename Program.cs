using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Agregar contexto de base de datos en memoria
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("PlatformStatuses"));

// Registrar servicios necesarios
builder.Services.AddScoped<PlatformStatusService>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetPlatformStatusesHandler>());
builder.Services.AddHttpClient();
builder.Services.AddAutoMapper(typeof(Program));

// Agregar Swagger para documentación de API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Agregar el logger
builder.Services.AddLogging();

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("PlatformStatuses"));

// Crear la aplicación.
var app = builder.Build();

// Habilitar Swagger en la ruta de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Genera el archivo Swagger JSON
    app.UseSwaggerUI(); // Interfaz de usuario de Swagger
}

// Configurar la canalización de solicitudes HTTP.
app.UseAuthorization();
app.MapControllers();

// Iniciar la aplicación
app.Run();
