using Microsoft.EntityFrameworkCore;
using Orders.Shared.Entities;

namespace Orders.Backend.Data;

public class DataContext : DbContext
{
    //Esta clase representa el contexto de datos de la aplicación Orders.
    //Se utiliza para interactuar con la base de datos y gestionar las entidades relacionadas con los pedidos.

    //El constructor de la clase DataContext recibe opciones de configuración para el contexto de datos.
    //Sintaxis para crear el contructor de la BD
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    //Propiedad DbSet (generica) / Heredado de Entities (Esto es opcional)
    //Para acceder a la colección de categorías en la base de datos
    public DbSet<Category> Categories { get; set; }

    public DbSet<City> Cities { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<State> States { get; set; }

    //Para indices únicos por nombre de las tablas de la BD ↓

    //Se sobrescribe el método OnModelCreating para configurar el modelo de datos.
    //Esto es una validación a nivel de base de datos
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Category>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<City>().HasIndex(x => new { x.StateId, x.Name }).IsUnique(); //Indice compuesto "Un solo nombre por ciudad"
        modelBuilder.Entity<Country>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<State>().HasIndex(x => new { x.CountryId, x.Name }).IsUnique(); //Indice compuesto "Un solo nombre por país"

        //Deshabilitar el borrado en cascada (DeleteBehavior.Restrict) ↓
        DisableCascadingDelete(modelBuilder);
    }

    private void DisableCascadingDelete(ModelBuilder modelBuilder)
    {
        var relationships = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
        foreach (var relationship in relationships)
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}