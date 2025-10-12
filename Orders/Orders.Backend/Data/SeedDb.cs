using Microsoft.EntityFrameworkCore;
using Orders.Shared.Entities;

namespace Orders.Backend.Data;

public class SeedDb
{
    //Esta clase es un alimentador de datos (seeder).
    //Esta clase se encarga de inicializar y poblar la base de datos con datos predeterminados.

    //Inyectar la conexión a la base de datos
    //Para que la inyección permanezca durante todo el ciclo de vida de la app se crea propiedad
    private readonly DataContext _context; //_context es un campo de la clase estandar

    public SeedDb(DataContext context) //Con lo que esta () se inyecta el contexto de la bd
    {
        _context = context;
    }

    //Método SeedAsync para garantizar que la BD exista y cree registros
    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync(); //Asegura que la BD exista, si no existe, se crea la BD.

        //Estos 2 métodos son para garantizar que existan los países y las categorías en la BD.
        await CheckCountriesFullAsync();
        await CheckCountriesAsync();
        await CheckCategoriesAsync();
    }

    //Métodos privados para verificar y agregar categorías y países
    private async Task CheckCountriesFullAsync()
    {
        if (!_context.Countries.Any())
        {
            var countriesSQLScript = File.ReadAllText("Data\\CountriesStatesCities.sql");
            await _context.Database.ExecuteSqlRawAsync(countriesSQLScript);
        }
    }

    private async Task CheckCategoriesAsync()
    {
        if (!_context.Categories.Any()) //Método Any = alguna, se niega condición (!) "Si no hay categorías"
        {
            //Adicionar nuevas categorías
            _context.Categories.Add(new Category { Name = "Apple" });
            _context.Categories.Add(new Category { Name = "Autos" });
            _context.Categories.Add(new Category { Name = "Belleza" });
            _context.Categories.Add(new Category { Name = "Calzado" });
            _context.Categories.Add(new Category { Name = "Comida" });
            _context.Categories.Add(new Category { Name = "Cosmeticos" });
            _context.Categories.Add(new Category { Name = "Deportes" });
            _context.Categories.Add(new Category { Name = "Erótica" });
            _context.Categories.Add(new Category { Name = "Ferreteria" });
            _context.Categories.Add(new Category { Name = "Gamer" });
            _context.Categories.Add(new Category { Name = "Hogar" });
            _context.Categories.Add(new Category { Name = "Jardín" });
            _context.Categories.Add(new Category { Name = "Jugetes" });
            _context.Categories.Add(new Category { Name = "Lenceria" });
            _context.Categories.Add(new Category { Name = "Mascotas" });
            _context.Categories.Add(new Category { Name = "Nutrición" });
            _context.Categories.Add(new Category { Name = "Ropa" });
            _context.Categories.Add(new Category { Name = "Tecnología" });
            await _context.SaveChangesAsync(); //Guardar los cambios
        }
    }

    private async Task CheckCountriesAsync()
    {
        if (!_context.Countries.Any()) //Método Any = alguna, se niega condición (!) "Si no hay países"
        {
            _context.Countries.Add(new Country
            {
                Name = "Colombia",
                States = [
               new State()
                {
                    Name = "Antioquia",
                    Cities = [
                        new City() { Name = "Medellín" },
                        new City() { Name = "Itagüí" },
                        new City() { Name = "Envigado" },
                        new City() { Name = "Bello" },
                        new City() { Name = "Rionegro" },
                    ]
                },
                new State()
                {
                    Name = "Bogotá",
                    Cities = [
                        new City() { Name = "Usaquen" },
                        new City() { Name = "Champinero" },
                        new City() { Name = "Santa fe" },
                        new City() { Name = "Useme" },
                        new City() { Name = "Bosa" },
                    ]
                },
            ]
            });
            _context.Countries.Add(new Country
            {
                Name = "Estados Unidos",
                States = [
                    new State()
                {
                    Name = "Florida",
                    Cities = [
                        new City() { Name = "Orlando" },
                        new City() { Name = "Miami" },
                        new City() { Name = "Tampa" },
                        new City() { Name = "Fort Lauderdale" },
                        new City() { Name = "Key West" },
                    ]
                },
                new State()
                    {
                        Name = "Texas",
                        Cities = [
                            new City() { Name = "Houston" },
                            new City() { Name = "San Antonio" },
                            new City() { Name = "Dallas" },
                            new City() { Name = "Austin" },
                            new City() { Name = "El Paso" },
                        ]
                    },
                ]
            });
        }
        await _context.SaveChangesAsync();
    }
}