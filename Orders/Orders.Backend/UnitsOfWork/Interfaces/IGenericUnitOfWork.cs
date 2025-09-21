using Orders.Shared.DTOs;
using Orders.Shared.Responses;

namespace Orders.Backend.UnitsOfWork.Interfaces;

public interface IGenericUnitOfWork<T> where T : class //clase genérica
{
    //Tiene los mismos métodos que el repositorio genérico
    //Se llama polimorfismo por interfaces

    Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);

    //Get con parametro, devuelve un objeto de tipo T
    Task<ActionResponse<T>> GetAsync(int id);

    //Get sin parametro, devuelve una lista IEnumerable de objetos que yo pida
    Task<ActionResponse<IEnumerable<T>>> GetAsync();

    //Método Add, recibe una entidad y lo agrega a la base de datos
    Task<ActionResponse<T>> AddAsync(T entity);

    //Método Delete, recibe un id y elimina la entidad correspondiente de la base de datos
    Task<ActionResponse<T>> DeleteAsync(int id);

    //Método Update, recibe una entidad y actualiza la información en la base de datos
    Task<ActionResponse<T>> UpdateAsync(T entity);
}