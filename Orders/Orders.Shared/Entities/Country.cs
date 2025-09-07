using System.ComponentModel.DataAnnotations;
using Orders.Shared.Interfaces;

namespace Orders.Shared.Entities;

public class Country : IEntityWithName
{
    public int Id { get; set; }

    //Display para mostrar al usuario el nombre como uno quiere "decorado"
    [Display(Name = "País")]

    //MaxLength se usa para definir de una vez la cantidad de caracteres máxima
    /*ErrorMessage se usa para colocar error de una vez como un tipo de validación,
     * al colocar {0} significa variables dentro del data notation, entonces
     * lo reemplaza de una vez por el nombre del campo (país) y con {1} por la cantidad máxima de caractéres (100)*/
    [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres.")]

    // Required para que el campo sea obligatorio
    [Required(ErrorMessage = "El campo {0] es obligatorio.")]
    public string Name { get; set; } = null!; //Condición null! es para no permitir null en ese campo

    //Para crear la relación con State (1:N)
    public ICollection<State>? States { get; set; } //Se coloca ? porque si permite null

    //Propiedad de lectura para saber cantidad de estados del país
    [Display(Name = "Estados/Departamentos")]
    public int StatesNumber => States == null ? 0 : States.Count;
}