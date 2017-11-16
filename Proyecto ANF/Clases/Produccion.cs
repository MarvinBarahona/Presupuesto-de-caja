using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_ANF.Clases
{
    public class Produccion
    {
        public decimal manoObra;
        public decimal materiaPrima;
        public decimal costoIndirecto;

        public Produccion()
        {
            this.manoObra = 0;
            this.materiaPrima = 0;
            this.costoIndirecto = 0; 
        }
    }
}
