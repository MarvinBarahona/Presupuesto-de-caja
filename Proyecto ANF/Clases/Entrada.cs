using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_ANF.Clases
{
    public class Entrada
    {
        public decimal saldoInicial;
        public decimal saldoMinimo;
        public decimal precioVenta;
        public int[] politicaCobros;
        public int[] politicaPagos; 
        public int[] proyeccion;
        public decimal otroPago;
        public decimal otroCobro;
        public Produccion produccion;
        public OtroFlujo[] otrosCobros;
        public OtroFlujo[] otrosPagos;
        public Financiamiento[] financiamientos; 

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
            this.politicaCobros = new int[5];
            this.politicaPagos = new int[5];
            
            // Solo agregar a la lista. 
            this.otrosCobros = new OtroFlujo[5];
            this.otrosPagos = new OtroFlujo[5];
            this.financiamientos = new Financiamiento[5];            
        }

        public bool validarPoliticaCobro()
        {
            return this.politicaCobros.Sum() == 100;
        }

        public bool validarPoliticaPago()
        {
            return this.politicaPagos.Sum() == 100;
        }
    }
}
