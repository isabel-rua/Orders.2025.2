using Orders.Shared.DTOs;
using Orders.Shared.Entities;
using Orders.Shared.Responses;

namespace Orders.Backend.Repositories.Interfaces;

public interface ICountriesRepository
{
    //Tiene los mismos métodos que el repositorio genérico pero hay 2 métodos que son diferentes:
    //La sobrecarga de los dos Get (Get por id y el Get de todos)
    //Tambien se definen en la unidad de trabajo genérica
    Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<Country>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<Country>>> GetAsync();
}