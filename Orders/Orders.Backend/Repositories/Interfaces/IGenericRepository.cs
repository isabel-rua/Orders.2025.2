using Orders.Shared.Responses;

namespace Orders.Backend.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
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
}