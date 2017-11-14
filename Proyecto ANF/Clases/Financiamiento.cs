using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_ANF.Clases
{
    class Financiamiento
    {
        Mes _inicio;
        public Mes inicio
        {
            get { return _inicio; }
            set { _inicio = value; }
        }

        int _plazo;
        public int plazo
        {
            get { return _plazo; }
            set { _plazo = value; }
        }

        float _tasa;
        public float tasa
        {
            get { return _tasa; }
            set { _tasa = value; }
        }

        TipoFinanciamiento _tipo;
        public TipoFinanciamiento tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        float _monto;
        public float Monto
        {
            get { return _monto; }
            set { _monto = value; }
        }

        Financiamiento() { }

        public Financiamiento(Mes inicio, int plazo, float tasa, TipoFinanciamiento tipo, float monto)
        {
            this._inicio = inicio;
            this._monto = monto;
            this._plazo = plazo;
            this._tasa = tasa;
            this._tipo = tipo;
        }
    }
}
