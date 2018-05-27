using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Produkterkenner
{
    class Tasche : Kategorie
    {
        private double Größe { get; set; }
        private string Farbe { get; set; }
        public Tasche(int seriennummer, string kategorie, string marke, string modell, double größe, string farbe, double preis) : base(seriennummer, kategorie, marke, modell, preis)
        {
            Größe = größe;
            Farbe = farbe;
        }
        public override string ToString()
        {
            return $"Seriennummer: {String.Format("{0:0000}", Seriennummer)}            Kategorie: {Kateg}                Marke: {Marke}            Modell: {Modell}            Größe: {Größe + " Zoll"}            Farbe: {Farbe}            Preis: {String.Format("{0:0.00}", Preis)}";
        }

    }
}
