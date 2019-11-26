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


        public Logging(int id, DateTime dato, double luftfugtighed)
        {
            Id = id;
            Dato = dato;
            Luftfugtighed = luftfugtighed;
        }

        public Logging()
        {
        }


        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Dato)}: {Dato}, {nameof(Luftfugtighed)}: {Luftfugtighed}";
        }
    }
}
