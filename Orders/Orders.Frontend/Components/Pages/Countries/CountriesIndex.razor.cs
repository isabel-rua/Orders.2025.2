using Microsoft.AspNetCore.Components;
using Orders.Frontend.Repositories;
using Orders.Shared.Entities;

namespace Orders.Frontend.Components.Pages.Countries
{
    public partial class CountriesIndex
    {
        [Inject] private IRepository Repository { get; set; } = null!;

        //Lista de países
        private List<Country>? countries;

        //Para consumir el API de países no paginado
        protected override async Task OnInitializedAsync()
        {
            var httResult = await Repository.GetAsync<List<Country>>("/api/countries");
            countries = httResult.Response;
        }
    }
}