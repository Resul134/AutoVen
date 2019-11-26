using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib
{
    public class Status
    {
        public int Id { get; set; }
        public DateTime Dato { get; set; }
        public bool Active { get; set; }

        public Status(int id, DateTime dato, bool active)
        {
            Id = id;
            Dato = dato;
            Active = active;
        }


        public Status()
        {
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Dato)}: {Dato}, {nameof(Active)}: {Active}";
        }
    }
}
