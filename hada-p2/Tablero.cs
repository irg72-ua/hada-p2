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
        private int tamTablero;
        public int TamTablero {
            get { return tamTablero; }
            set {
                if (value >= 4 && value <= 9)
                {
                    tamTablero = value;
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
            foreach (var barco in barcos) {
                barco.eventoTocado += cuandoEventoTocado;
                barco.eventoHundido += cuandoEventoHundido;
            }

        }

        private void inicializaCasillasTablero() {
            for (int i = 0; i < TamTablero; i++) {
                for (int j = 0; j < TamTablero; j++) {
                    casillasTablero.Add(new Coordenada(i, j), "AGUA");
                }
            }
            foreach (var barco in barcos)
            {
                foreach (var coordenada in barco.CoordenadasBarco.Keys)
                {
                    if (casillasTablero.ContainsKey(coordenada))
                    {
                        casillasTablero[coordenada] = barco.Nombre; // Sobrescribimos el agua con un barco
                    }
                }
            }
        }

        public void Disparar(Coordenada c) {

            if (c.Fila >= TamTablero || c.Fila < 0 || c.Columna >= TamTablero || c.Columna < 0)
            {
                Console.WriteLine($"La coordenada {c.ToString()} esta fuera de las dimensiones del tablero.");
            }
            else {
                coordenadasDisparadas.Add(c);
            }

                foreach (var barco in barcos)
                {
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
            t += "Coordenadas disparadas: ";
            foreach (var coord in coordenadasDisparadas) {
                t += coord.ToString();
            }
            t += Environment.NewLine;
            t += "Coordenadas tocadas: ";
            foreach (var coord in coordenadasTocadas) {
                t += coord.ToString();
            }
            for(int i = 0; i < 4; i++)
            t += Environment.NewLine;

            t += "CASILLAS TABLERO" + Environment.NewLine;
            t += "-------" + Environment.NewLine; 
            t += DibujarTablero();
            return t;

        }

        private void cuandoEventoTocado(object sender, TocadoArgs e) {
            coordenadasTocadas.Add(e.CoordenadaImpacto);
            Console.WriteLine($"TABLERO: BARCO {e.Nombre} tocado en Coordenada: {e.CoordenadaImpacto}");
        }

        private void cuandoEventoHundido(object sender, HundidoArgs e) {
            Console.WriteLine($"TABLERO: Barco {e.Nombre} hundido!!");
            foreach (var barco in barcos) {
                if (barco.Nombre == e.Nombre) {
                    barcosEliminados.Add(barco);
                }
            }
            
            if (barcos.Count == barcosEliminados.Count) {
                eventoFinPartida?.Invoke(this, new EventArgs());
            }
        }
    }
}

