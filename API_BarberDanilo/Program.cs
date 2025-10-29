using API_BarberDanilo.Data;
using API_BarberDanilo.Interfaces;
using API_BarberDanilo.Models;
using API_BarberDanilo.Repositories;
using API_BarberDanilo.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext con SQL Server
builder.Services.AddDbContext<BarberShopDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()
    )
);

// Inyecci�n de dependencias - Principio D de SOLID (Inversi�n de Dependencias)
// Registramos el repositorio gen�rico con su implementaci�n de Entity Framework
builder.Services.AddScoped<IRepository<Appointment>, EfAppointmentRepository>();

// Registramos el servicio de citas
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Barber�a Danilo",
        Version = "v1",
        Description = "API REST para gesti�n de citas de barber�a usando arquitectura limpia y principios SOLID"
    });
});

// Configurar CORS (�til para desarrollo con frontend)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Aplicar migraciones autom�ticamente en desarrollo
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<BarberShopDbContext>();
        try
        {
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Error al aplicar migraciones de base de datos");
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
