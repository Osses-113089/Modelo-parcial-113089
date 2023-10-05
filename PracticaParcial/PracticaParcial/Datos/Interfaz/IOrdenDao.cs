using PracticaParcial.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaParcial.Datos.Interfaz
{
    interface IOrdenDao
    {
        bool Crear(OrdenRetiro oOrden);
        List<Material> ObtenerMateriales();
    }
}
