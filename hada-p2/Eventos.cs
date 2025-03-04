using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Eventos
    {
        public class TocadoArgs : EventArgs { 
            public string Nombre { get; }
            public Coordenada CoordenadaImpacto { get; }
            public TocadoArgs(string nombre, Coordenada coordenada) {
                Nombre = Nombre;
                CoordenadaImpacto = coordenada;
            }
        }

        public class HundidoArgs : EventArgs
        {
            public string Nombre { get; }
            public HundidoArgs(string nombre)
            {
                Nombre = nombre;
            }
        }
    }
}
