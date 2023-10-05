using PracticaParcial.Datos.Interfaz;
using PracticaParcial.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaParcial.Datos.Implementacion
{
    public class OrdenDao : IOrdenDao
    {
        public bool Crear(OrdenRetiro oOrden)
        {
            bool resultado = true;
            SqlConnection conexion = HelperDao.ObtenerInstancia().ObtenerConexion();
            SqlTransaction t = null;
            try
            {
                conexion.Open();
                t = conexion.BeginTransaction();

                SqlCommand comando = new SqlCommand();
                comando.Connection = conexion;
                comando.Transaction = t;
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "SP_INSERTAR_ORDEN";

                comando.Parameters.AddWithValue("@responsable", oOrden.Responsable);

                SqlParameter parametroNro = new SqlParameter("@nro", SqlDbType.Int);
                parametroNro.Direction = ParameterDirection.Output;
                comando.Parameters.Add(parametroNro);

                comando.ExecuteNonQuery();

                int presupuestoNro = (int)parametroNro.Value;
                int detalleNro = 1;

                SqlCommand cmdDetalle;
                foreach (DetalleOrden dp in oOrden.Detalles)
                {
                    cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLES", conexion, t);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;

                    cmdDetalle.Parameters.AddWithValue("@nro_orden", presupuestoNro);
                    cmdDetalle.Parameters.AddWithValue("@detalle", detalleNro);
                    cmdDetalle.Parameters.AddWithValue("@codigo", dp.Material.Codigo);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", dp.Cantidad);

                    cmdDetalle.ExecuteNonQuery();
                    detalleNro++;
                }

                t.Commit();
            }
            catch
            {
                if (t != null)
                    t.Rollback();
                resultado = false;
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

            return resultado;
        }


        public List<Material> ObtenerMateriales()
        {
            List<Material> lMateriales = new List<Material>();
            DataTable tabla = HelperDao.ObtenerInstancia().Consultar("SP_CONSULTAR_MATERIALES");
            foreach (DataRow fila in tabla.Rows)
            {
                int codigo = int.Parse(fila["codigo"].ToString());
                string nombre = fila["nombre"].ToString();
                decimal stock = decimal.Parse(fila["stock"].ToString());
                Material m = new Material(codigo, nombre, stock);
                lMateriales.Add(m);
            }
            return lMateriales;
        }
    }

}

