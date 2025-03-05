using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hada.Eventos;

namespace Hada
{
    class Tablero
    {
        public event EventHandler<EventArgs> eventoFinPartida;
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
        private List<Coordenada> coordenadasDisparadas {
            get { return coordenadasDisparadas; }
            set {
                int i = value.Count - 1;
                if (value[i].Fila < TamTablero && value[i].Fila >= 0 && value[i].Columna < TamTablero && value[i].Columna >= 0)
                {
                    coordenadasDisparadas = value;
                }
                else {
                    throw new ArgumentOutOfRangeException("coordanada fuera de rango");
                }
            }
        }
        private List<Coordenada> coordenadasTocadas {
            get { return coordenadasTocadas; }
            set { foreach (var c in coordenadasTocadas) {
                    if (c == value[value.Count - 1]) {
                        throw new ArgumentException("La coordenada ya esta tocada");
                    }
                }
                coordenadasTocadas = value;
            }
        }
        private List<Barco> barcos;
        private List<Barco> barcosEliminados {
            get { return barcosEliminados; }
            set {
                foreach (var barco in barcosEliminados) {
                    if (barco == value[value.Count - 1]) {
                        throw new ArgumentException("El barco ya esta eliminado");
                    }
                }
                barcosEliminados = value;
            }
        }
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
                foreach (var coordenada in barco.CoordenadasBarco.Keys)
                {
                    if (casillasTablero.ContainsKey(coordenada))
                    {
                        casillasTablero[coordenada] = "BARCO"; // Sobrescribimos el agua con un barco
                    }
                }
            }
        }

        public void Dispara(Coordenada c) {

            if (c.Fila >= TamTablero || c.Fila < 0 || c.Columna >= TamTablero || c.Columna < 0) {
                Console.WriteLine($"La coordenada {c.ToString()} esta fuera de las dimensiones del tablero.");
            }

            foreach (var barco in barcos) {
                barco.Disparo(c);
            }
        }

        public string DibujarTablero() {
            string tab = "";
            for (int i = 0; i < TamTablero; i++) {
                for (int j = 0; j < TamTablero; j++) {
                    tab += $"[{casillasTablero[new Coordenada(i, j)]}]";
                    if (j == TamTablero - 1) {
                        tab += Environment.NewLine;
                    }
                }
            }
            return tab;
        }

        public override string ToString() {
            string t = "";

            foreach (var barco in barcos) {
                t += barco.ToString();
            }
            t += Environment.NewLine;
            t += Environment.NewLine;
            t += "Coordenadas disparadas: ";
            foreach (var coord in coordenadasDisparadas) {
                t += coord.ToString();
            }
            t += Environment.NewLine;
            t += "Coordenadas tocadas: ";
            foreach (var coord in coordenadasTocadas) {
                t += coord.ToString();
            }
            t += Environment.NewLine;
            t += "CASILLAS TABLERO" + Environment.NewLine;
            t += "-------" + Environment.NewLine; 
            t += DibujarTablero() + Environment.NewLine;
            return t;

        }

        private void cuandoEventoTocado(object sender, TocadoArgs e) {
            coordenadasTocadas.Add(e.CoordenadaImpacto);
            Console.WriteLine($"TABLERO: BARCO {e.Nombre} tocado en Coordenada: {e.CoordenadaImpacto}");
        }

        private void cuandoEventoHundido(object sender, HundidoArgs e) {
            Console.WriteLine($"TABLERO: Barco {e} hundido!!");
            if (barcos.Count == barcosEliminados.Count) {
                eventoFinPartida?.Invoke(this, new EventArgs());
            }
        }
    }
}

