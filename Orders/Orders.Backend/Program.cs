using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Backend.Repositories.Implementations;
using Orders.Backend.Repositories.Interfaces;
using Orders.Backend.UnitsOfWork.Implementations;
using Orders.Backend.UnitsOfWork.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Inyectar conexión con SQL Server
//Se referencia que tipo de motor se va a usar
//"Name=LocaltConnection" es para que tome el string de conexión del appsettings.json
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=LocaltConnection"));

//Inyección de dependencias
//Antes de hacer el bill se le dice "que me inyecte al Builder.Servicies,
//adicione de tipo de la unidad de tabajo genérico el repositorio genérico
builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));

//y agregueme del GenericRepositor, agrege o implemente repositorio genérico"
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

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