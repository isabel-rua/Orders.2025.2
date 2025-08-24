using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Shared.Entities;

namespace Orders.Backend.Controllers;

[ApiController] //Indica que es un controlador de API
[Route("api/[controller]")] //Define la ruta base para las solicitudes HTTP
public class CountriesController : ControllerBase
{
    private readonly DataContext _context; //Propiedad de solo lectura para el contexto de datos

    //Crear constructor e inyectar el DataContext (Bd)
    public CountriesController(DataContext context) //Inyección de dependencias clasica - Así se crea la conexión con la BD
    {
        _context = context; //Al usar _context se evita usar this.
    }

    //Crear métodos para crear la lista en BD
    //post para crear, get para traer, put para actualizar, delete para eliminar

    [HttpGet]
    public async Task<IActionResult> GetAsync() //Trae un objeto Country - método asíncrono
    {
        return Ok(await _context.Countries.ToListAsync()); //await es esperar a que se complete la tarea asíncrona
        //Devuelve una respuesta HTTP 200 (OK) con una lista de paises
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(Country country) //Recibe un objeto Country - método asíncrono
    {
        _context.Countries.Add(country); //Agrega el objeto Country al contexto de datos
        await _context.SaveChangesAsync(); //Guarda los cambios en la base de datos
        return Ok(country); //Devuelve una respuesta HTTP 200 (OK) con el objeto Country creado
    }
}