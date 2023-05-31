using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using NaveEspacial.Project.Graphic_elements;

namespace NaveEspacial.Project
{
    internal class Nave
    {
        public float Vida { get; set; }
        public Point Posicion { get; set; }
        public ConsoleColor Color { get; set; }
        public Ventana VentanaC { get; set; }
        public List<Point> PosicionesNave { get; set; }
        public List<Bala> Balas { get; set; }
        public float SobreCarga { get; set; }
        public bool SobreCargaCond { get; set; }
        public float BalaEspecial { get; set; }

        public Nave(Point posicion, ConsoleColor color, Ventana ventana)
        {
            Posicion = posicion;
            Color = color;
            VentanaC = ventana;
            Vida = 100;
            PosicionesNave = new List<Point>();
            Balas = new List<Bala>();
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

            if (tecla.Key == ConsoleKey.RightArrow) // Si se presionó la tecla derecha
            {
                if (!SobreCargaCond)
                {
                    Bala bala = new Bala(new Point(Posicion.X + 6, Posicion.Y + 2),
                    ConsoleColor.Blue, TipoBala.Normal);
                    Balas.Add(bala);

                    SobreCarga += 1.2f;
                    if (SobreCarga >= 100)
                    {
                        SobreCargaCond = true;
                        SobreCarga = 100;
                    }

                }
                
                 
            }

            if (tecla.Key == ConsoleKey.LeftArrow) // Si se presionó la tecla izquierda
            {
                if (!SobreCargaCond)
                {
                    Bala bala = new Bala(new Point(Posicion.X, Posicion.Y + 2),
                    ConsoleColor.Blue, TipoBala.Normal);
                    Balas.Add(bala);

                    SobreCarga += 1.2f;
                    if (SobreCarga >= 100)
                    {
                        SobreCargaCond = true;
                        SobreCarga = 100;
                    }
                        

                }
                
            }

            if (tecla.Key == ConsoleKey.UpArrow)
            {
                if (BalaEspecial >= 100)
                {
                    Bala bala = new Bala(new Point(Posicion.X + 2, Posicion.Y - 2),
                    ConsoleColor.Blue, TipoBala.Especial);
                    Balas.Add(bala);
                    BalaEspecial = 0;
                }
            }
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

        public void Informacion()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(VentanaC.LimiteSuperior.X, VentanaC.LimiteSuperior.Y - 1);
            Console.Write(" Spaceship Health: " + (int)Vida + "% ");

            if (SobreCarga <= 0) // Al momento de disparar, ésta se disminuye cuando no se sobrecarga
                SobreCarga = 0;
            else
                SobreCarga += 0.0007f;

            if (SobreCarga <= 50)
                SobreCargaCond = false;

            if (SobreCargaCond)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(VentanaC.LimiteSuperior.X + 23, VentanaC.LimiteSuperior.Y - 1);
            Console.Write(" Overload: " + (int)SobreCarga + "% ");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(VentanaC.LimiteSuperior.X + 42, VentanaC.LimiteSuperior.Y - 1);
            Console.Write(" Special Bullet: " + (int)BalaEspecial + "% ");

            if (BalaEspecial <= 100)
                BalaEspecial = 100;
            else
                BalaEspecial += -0.0050f;
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
            Informacion();
        }

        public void Disparar()
        {
            for (int i = 0; i < Balas.Count; i++)
            {
                if (Balas[i].Mover(1, VentanaC.LimiteSuperior.Y))
                {
                    Balas.Remove(Balas[i]);
                }
            }
        }
    }
}
