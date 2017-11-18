using Microsoft.VisualBasic;
using Proyecto_ANF.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_ANF.Forms
{
    public partial class FormResultado : Form
    {
        Entrada entrada; 

        public FormResultado(Entrada entrada)
        {
            InitializeComponent();
            this.entrada = entrada; 
        }

        private void FormResultado_Load(object sender, EventArgs e)
        {
            Flujo ventasEfectivo = new Flujo("Ventas en efectivo", 0);
            Flujo recaudacionCC = new Flujo("Recaudación de CxC", 0);
            Flujo financiamientoEntrada = new Flujo("Financiamiento", 0);
            Flujo otrosEntrada = new Flujo("Otros ingresos", this.entrada.otroCobro);

            Flujo gastosFabricacion = new Flujo("Gastos de fabricación", 
                this.entrada.produccion.manoObra + this.entrada.produccion.costoIndirecto + 
                this.entrada.produccion.materiaPrima * this.entrada.politicaPagos[0] / 100);
            Flujo pagoCP = new Flujo("Pago de CxP", 0);
            Flujo financiamientoSalida = new Flujo("Pago de interés", 0);
            Flujo otrosSalida = new Flujo("Otros pagos", this.entrada.otroPago);

            for (int i = 0; i < 12; i++)
            {
                ventasEfectivo.agregarDato(i, 
                    this.entrada.proyeccion[i] * this.entrada.precioVenta * this.entrada.politicaCobros[0] / 100);

                if (i > 0)
                {
                    recaudacionCC.agregarDato(i, 
                        this.entrada.proyeccion[i - 1] * this.entrada.precioVenta * this.entrada.politicaCobros[1] / 100);
                    pagoCP.agregarDato(i, this.entrada.produccion.materiaPrima * this.entrada.politicaPagos[1] / 100);
                }

                if (i > 1)
                {
                    recaudacionCC.agregarDato(i,
                       this.entrada.proyeccion[i - 2] * this.entrada.precioVenta * this.entrada.politicaCobros[2] / 100);
                    pagoCP.agregarDato(i, this.entrada.produccion.materiaPrima * this.entrada.politicaPagos[2] / 100);
                }

                if (i > 2)
                {
                    recaudacionCC.agregarDato(i,
                       this.entrada.proyeccion[i - 3] * this.entrada.precioVenta * this.entrada.politicaCobros[3] / 100);
                    pagoCP.agregarDato(i, this.entrada.produccion.materiaPrima * this.entrada.politicaPagos[3] / 100);
                }

                if (i > 3)
                {
                    recaudacionCC.agregarDato(i,
                       this.entrada.proyeccion[i - 4] * this.entrada.precioVenta * this.entrada.politicaCobros[4] / 100);
                    pagoCP.agregarDato(i, this.entrada.produccion.materiaPrima * this.entrada.politicaPagos[4] / 100);
                }
            }

            foreach(OtroFlujo otro in this.entrada.otrosCobros)
            {
                if (otro != null) otrosEntrada.agregarDato(otro.mes, otro.monto);
            }

            foreach (OtroFlujo otro in this.entrada.otrosPagos)
            {
                if (otro != null) otrosSalida.agregarDato(otro.mes, otro.monto);
            }

            foreach (Financiamiento financiamiento in this.entrada.financiamientos)
            {
                if (financiamiento != null)
                {
                    financiamientoEntrada.agregarDato(financiamiento.inicio, financiamiento.monto);

                    decimal tasa = financiamiento.tasa / 100 * ((decimal)((int)financiamiento.tipo) / 12);
                    double n = (double)financiamiento.plazo / (int)financiamiento.tipo;
                    
                    double interes = Financial.Pmt((double)tasa, n, (double)(-financiamiento.monto));

                    int i = 1;
                    int j = (int)financiamiento.inicio + (int)financiamiento.tipo * i++;
                    while (j < 12)
                    {
                        financiamientoSalida.agregarDato(j, (decimal)interes);
                        j = (int)financiamiento.inicio + (int)financiamiento.tipo * i++;
                    }
                }
            }


            Flujo[] entradas = new Flujo[]{ventasEfectivo, recaudacionCC, financiamientoEntrada, otrosEntrada};
            Flujo flujoE = Flujo.sumar("Entrada de efectivo", entradas);

            Flujo[] salidas = new Flujo[]{gastosFabricacion, pagoCP, financiamientoSalida, otrosSalida};
            Flujo flujoS = Flujo.sumar("Salida de efectivo", salidas);

            Flujo flujoNeto = Flujo.restar("Flujo de efectivo neto", flujoE, flujoS);

            Flujo montoInicial = new Flujo("Saldo inicial", 0);
            Flujo montoFinal = new Flujo("Saldo final", 0);

            montoInicial.agregarDato(0, this.entrada.saldoInicial);
            montoFinal.agregarDato(0, montoInicial.obtenerDato(0) + flujoNeto.obtenerDato(0));

            for (int i = 1; i < 12; i++)
            {
                montoInicial.agregarDato(i, montoFinal.obtenerDato(i - 1));
                montoFinal.agregarDato(i, montoInicial.obtenerDato(i) + flujoNeto.obtenerDato(i));
            }

            Flujo saldoMin = new Flujo("Saldo mínimo", this.entrada.saldoMinimo);

            Flujo excedente = Flujo.restar("Efectivo excedente", montoFinal, saldoMin);
            Flujo financiar = Flujo.quitarNegativos(excedente);

            dgvResultado.Rows.Add(flujoE.toRow(dgvResultado.DefaultCellStyle.Font, Color.Black, false, true));
            foreach (Flujo flujo in entradas)
            {
                dgvResultado.Rows.Add(flujo.toRow(dgvResultado.DefaultCellStyle.Font, Color.Gray, true));
            }
            
            dgvResultado.Rows.Add(flujoS.toRow(dgvResultado.DefaultCellStyle.Font, Color.Black, false, true));
            foreach (Flujo flujo in salidas)
            {
                dgvResultado.Rows.Add(flujo.toRow(dgvResultado.DefaultCellStyle.Font, Color.Gray, true));
            }

            dgvResultado.Rows.Add(flujoNeto.toRow(dgvResultado.DefaultCellStyle.Font, Color.Black, false, true));

            dgvResultado.Rows.Add(montoInicial.toRow(dgvResultado.DefaultCellStyle.Font, Color.Black));

            dgvResultado.Rows.Add(montoFinal.toRow(dgvResultado.DefaultCellStyle.Font, Color.Black, false, true));

            dgvResultado.Rows.Add(saldoMin.toRow(dgvResultado.DefaultCellStyle.Font, Color.Black));

            dgvResultado.Rows.Add(financiar.toRow(dgvResultado.DefaultCellStyle.Font, Color.Red, false, true));
            dgvResultado.Rows.Add(excedente.toRow(dgvResultado.DefaultCellStyle.Font, Color.Blue, false, true));
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Bitmap bitmap = new Bitmap(dgvResultado.Width, dgvResultado.Height);
                dgvResultado.DrawToBitmap(bitmap, new Rectangle(0, 0, dgvResultado.Width, dgvResultado.Height));
                bitmap.Save(saveFileDialog1.FileName);
                
                MessageBox.Show("Archivo guardado", "Éxito en la operación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
