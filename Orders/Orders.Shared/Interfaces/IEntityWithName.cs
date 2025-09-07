using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Shared.Interfaces
{
    //Esta interfaz es para las entidades que tienen un nombre
    //Todas las entidades que implementen esta interfaz deben tener una propiedad Name
    public interface IEntityWithName
    {
        string Name { get; set; }
    }
}