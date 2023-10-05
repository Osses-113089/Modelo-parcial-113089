using PracticaParcial.Datos.Implementacion;
using PracticaParcial.Datos.Interfaz;
using PracticaParcial.Entidades;
using PracticaParcial.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaParcial.Servicios.Implementacion
{
    public class Servicio : IServicio
    {
        private IOrdenDao dao;
        public Servicio()
        {
            dao = new OrdenDao();
        }

        public bool CrearOrden(OrdenRetiro oOrden)
        {
            return dao.Crear(oOrden);
                
        }

        public List<Material> TraerMateriales()
        {
            return dao.ObtenerMateriales();
        }
    }
}
