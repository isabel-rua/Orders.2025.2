using Microsoft.AspNetCore.Mvc;
using Orders.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.Entities;

namespace Orders.Backend.Controllers;

[ApiController] //Indica que es un controlador de API
[Route("api/[controller]")] //Define la ruta base para las solicitudes HTTP
public class StatesController : GenericController<State>
{
    public StatesController(IGenericUnitOfWork<State> unitOfWork) : base(unitOfWork)
    {
    }
}