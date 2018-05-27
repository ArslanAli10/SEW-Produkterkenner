using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Produkterkenner
{
    abstract class Kategorie
    {
        public int Seriennummer { get; set; }
        public string Kateg { get; set; }
        public string Marke { get; set; }
        public string Modell { get; set; }
        public double Preis { get; set; }
        public Kategorie(int seriennummer, string kategorie, string marke, string modell, double preis)
        {
            Seriennummer = seriennummer;
            Kateg = kategorie;
            Marke = marke;
            Modell = modell;
            Preis = preis;
        }
        public override string ToString()
        {
            return $"Seriennummer: {Seriennummer}            Kategorie: {Kateg}            Marke: {Marke}            Preis: {String.Format("{0:0.00}", Preis)}";
        }

    }
}
