using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib
{
    public class Status
    {
        public int Id { get; set; }
        public DateTime Dato { get; set; }
        public bool AllowChange { get; set; }

        public Status(int id, DateTime dato, bool allowChange)
        {
            Id = id;
            Dato = dato;
            AllowChange = allowChange;
        }


        public Status()
        {
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Dato)}: {Dato}, {nameof(AllowChange)}: {AllowChange}";
        }
    }
}
