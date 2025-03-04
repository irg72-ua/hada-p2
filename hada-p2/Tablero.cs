using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Tablero
    {
        public int TamTablero {
            get { return TamTablero; }
            set {
                if (value >= 4 && value <= 9)
                {
                    TamTablero = value;
                }
                else {
                    throw new ArgumentOutOfRangeException($"{nameof(value)} tiene que estar entre 4 y 9"); 
                }
            }
        }
        private List<Coordenada> coordenadasDisparadas;
        private List<Coordenada> coordenadasTocadas;
        private List<Barco> barcos;
        private List<Barco> barcosEliminados;
        private Dictionary<Coordenada, string> casillasTablero;


        public Tablero(int tamTablero, List<Barco> barcos) {
            this.TamTablero = tamTablero;
            this.barcos = barcos;

            barcosEliminados = new List<Barco>();
            coordenadasDisparadas = new List<Coordenada>();
            coordenadasTocadas = new List<Coordenada>();
            casillasTablero = new Dictionary<Coordenada, string>();

            inicializaCasillasTablero();
            
        }

        private void inicializaCasillasTablero() {
            for (int i = 0; i < TamTablero; i++) {
                for (int j = 0; i < TamTablero; i++) {
                    casillasTablero.Add(new Coordenada(i, j), "AGUA");
                }
            }
            foreach (var barco in barcos)
            {
                foreach (var coordenada in barco.) 
                {
                    if (casillasTablero.ContainsKey(coordenada))
                    {
                        casillasTablero[coordenada] = "BARCO"; // Sobrescribimos el agua con un barco
                    }
                }
            }
        }
    }

    }
}
