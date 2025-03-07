using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Game
    {
        private bool finPartida;
        private Tablero tablero;

        public Game()
        {
            finPartida = false;
            gameLoop();

            tablero.eventoFinPartida += cuandoEventoFinPartida;
        }

        private void gameLoop()
        {
            List<Barco> barcos = new List<Barco>
            {
                new Barco("THOR",1,'h', new Coordenada(0,0)),
                new Barco("LOKI",2,'v', new Coordenada(1,2)),
                new Barco("MAYA",3,'h', new Coordenada(3,1))
            };

            tablero = new Tablero(5, barcos);

            tablero.eventoFinPartida += cuandoEventoFinPartida;

            while (!finPartida)
            {
                Console.WriteLine(tablero);
                Console.Write("Introduce la coordenada a la que disparar FILA,COLUMNA ('S' para Salir): ");
                string coord = Console.ReadLine();

                if (coord.ToLower() == "s")
                {
                    finPartida = true;
                    Console.WriteLine("Has salido del juego");
                    break;
                }

                if (ValidarCoord(coord, out int fila, out int columna))
                {
                    tablero.Disparar(new Coordenada(fila, columna));
                }
                else
                {
                    Console.WriteLine("Formato incorrecto");
                }
            }
        }

        private bool ValidarCoord(string coord, out int fila, out int columna)
        {
            fila = -1;
            columna = -1;

            string[] partes = coord.Split(',');
            if (partes.Length == 2 && int.TryParse(partes[0], out fila) && int.TryParse(partes[1],out columna)){
                return true;
            } else {
                return false;
            }
            
        }

        private void cuandoEventoFinPartida(object sender, EventArgs e) {
            Console.WriteLine("PARTIDA FINALIZADA!!");
            finPartida = true;
        }
    }
}
