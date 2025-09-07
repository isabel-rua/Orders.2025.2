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
        await CheckCountriesAsync();
        await CheckCategoriesAsync();
    }

    //Métodos privados para verificar y agregar categorías y países
    private async Task CheckCategoriesAsync()
    {
        if (!_context.Categories.Any()) //Método Any = alguna, se niega condición (!) "Si no hay categorías"
        {
            //Adicionar nuevas categorías
            _context.Categories.Add(new Category { Name = "Calzado" });
            _context.Categories.Add(new Category { Name = "Tecnología" });
            await _context.SaveChangesAsync(); //Guardar los cambios
        }
    }

    private async Task CheckCountriesAsync()
    {
        if (!_context.Countries.Any()) //Método Any = alguna, se niega condición (!) "Si no hay países"
        {
            _context.Countries.Add(new Country { Name = "Colombia" });
            _context.Countries.Add(new Country { Name = "Bolivia" });
            await _context.SaveChangesAsync(); //Guardar los cambios
        }
    }
}