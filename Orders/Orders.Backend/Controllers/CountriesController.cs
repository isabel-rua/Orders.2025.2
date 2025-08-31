using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.Entities;

namespace Orders.Backend.Controllers;

[ApiController] //Indica que es un controlador de API
[Route("api/[controller]")] //Define la ruta base para las solicitudes HTTP
public class CountriesController : GenericController<Country>
{
    //En el constructor se inyecta la unidad de trabajo genérica y
    //esa unidad genérica se la paso al controlador de la clase base
    public CountriesController(IGenericUnitOfWork<Country> unitOfWork) : base(unitOfWork)
    {
    }
}