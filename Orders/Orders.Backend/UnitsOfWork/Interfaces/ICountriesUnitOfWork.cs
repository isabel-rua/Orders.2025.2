using Orders.Shared.Entities;
using Orders.Shared.Responses;

namespace Orders.Backend.UnitsOfWork.Interfaces;

//La unidad de trabajao tiene 2 cosas propias de países (taer un país por id y traer todos los países)
//El Put, Post y Delete se heredan de la unidad de trabajo genérica
public interface ICountriesUnitOfWork
{
    Task<ActionResponse<Country>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<Country>>> GetAsync();
}