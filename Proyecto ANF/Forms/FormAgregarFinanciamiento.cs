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
    public partial class FormAgregarFinanciamiento : Form
    {
        public Financiamiento financiamiento;
        public char result; 

        public FormAgregarFinanciamiento(Financiamiento financiamiento)
        {
            InitializeComponent();
            this.financiamiento = financiamiento;

            result = 'o';

            cmbMes.DataSource = Enum.GetValues(typeof(Mes));
            cmbPeriodo.DataSource = Enum.GetValues(typeof(TipoFinanciamiento));

            if (financiamiento != null)
            {
                nudPlazo.Value = financiamiento.plazo;
                cmbMes.SelectedItem = financiamiento.inicio;
                nudMonto.Value = financiamiento.monto;
                nudTasa.Value = financiamiento.tasa;
                cmbPeriodo.SelectedItem = financiamiento.tipo;
            } 
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (nudMonto.Value == 0)
            {
                MessageBox.Show("Ingrese todos los campos", "Error en el ingreso de datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Mes mes;
                Enum.TryParse<Mes>(cmbMes.SelectedItem.ToString(), out mes);

                TipoFinanciamiento tipo;
                Enum.TryParse<TipoFinanciamiento>(cmbPeriodo.SelectedItem.ToString(), out tipo);

                this.financiamiento = new Financiamiento(mes, (int)nudPlazo.Value, nudTasa.Value, tipo, nudMonto.Value);
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
