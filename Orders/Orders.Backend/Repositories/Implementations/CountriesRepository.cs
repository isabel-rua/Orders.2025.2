using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Backend.Helpers;
using Orders.Backend.Repositories.Interfaces;
using Orders.Shared.DTOs;
using Orders.Shared.Entities;
using Orders.Shared.Responses;

namespace Orders.Backend.Repositories.Implementations;

//Implementación
//Esta clase si utiliza las cosas del genérico y tambien implementa la interfaz de países
public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
{
    private readonly DataContext _context;

    public CountriesRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    //Sobreescribir método GetAsync con paginación
    public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _context.Countries
            .Include(c => c.States)
            .AsQueryable();

        return new ActionResponse<IEnumerable<Country>>
        {
            WasSuccess = true,
            Result = await queryable
                .OrderBy(x => x.Name)
                .Paginate(pagination)
                .ToListAsync()
        };
    }

    //Sobreescribir métodos Get de la interfaz ICountriesRepository
    //Se hace así porque es un Get especifico

    public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync()
    {
        var countries = await _context.Countries
            .Include(c => c.States)
            .ToListAsync();
        return new ActionResponse<IEnumerable<Country>>
        {
            WasSuccess = true,
            Result = countries
        };
    }

    public override async Task<ActionResponse<Country>> GetAsync(int id)
    {
        var country = await _context.Countries
             .Include(c => c.States!) //Include de primer nivel, relación directa, si no hay estados ignore
             .ThenInclude(s => s.Cities) // Para relación indirecta se usa ThenInclude
             .FirstOrDefaultAsync(c => c.Id == id); //Ya no sirve el FineAsync porque siempre que se usa Include debe ser con FirstOrDefaultAsync

        if (country == null)
        {
            return new ActionResponse<Country>
            {
                WasSuccess = false,
                Message = "País no existe"
            };
        }

        return new ActionResponse<Country>
        {
            WasSuccess = true,
            Result = country
        };
    }
}