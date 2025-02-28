using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hada
{
    internal class Coordenada
    {
        public int Fila{
            get { return Fila; }
            set
            {
                if (value < 0 || value > 9)
                {
                    throw new ArgumentException("Valor fuera de rango");
                } else
                {
                    Fila = value;
                }
            }
        }
        public int Columna
        {
            get { return Columna; }
            set
            {
                if (value < 0 || value > 9)
                {
                    throw new ArgumentException("Valor fuera de rango");
                }
                else
                {
                    Columna = value;
                }
            }
        }

        public Coordenada()
        {
            Fila = 0;
            Columna = 0;
        }

        public Coordenada(int f, int c)
        {
            Fila = f;
            Columna = c;
        }

        public Coordenada (string f, string c)
        {
            Fila = int.Parse(f);
            Columna = int.Parse(c);
        }

        public Coordenada(Coordenada c)
        {
            Fila = c.Fila;
            Columna = c.Columna;
        }

        public string toString()
        {
            return "(" + Fila + "," + Columna + ")";
        }

        public override int GetHashCode()
        {
            return this.Fila.GetHashCode() ^ this.Columna.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Coordenada otra = (Coordenada)obj;
            return (Fila == otra.Fila && Columna == otra.Columna);
        }
        public bool Equals(Coordenada coordenada)
        {
            if (coordenada == null)
                return false;

            return (Fila == coordenada.Fila && Columna == coordenada.Columna);
        }
    }
}
