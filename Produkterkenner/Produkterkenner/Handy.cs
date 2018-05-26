using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Produkterkenner
{
    class Handy : Kategorie
    {
        public double Zoll { get; set; }
        public int Speicherplatz { get; set; }

        public Handy(int seriennummer, string kategorie, string marke, string modell, double zoll, int speicherplatz, double preis) : base(seriennummer, kategorie, marke, modell, preis)
        {
            Zoll = zoll;
            Speicherplatz = speicherplatz;
        }


        public override string ToString()
        {
            return $"Seriennummer: {Seriennummer}            Kategorie: {Kateg}            Marke: {Marke}            Modell: {Modell}            Zoll: {Zoll}            Speicherplatz: {Speicherplatz}            Preis: {String.Format("{0:0.00}", Preis)}";
        }

    }
}
