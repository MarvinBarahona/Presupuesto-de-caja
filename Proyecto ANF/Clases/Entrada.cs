using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_ANF.Clases
{
    class Entrada
    {
        public float saldoInicial;
        public float saldoMinimo;
        public float precioVenta;
        public List<int> politicaCobros;
        public List<int> politicaPagos; 
        public int[] proyeccion;
        public float otroPago;
        public float otroCobro;
        public Produccion produccion;
        public List<OtroFlujo> otrosFlujos;
        public List<Financiamiento> financiamientos; 

        public Entrada()
        {
            // Estos campos son se agregan, no es necesario validar. 
            this.saldoInicial = 0; 
            this.saldoMinimo = 0; 
            this.precioVenta = 0; 
            this.otroPago = 0; 
            this.otroCobro = 0;

            this.proyeccion = new int[12];

            this.produccion = new Produccion();

            //Validar suma menor o igual a 100. 
            this.politicaCobros = new List<int>();
            this.politicaCobros.Add(100); 

            this.politicaPagos = new List<int>();
            this.politicaPagos.Add(100);
            
            // Solo agregar a la lista. 
            this.otrosFlujos = new List<OtroFlujo>();
            this.financiamientos = new List<Financiamiento>();
            
        }

        public bool agregarPoliticaCobro(int porcentaje)
        {
            bool r = false;
            if (this.politicaCobros.Sum() + porcentaje <= 100)
            {
                this.politicaCobros.Add(porcentaje); 
            }

            return r; 
        }

        public bool validarPoliticaCobro()
        {
            return this.politicaCobros.Sum() == 100;
        }

        public bool agregarPoliticaPago(int porcentaje)
        {
            bool r = false;
            if (this.politicaPagos.Sum() + porcentaje <= 100)
            {
                this.politicaPagos.Add(porcentaje);
            }

            return r;
        }

        public bool validarPoliticaPago()
        {
            return this.politicaPagos.Sum() == 100;
        }

        public void agregarFlujo(bool entrada, Mes mes, float monto)
        {
            this.otrosFlujos.Add(new OtroFlujo(entrada, mes, monto));
        }

        public void agregarFinanciamiento(Mes inicio, int plazo, float tasa, TipoFinanciamiento tipo, float monto)
        {
            this.financiamientos.Add(new Financiamiento(inicio, plazo, tasa, tipo, monto));
        }
    }
}
