using PracticaParcial.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaParcial.Servicios.Interfaz
{
    interface IServicio
    {
        bool CrearOrden(OrdenRetiro oOrden);
        List<Material> TraerMateriales();
    }
}
