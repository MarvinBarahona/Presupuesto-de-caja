using Proyecto_ANF.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_ANF.Forms
{
    public partial class FormAgregarOtro : Form
    {
        public OtroFlujo otro;
        TipoOtro tipo;

        public char result; 

        public FormAgregarOtro(OtroFlujo otro, TipoOtro tipo)
        {
            InitializeComponent();
            this.otro = otro;
            this.tipo = tipo;
            result = 'o';

            cmbMes.DataSource = Enum.GetValues(typeof(Mes));
            
            if(otro != null)
            {
                cmbMes.SelectedItem = otro.mes;
                nudMonto.Value = otro.monto;
            }            
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (nudMonto.Value == 0)
            {
                MessageBox.Show("Ingrese todos los campos", "Error en el ingreso de datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Mes mes; 
                Enum.TryParse<Mes>(cmbMes.SelectedItem.ToString(), out mes);
                this.otro = new OtroFlujo(tipo, mes, nudMonto.Value);
                this.result = 'a'; 
                this.Close();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            this.result = 'e';
            this.Close();
        }
    }
}
