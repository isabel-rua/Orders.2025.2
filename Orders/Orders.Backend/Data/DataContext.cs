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

    //Propiedad DbSet (generica) / Heredado de Entities
    public DbSet<Category> Categories { get; set; }

    public DbSet<Country> Countries { get; set; }

    //Indice único por nombre de la tabla Country
    //Se sobrescribe el método OnModelCreating para configurar el modelo de datos.
    //Esto es una validación a nivel de base de datos
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Category>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<Country>()
            .HasIndex(x => x.Name) // => notación lambda / notación de flecha / Indice por el nombre
            .IsUnique();
    }
}