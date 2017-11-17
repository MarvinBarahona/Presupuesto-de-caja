using Proyecto_ANF.Clases;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_ANF.Forms
{
    class Flujo
    {
        string titulo;
        decimal[] datos;

        public Flujo(string titulo, decimal montoInicial)
        {
            this.titulo = titulo;
            this.datos = new decimal[12];

            for (int i = 0; i < 12; i++)
            {
                this.datos[i] = montoInicial;
            }
        }

        public void agregarDato(Mes mes, decimal monto)
        {
            int i = (int)mes;
            this.agregarDato(i, monto);
        }

        public void agregarDato(int i, decimal monto)
        {
            this.datos[i] += monto;
        }

        public DataGridViewRow toRow(Font fuenteBase, Color color, bool cursiva = true, bool negrita = false)
        {
            DataGridViewRow row = new DataGridViewRow();
            
            if(cursiva) row.DefaultCellStyle.Font = new Font(fuenteBase, FontStyle.Italic);
            if (negrita) row.DefaultCellStyle.Font = new Font(fuenteBase, FontStyle.Bold);

            row.DefaultCellStyle.ForeColor = color;


            DataGridViewCell cellTitulo = new DataGridViewTextBoxCell();
            cellTitulo.Value = this.titulo;
            row.Cells.Add(cellTitulo);

            for (int i = 0; i < 12; i++)
            {
                DataGridViewCell cellDato = new DataGridViewTextBoxCell();
                if (this.datos[i] != 0) cellDato.Value = this.datos[i];
                else cellDato.Value = null;
                row.Cells.Add(cellDato);
            }

            return row;
        }

        public static Flujo sumar(string titulo, Flujo[] flujos)
        {
            Flujo resultado = new Flujo(titulo, 0);

            for (int i = 0; i < 12; i++)
            {
                foreach (Flujo sumando in flujos)
                {
                    resultado.agregarDato(i, sumando.datos[i]);
                }
            }

            return resultado;
        }

        public static Flujo restar(string titulo, Flujo minuendo, Flujo sustraendo)
        {
            Flujo resultado = new Flujo(titulo, 0);

            for (int i = 0; i < 12; i++)
            {
                resultado.agregarDato(i, minuendo.datos[i]);
                resultado.agregarDato(i, -sustraendo.datos[i]);
            }

            return resultado;
        }

        public static Flujo quitarNegativos(Flujo requerido)
        {
            Flujo excedente = new Flujo("Financiamiento necesario", 0);

            for (int i = 0; i < 12; i++)
            {
                if (requerido.datos[i] < 0)
                {
                    excedente.agregarDato(i, -requerido.datos[i]);
                    requerido.datos[i] = 0;
                }
                else
                {
                    excedente.datos[i] = 0;
                }
            }

            return excedente;
        }
    }
}
