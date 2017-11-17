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
                if (financiamiento != null) financiamientoEntrada.agregarDato(financiamiento.inicio, financiamiento.monto);
            }

            Flujo[] entradas = new Flujo[]{ventasEfectivo, recaudacionCC, financiamientoEntrada, otrosEntrada};
            Flujo flujoE = Flujo.sumar("Entrada de efectivo", entradas);

            Flujo[] salidas = new Flujo[]{gastosFabricacion, pagoCP, financiamientoSalida, otrosSalida};
            Flujo flujoS = Flujo.sumar("Salida de efectivo", salidas);

            //Flujo excedente = Flujo.restar("Efectivo excedente", flujoE, flujoS);
            //Flujo financiamiento = Flujo.quitarNegativos(excedente);

            dgvResultado.Rows.Add(flujoE.toRow(dgvResultado.DefaultCellStyle.Font, Color.Black, false));
            foreach (Flujo flujo in entradas)
            {
                dgvResultado.Rows.Add(flujo.toRow(dgvResultado.DefaultCellStyle.Font, Color.Gray));
            }
            
            dgvResultado.Rows.Add(flujoS.toRow(dgvResultado.DefaultCellStyle.Font, Color.Black, false));
            foreach (Flujo flujo in salidas)
            {
                dgvResultado.Rows.Add(flujo.toRow(dgvResultado.DefaultCellStyle.Font, Color.Gray));
            }
            //dgvResultado.Rows.Add(financiamiento.toRow(dgvResultado.DefaultCellStyle.Font, Color.Red, false, true));
            //dgvResultado.Rows.Add(excedente.toRow(dgvResultado.DefaultCellStyle.Font, Color.Blue, false, true));
        }
    }
}
