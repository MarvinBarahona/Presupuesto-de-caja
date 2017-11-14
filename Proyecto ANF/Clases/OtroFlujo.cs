using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_ANF.Clases
{
    class OtroFlujo
    {
        private bool _entrada;
        public bool entrada
        {
            get { return _entrada; }
            set { _entrada = value; }
        }

        private Mes _mes;
        public Mes mes
        {
            get { return _mes; }
            set { _mes = value; }
        }

        private float _monto;
        public float monto
        {
            get { return _monto; }
            set { _monto = value; }
        }

        private OtroFlujo() { }

        public OtroFlujo(bool entrada, Mes mes, float monto)
        {
            this._entrada = entrada;
            this._mes = mes;
            this._monto = monto;
        }
    }
}
