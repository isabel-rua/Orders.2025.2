using Orders.Backend.Repositories.Implementations;
using Orders.Backend.Repositories.Interfaces;
using Orders.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.DTOs;
using Orders.Shared.Entities;
using Orders.Shared.Responses;

namespace Orders.Backend.UnitsOfWork.Implementations;

//Implementación de unidad de trabajo que tiene los dos métodos especificos difernete al genérico
public class CountriesUnitOfWork : GenericUnitOfWork<Country>, ICountriesUnitOfWork
{
    private readonly ICountriesRepository _countriesRepository;

    public CountriesUnitOfWork(IGenericRepository<Country> repository, ICountriesRepository
        countriesRepository) : base(repository)
    {
        _countriesRepository = countriesRepository;
    }

    public override async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination) => await
        _countriesRepository.GetTotalRecordsAsync(pagination);

    //Sobreescribir método GetAsync con paginación
    public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination) => await
        _countriesRepository.GetAsync(pagination);

    //Sobreescribir métodos Get de la interfaz ICountriesRepository
    //Se hace así porque es un Get especific

    public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync() => await
        _countriesRepository.GetAsync();

    public override async Task<ActionResponse<Country>> GetAsync(int id) => await
        _countriesRepository.GetAsync(id);
}