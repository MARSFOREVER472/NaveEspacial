using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NaveEspacial.Project
{
    internal class Nave
    {
        public float Vida { get; set; }
        public Point Posicion { get; set; }
        public ConsoleColor Color { get; set; }
        public Ventana VentanaC { get; set; }
        public List<Point> PosicionesNave { get; set; }

        public Nave(Point posicion, ConsoleColor color, Ventana ventana)
        {
            Posicion = posicion;
            Color = color;
            VentanaC = ventana;
            Vida = 100;
            PosicionesNave = new List<Point>();
        }

        public void Dibujar()
        {
            Console.ForegroundColor = Color;
            int x = Posicion.X;
            int y = Posicion.Y;

            Console.SetCursorPosition(x + 3, y);
            Console.Write("A");
            Console.SetCursorPosition(x + 1, y + 1);
            Console.Write("<{x}>");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("± W W ±");

            PosicionesNave.Clear(); // Este método elimina todos los elementos de la lista.

            // Procederemos al movimiento de la nave.

            // Para la primera fila
            PosicionesNave.Add(new Point(x + 3, y)); // Para A

            // Para la segunda fila
            PosicionesNave.Add(new Point(x + 1, y + 1)); // Para <
            PosicionesNave.Add(new Point(x + 2, y + 1)); // Para {
            PosicionesNave.Add(new Point(x + 3, y + 1)); // Para x
            PosicionesNave.Add(new Point(x + 4, y + 1)); // Para }
            PosicionesNave.Add(new Point(x + 5, y + 1)); // Para >

            // Para la tercera fila
            PosicionesNave.Add(new Point(x, y + 2)); // Para ±
            PosicionesNave.Add(new Point(x + 2, y + 2)); // Para W
            PosicionesNave.Add(new Point(x + 4, y + 2)); // Para W
            PosicionesNave.Add(new Point(x + 6, y + 2)); // Para ±
        }

        public void Borrar()
        {
            foreach (Point item in PosicionesNave)
            {
                Console.SetCursorPosition(item.X, item.Y);
                Console.Write(" ");
            }
        }

        // Leemos desde teclado los movimientos de la nave.
        public void Teclado(ref Point distancia, int velocidad)
        {
            ConsoleKeyInfo tecla = Console.ReadKey(true);
            if (tecla.Key == ConsoleKey.W) // Arriba
                distancia = new Point(0, -1);
            if (tecla.Key == ConsoleKey.S) // Abajo
                distancia = new Point(0, 1);
            if (tecla.Key == ConsoleKey.D) // Derecha
                distancia = new Point(1, 0);
            if (tecla.Key == ConsoleKey.A) // Izquierda
                distancia = new Point(-1, 0);

            distancia.X *= velocidad;
            distancia.Y *= velocidad;
        }

        public void Colisiones(Point distancia) // Aquí se realizan las colisiones para que no sobrepase por el marco dibujado.
        {
            Point posicionAux = new Point(Posicion.X + distancia.X, Posicion.Y + distancia.Y);
            if (posicionAux.X <= VentanaC.LimiteSuperior.X) // Para el lado izquierdo del marco.
                posicionAux.X = VentanaC.LimiteSuperior.X + 1;
            if (posicionAux.X + 6 >= VentanaC.LimiteInferior.X) // Para el lado derecho del marco.
                posicionAux.X = VentanaC.LimiteInferior.X - 7;
            if (posicionAux.Y <= VentanaC.LimiteSuperior.Y) // Para la parte superior del marco.
                posicionAux.Y = VentanaC.LimiteSuperior.Y + 1;
            if (posicionAux.Y + 2 >= VentanaC.LimiteInferior.Y) // Para la parte inferior del marco.
                posicionAux.Y = VentanaC.LimiteInferior.Y - 3;

            Posicion = posicionAux;
        }

        public void Mover(int velocidad)
        {
            if (Console.KeyAvailable)
            {
                Borrar();
                Point distancia = new Point();
                Teclado(ref distancia, velocidad);
                Colisiones(distancia);
                Dibujar();
            }
        }
    }
}
