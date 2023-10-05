using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticaParcial.Presentacion
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void ordenDeRetiroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOrdenRetiro f = new FrmOrdenRetiro();
            f.ShowDialog();
        }

        private void reporteDeStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReporte f = new FrmReporte();
            f.ShowDialog();
        }
    }
}
