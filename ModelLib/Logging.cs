using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib
{
    public class Logging
    {
        public int Id { get; set; }
        public DateTime Dato { get; set; }
        public double Luftfugtighed { get; set; }
        public bool Aktiv { get; set; }


        public Logging(DateTime dato, double luftfugtighed, bool aktiv)
        {
            Dato = dato;
            Luftfugtighed = luftfugtighed;
            Aktiv = aktiv;
        }

        public Logging()
        {
        }


        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Dato)}: {Dato}, {nameof(Luftfugtighed)}: {Luftfugtighed}, {nameof(Aktiv)}: {Aktiv}";
        }
    }
}
