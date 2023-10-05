using PracticaParcial.Datos.Implementacion;
using PracticaParcial.Entidades;
using PracticaParcial.Servicios.Implementacion;
using PracticaParcial.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticaParcial
{
    public partial class FrmOrdenRetiro : Form
    {
        IServicio servicio = null;
        OrdenRetiro nuevo = null;
        public FrmOrdenRetiro()
        {
            InitializeComponent();
            servicio = new Servicio();
            nuevo = new OrdenRetiro();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            dtpFecha.Text = DateTime.Today.ToShortDateString();
            txtBoxResponsable.Text = "Responsable 1";
            nudCantidad.Text = "1";
            CargarMateriales();

        }

        private void CargarMateriales()
        {
            cboMateriales.DataSource = servicio.TraerMateriales();
            cboMateriales.ValueMember = "Codigo";
            cboMateriales.DisplayMember = "Nombre";
        }
        private void GrabarOrden()
        {
            nuevo.Fecha = Convert.ToDateTime(dtpFecha.Text);
            nuevo.Responsable = txtBoxResponsable.Text;
            if (servicio.CrearOrden(nuevo))
            {
                MessageBox.Show("Se registró con éxito el presupuesto...", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("NO se pudo registrar el presupuesto...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void brnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Desea cancelar? Ningun dato será guardado.", "Cancelar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBoxResponsable.Text))
            {
                MessageBox.Show("Debe ingresar un responsable...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Debe ingresar al menos un detalle...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            GrabarOrden();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cboMateriales.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un material...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(nudCantidad.Text) || !int.TryParse(nudCantidad.Text, out _))
            {
                MessageBox.Show("Debe ingresar una cantidad válida...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["ColMaterial"].Value.ToString().Equals(cboMateriales.Text))
                {
                    MessageBox.Show("Este producto ya está presupuestado...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            Material m = (Material)cboMateriales.SelectedItem;

            int cant = Convert.ToInt32(nudCantidad.Text);
            DetalleOrden detalle = new DetalleOrden(m, cant);

            nuevo.AgregarDetalle(detalle);
            dataGridView1.Rows.Add(new object[] { m.Codigo, m.Nombre, m.Stock, cant, "Quitar" });

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 4)
            {
                nuevo.QuitarDetalle(dataGridView1.CurrentRow.Index);
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            }
        }

        private void cboMateriales_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
