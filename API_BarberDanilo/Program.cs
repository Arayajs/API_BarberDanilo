using API_BarberDanilo.Interfaces;
using API_BarberDanilo.Models;
using API_BarberDanilo.Repositories;
using API_BarberDanilo.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. A�adir servicios de controladores y API Explorer (para Swagger)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Registrar nuestras interfaces y sus implementaciones concretas
// Principio de Inversi�n de Dependencias en acci�n
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

// Usamos Singleton para el repositorio en memoria porque debe persistir durante toda la vida de la app.
// Si usaras EF Core, usar�as AddScoped.
builder.Services.AddSingleton<IRepository<Appointment>, InMemoryAppointmentRepository>();

//--Fin de configconfiguraci�n --
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
