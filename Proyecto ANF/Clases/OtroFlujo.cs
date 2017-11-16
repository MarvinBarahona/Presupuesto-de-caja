using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_ANF.Clases
{
    public class OtroFlujo
    {
        private TipoOtro _tipo;
        public TipoOtro tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        private Mes _mes;
        public Mes mes
        {
            get { return _mes; }
            set { _mes = value; }
        }

        private decimal _monto;
        public decimal monto
        {
            get { return _monto; }
            set { _monto = value; }
        }

        private OtroFlujo() { }

        public OtroFlujo(TipoOtro tipo, Mes mes, decimal monto)
        {
            this._tipo = tipo;
            this._mes = mes;
            this._monto = monto;
        }
    }
}
