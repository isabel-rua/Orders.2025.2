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

//Inyectar el servicio de SeedDb de forma "Transient" (transitorio)
builder.Services.AddTransient<SeedDb>();

//Inyección de dependencias de forma "Scoped" (alcance o ámbito)
//Antes de hacer el bill se le dice "que me inyecte al Builder.Servicies,
//adicione de tipo de la unidad de tabajo genérico el repositorio genérico
builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));

//y agregueme del GenericRepositor, agrege o implemente repositorio genérico"
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

//Luego de crear la app, se inyecata llamando el método SeedData que recibe la app (WebApplication)
SeedData(app);

void SeedData(WebApplication app)
{
    //scopedFactory es la forma de llamar las direcciones de los servicios
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    //Con esto se garantiza que cada que se corra el programa se ejecute el método SeedAsync de la clase SeedDb
    using var scope = scopedFactory!.CreateScope();
    var service = scope.ServiceProvider.GetService<SeedDb>();
    service!.SeedAsync().Wait(); //Es .Wait porque se llama un método async desde un método que no es async

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