using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Backend.Repositories.Implementations;
using Orders.Backend.Repositories.Interfaces;
using Orders.Backend.UnitsOfWork.Implementations;
using Orders.Backend.UnitsOfWork.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); //Para ignorar redundancias cíclicas
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Inyectar conexión con SQL Server
//Se referencia que tipo de motor se va a usar
//"Name=LocaltConnection" es para que tome el string de conexión del appsettings.json

//Este es el código original de la guía ↓
//builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=LocaltConnection"));

//MODIFICO lo de arriba ↑ con var connStr para poder usar el CommandTimeout
//para poder ejecutar con mas tiempo scripts SQL de gran tamaño
var connStr = builder.Configuration.GetConnectionString("LocaltConnection");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(
        connStr,
        sql => sql.CommandTimeout(600) // ⬅️ 600s = 10 min
    )
);

//Inyectar el servicio de SeedDb de forma "Transient" (transitorio)
builder.Services.AddTransient<SeedDb>();

//Inyección de dependencias de forma "Scoped" (alcance o ámbito)
//Antes de hacer el bill se le dice "que me inyecte al Builder.Servicies,
//adicione de tipo de la unidad de tabajo genérico el repositorio genérico
builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));

//y agregueme del GenericRepositor, agrege o implemente repositorio genérico"
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<ICitiesRepository, CitiesRepository>();
builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();
builder.Services.AddScoped<IStatesRepository, StatesRepository>();

builder.Services.AddScoped<ICitiesUnitOfWork, CitiesUnitOfWork>();
builder.Services.AddScoped<ICountriesUnitOfWork, CountriesUnitOfWork>();
builder.Services.AddScoped<IStatesUnitOfWork, StatesUnitOfWork>();

var app = builder.Build();

//Luego de crear la app, se inyecata llamando el método SeedData que recibe la app (WebApplication)

//CAMBIOS DE LA GUIA para que el programa no se bloquee al iniciar
//SeedData(app);

//Se cambió el método SeedData por SeedDataAsync y se agregó await
await SeedDataAsync(app); //CAMBIO

//void SeedData(WebApplication app)
static async Task SeedDataAsync(WebApplication app) //CAMBIO
{
    //scopedFactory es la forma de llamar las direcciones de los servicios
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    //Con esto se garantiza que cada que se corra el programa se ejecute el método SeedAsync de la clase SeedDb
    using var scope = scopedFactory!.CreateScope();
    var service = scope.ServiceProvider.GetService<SeedDb>();
    //service!.SeedAsync().Wait(); //Es .Wait porque se llama un método async desde un método que no es async

    await service!.SeedAsync(); //CAMBIO esto sin .Wait()

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
}