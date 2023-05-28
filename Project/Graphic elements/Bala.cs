using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NaveEspacial.Project.Graphic_elements
{
    public enum TipoBala
    {
        Normal, Especial
    }
    internal class Bala
    {
        public Point Posicion { get; set; }
        public ConsoleColor Color { get; set; }
        public TipoBala TipoBala1 { get; set; }
        public List<Point> PosicionesBala { get; set; }
        public Bala(Point posicion, ConsoleColor color, TipoBala tipoBala)
        {
            Posicion = posicion;
            Color = color;
            TipoBala1 = tipoBala;
            PosicionesBala = new List<Point>();
        }

        public void Dibujar() // Dibuja las balas de la nave
        {
            Console.ForegroundColor = Color;
            int x = Posicion.X;
            int y = Posicion.Y;

            PosicionesBala.Clear();

            switch (TipoBala1)
            {
                case TipoBala.Normal: // Tipo de bala normal
                    Console.SetCursorPosition(x, y);
                    Console.Write("*");
                    PosicionesBala.Add(new Point(x, y));
                    break;

                case TipoBala.Especial: // Tipo de bala especial
                    Console.SetCursorPosition(x + 1, y);
                    Console.Write("_");
                    Console.SetCursorPosition(x, y + 1);
                    Console.Write("( )");
                    Console.SetCursorPosition(x + 1, y + 2);
                    Console.Write("W");
                    PosicionesBala.Add(new Point(x + 1, y));
                    PosicionesBala.Add(new Point(x, y + 1));
                    PosicionesBala.Add(new Point(x + 2, y + 1));
                    PosicionesBala.Add(new Point(x + 1, y + 2));
                    break;

            }
        }

        public void Borrar()
        {
            foreach (Point item in PosicionesBala)
            {
                Console.SetCursorPosition(item.X, item.Y);
                Console.Write(" ");
            }
        }

        public bool Mover(int velocidad, int limite)
        {
            Borrar();

            switch (TipoBala1)

            {
                case TipoBala.Normal:
                    Posicion = new Point(Posicion.X, Posicion.Y - velocidad);
                    if (Posicion.Y <= limite)
                        return true;
                    break;

                case TipoBala.Especial:
                    Posicion = new Point(Posicion.X, Posicion.Y - velocidad);
                    if (Posicion.Y <= limite)
                        return true;
                    break;
            }

            Dibujar();
            return false;
        }
    }
}
