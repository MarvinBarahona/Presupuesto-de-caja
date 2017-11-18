using Newtonsoft.Json;
using Proyecto_ANF.Clases;
using Proyecto_ANF.Forms;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Proyecto_ANF
{
    public partial class FormIngreso : Form
    {
        private Entrada entrada;

        private NumericUpDown[] proyeccionInput;
        private NumericUpDown[] cobrosInput;
        private NumericUpDown[] pagosInput;

        private Button[] cobrosButtons;
        private Button[] pagosButtons;
        private Button[] financiamientosButtons;

        public FormIngreso()
        {
            InitializeComponent();
            this.entrada = new Entrada();

            this.proyeccionInput = new NumericUpDown[] {
                nudMes0, nudMes1, nudMes2, nudMes3, 
                nudMes4, nudMes5, nudMes6, nudMes7, 
                nudMes8, nudMes9, nudMes10, nudMes11, 
            };

            this.cobrosInput = new NumericUpDown[] {
                nudPCobroE, nudPCobro1, nudPCobro2, nudPCobro3, nudPCobro4
            };

            this.pagosInput = new NumericUpDown[] {
                nudPPagoE, nudPPago1, nudPPago2, nudPPago3, nudPPago4
            };

            this.cobrosButtons = new Button[]{
                btnIngreso1, btnIngreso2, btnIngreso3, btnIngreso4, btnIngreso5
            };

            this.pagosButtons = new Button[]{
                btnPago1, btnPago2, btnPago3, btnPago4, btnPago5
            };

            this.financiamientosButtons = new Button[]{
                btnFinanciamiento1, btnFinanciamiento2, btnFinanciamiento3, btnFinanciamiento4, btnFinanciamiento5
            };
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (asignarDatos())
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = (FileStream)saveFileDialog1.OpenFile();
                    string objeto = JsonConvert.SerializeObject(this.entrada);
                    Byte[] info = new UTF8Encoding(true).GetBytes(objeto);
                    fs.Write(info, 0, info.Length);
                    fs.Close();
                    MessageBox.Show("Archivo guardado", "Éxito en la operación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Stream fs = openFileDialog1.OpenFile();
                StreamReader reader = new StreamReader(fs);

                string objeto = reader.ReadLine();
                reader.Close();

                this.entrada = JsonConvert.DeserializeObject<Entrada>(objeto);

                asignarInputs();

                MessageBox.Show("Archivo cargado", "Éxito en la operación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void asignarInputs()
        {
            nudSaldoInicial.Value = this.entrada.saldoInicial;
            nudSaldoMinimo.Value = this.entrada.saldoMinimo;
            nudPrecioVenta.Value = this.entrada.precioVenta;
            nudOtroPagoM.Value = this.entrada.otroPago;
            nudOtroIngresoM.Value = this.entrada.otroCobro;

            for (int i = 0; i < this.proyeccionInput.Length; i++)
            {
                proyeccionInput[i].Value = this.entrada.proyeccion[i];
            }

            nudMO.Value = this.entrada.produccion.manoObra;
            nudMP.Value = this.entrada.produccion.materiaPrima;
            nudCIF.Value = this.entrada.produccion.costoIndirecto;

            for (int i = 0; i < this.cobrosInput.Length; i++)
            {
                cobrosInput[i].Value = this.entrada.politicaCobros[i];
            }

            for (int i = 0; i < this.pagosInput.Length; i++)
            {
                pagosInput[i].Value = this.entrada.politicaPagos[i];
            }

            for (int i = 0; i < this.cobrosButtons.Length; i++)
            {
                this.cobrosButtons[i].ForeColor = this.entrada.otrosCobros[i] != null? Color.Green : Color.Red;
            }

            for (int i = 0; i < this.pagosButtons.Length; i++)
            {
                this.pagosButtons[i].ForeColor = this.entrada.otrosPagos[i] != null? Color.Green : Color.Red;
            }

            for (int i = 0; i < this.financiamientosButtons.Length; i++)
            {
                this.financiamientosButtons[i].ForeColor = this.entrada.financiamientos[i] != null? Color.Green : Color.Red;
            }
        }

        private bool asignarDatos()
        {
            bool r = false;

            this.entrada.saldoInicial = nudSaldoInicial.Value;
            this.entrada.saldoMinimo = nudSaldoMinimo.Value;
            this.entrada.precioVenta = nudPrecioVenta.Value;
            this.entrada.otroPago = nudOtroPagoM.Value;
            this.entrada.otroCobro = nudOtroIngresoM.Value;

            for (int i = 0; i < this.proyeccionInput.Length; i++)
            {
                this.entrada.proyeccion[i] = (int)proyeccionInput[i].Value;
            }

            this.entrada.produccion.manoObra = nudMO.Value;
            this.entrada.produccion.materiaPrima = nudMP.Value;
            this.entrada.produccion.costoIndirecto = nudCIF.Value;

            for (int i = 0; i < this.cobrosInput.Length; i++)
            {
                this.entrada.politicaCobros[i] = (int)cobrosInput[i].Value;
            }

            for (int i = 0; i < this.pagosInput.Length; i++)
            {
                this.entrada.politicaPagos[i] = (int)pagosInput[i].Value;
            }

            if (!this.entrada.validarPoliticaCobro())
            {
                MessageBox.Show("La suma de la política de cobro debe ser 100", "Error en la entrada de datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!this.entrada.validarPoliticaPago())
            {
                MessageBox.Show("La suma de la política de pago debe ser 100", "Error en la entrada de datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                r = true;
            }

            return r;
        }

        private void btnIngreso_Click(object sender, EventArgs e)
        {
            int index = Array.IndexOf(this.cobrosButtons, sender);

            using (FormAgregarOtro agregarForm = new FormAgregarOtro(this.entrada.otrosCobros[index], TipoOtro.Cobro))
            {
                agregarForm.ShowDialog();

                if (agregarForm.result == 'a')
                {
                    this.cobrosButtons[index].ForeColor = Color.Green;
                    this.entrada.otrosCobros[index] = agregarForm.otro;
                }

                if (agregarForm.result == 'e')
                {
                    this.cobrosButtons[index].ForeColor = Color.Red;
                    this.entrada.otrosCobros[index] = null;
                }
            }            
        }

        private void btnPago_Click(object sender, EventArgs e)
        {
            int index = Array.IndexOf(this.pagosButtons, sender);

            using (FormAgregarOtro agregarForm = new FormAgregarOtro(this.entrada.otrosPagos[index], TipoOtro.Pago))
            {
                agregarForm.ShowDialog();

                if (agregarForm.result == 'a')
                {
                    this.pagosButtons[index].ForeColor = Color.Green;
                    this.entrada.otrosPagos[index] = agregarForm.otro;
                }

                if (agregarForm.result == 'e')
                {
                    this.pagosButtons[index].ForeColor = Color.Red;
                    this.entrada.otrosPagos[index] = null;
                }
            }  
        }

        private void btnFinanciamiento_Click(object sender, EventArgs e)
        {
            int index = Array.IndexOf(this.financiamientosButtons, sender);

            using (FormAgregarFinanciamiento agregarForm = new FormAgregarFinanciamiento(this.entrada.financiamientos[index]))
            {
                agregarForm.ShowDialog();

                if (agregarForm.result == 'a')
                {
                    this.financiamientosButtons[index].ForeColor = Color.Green;
                    this.entrada.financiamientos[index] = agregarForm.financiamiento;
                }

                if (agregarForm.result == 'e')
                {
                    this.financiamientosButtons[index].ForeColor = Color.Red;
                    this.entrada.financiamientos[index] = null;
                }
            }  
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (this.asignarDatos())
            {
                using (FormResultado formResultado = new FormResultado(this.entrada))
                {
                    formResultado.ShowDialog();
                }  
            }            
        }        
    }
}
