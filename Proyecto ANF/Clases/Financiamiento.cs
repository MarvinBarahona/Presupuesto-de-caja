using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_ANF.Clases
{
    public class Financiamiento
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

        decimal _tasa;
        public decimal tasa
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

        decimal _monto;
        public decimal monto
        {
            get { return _monto; }
            set { _monto = value; }
        }

        Financiamiento() { }

        public Financiamiento(Mes inicio, int plazo, decimal tasa, TipoFinanciamiento tipo, decimal monto)
        {
            this._inicio = inicio;
            this._monto = monto;
            this._plazo = plazo;
            this._tasa = tasa;
            this._tipo = tipo;
        }
    }
}
