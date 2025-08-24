using System.ComponentModel.DataAnnotations;

namespace Orders.Shared.Entities;

public class Country
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
}