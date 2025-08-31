using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Shared.Responses;

public class ActionResponse<T>
{
    //Propiedad 1: indica si la acción fue exitosa devuelve true o false
    public bool WasSuccess { get; set; }

    //Propiedad 2: si algo fallo se devuelve un mensaje
    public string? Message { get; set; }

    //Propiedad 3: se esta devolviendo una propiedad de lo que le mande (tipo T), cualquier tipo de dato
    public T? Result { get; set; }
}