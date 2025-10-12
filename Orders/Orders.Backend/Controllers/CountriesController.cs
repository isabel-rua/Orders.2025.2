using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.DTOs;
using Orders.Shared.Entities;

namespace Orders.Backend.Controllers;

[ApiController] //Indica que es un controlador de API
[Route("api/[controller]")] //Define la ruta base para las solicitudes HTTP
public class CountriesController : GenericController<Country>
{
    private readonly ICountriesUnitOfWork _countriesUnitOfWork;

    //En el constructor se inyecta la unidad de trabajo genérica y
    //esa unidad genérica se la paso al controlador de la clase base
    //y porque los métodos Get son especificos se inyecta la unidad de trabajo especifica de Countries
    public CountriesController(IGenericUnitOfWork<Country> unitOfWork, ICountriesUnitOfWork
        countriesUnitOfWork) : base(unitOfWork)
    {
        _countriesUnitOfWork = countriesUnitOfWork;
    }

    [HttpGet("totalRecords")]
    public override async Task<IActionResult> GetTotalRecordsAsync([FromQuery] PaginationDTO pagination)
    {
        var action = await _countriesUnitOfWork.GetTotalRecordsAsync(pagination);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest();
    }

    //Sobreescribir el Get con paginación
    [HttpGet("paginated")]
    public override async Task<IActionResult> GetAsync(PaginationDTO pagination)
    {
        var response = await _countriesUnitOfWork.GetAsync(pagination);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    //Sobreescribir los dos Get
    [HttpGet]
    public override async Task<IActionResult> GetAsync()
    {
        var action = await _countriesUnitOfWork.GetAsync();
        if (action.WasSuccess)
        {
            return Ok(action.Result);//Devuelve un código 200 con la lista de objetos
        }
        return BadRequest(action.Message); //Devuelve un error 400 (error genérico) con el mensaje de error
    }

    [HttpGet("{id}")]
    public override async Task<IActionResult> GetAsync(int id)
    {
        var action = await _countriesUnitOfWork.GetAsync(id);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return NotFound(); //Devuelve un error 404 (no encontrado)
    }
}