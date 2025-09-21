using Orders.Shared.DTOs;

namespace Orders.Backend.Helpers;

public static class QueryableExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDTO pagination)
    {
        return queryable
            .Skip((pagination.Page - 1) * pagination.RecordsNumber) //Skip se salta los registros de las páginas anteriores
            .Take(pagination.RecordsNumber); //Take toma la cantidad de registros que se van a mostrar en la página actual
    }
}