using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Barco
    {
        Dictionary<Coordenada, String> CoordenadasBarco { get; set; }
        public string Nombre { get; set; }
        public int NumDanyos { get; set; }

        public event Action<TocadoArgs> eventoTocado;
        public event Action<HundidoArgs> eventoHundido;

        public Barco(string nombre, int longitud, char orientacion, Coordenada coordenadaInicio)
        {

            if (orientacion != 'h' && orientacion != 'v')
            {
                throw new ArgumentException("La orientación no correcta");
            }

            Nombre = nombre;
            NumDanyos = 0;
            CoordenadasBarco = new Dictionary<Coordenada, string>();

            for (int i = 0; i < longitud; i++)
            {
                int x = coordenadaInicio.Fila;
                int y = coordenadaInicio.Columna;

                if (orientacion == 'h')
                {
                    y += i;
                }
                else
                {
                    x += i;
                }

                Coordenada nuevaCoordenada = new Coordenada(x, y);
                CoordenadasBarco[nuevaCoordenada] = nombre;
            }
        }

        public void Disparo(Coordenada c)
        {
            if (CoordenadasBarco.ContainsKey(c))
            {
                CoordenadasBarco[c] += "_T";
                NumDanyos++;

                eventoTocado.Invoke(this, new TocadoArgs(c));

                if (NumDanyos == CoordenadasBarco.Count)
                {
                    eventoHundido.Invoke(this, new HundidoArgs(Nombre));
                }
            }
        }
        public bool Hundido()
        {
            List<string> etiquetas = new List<string>(CoordenadasBarco.Values);
            for (int i = 0; i < etiquetas.Count; i++)
            {
                if (etiquetas[i] == Nombre)
                {
                    return false;
                }
            }
            return true;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"[{Nombre}] - DAÑOS [{NumDanyos}] - HUNDIDO: [{Hundido()}] - COORDENADAS");

            foreach (var i in CoordenadasBarco)
            {
                sb.AppendLine($" [{i.ToString()} :{Nombre}]");
            }

            return sb.ToString();
        }
    }
}
