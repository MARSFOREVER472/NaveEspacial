using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaveEspacial.Project.CPU
{
    public enum TipoEnemigo
    {
        Normal, Boss
    }
    internal class Enemigo
    {
        enum Direccion // Agregamos el movimiento del enemigo en distintas direcciones
        {
            Derecha,Izquierda,Arriba,Abajo
        }
        
        public bool Vivo { get; set; } // Verifica si está vivo o no
        public float Vida { get; set; } // Añade una vida al enemigo
        public Point Posicion { get; set; }
        public Ventana VentanaC { get; set; }
        public ConsoleColor Color { get; set; }
        public TipoEnemigo EnemyType { get; set; }
        public List<Point> PosicionesEnemigo { get; set; }
        private Direccion _direccion;
        private DateTime _tiempoDireccion;
        private float _tiempoDireccionAleatoria;
        private DateTime _tiempoMovimiento;

        public Enemigo(Point posicion, ConsoleColor color, Ventana ventana, 
            TipoEnemigo enemytype)
        {
            Posicion = posicion;
            VentanaC = ventana;
            Color = color;
            EnemyType = enemytype;
            Vivo = true;
            Vida = 100;
            _direccion = Direccion.Derecha;
            _tiempoDireccion = DateTime.Now;
            _tiempoDireccionAleatoria = 1000;
            PosicionesEnemigo = new List<Point>();
            _tiempoMovimiento = DateTime.Now;
        }

        public void Dibujar()
        {
            switch (EnemyType)
            {
                case TipoEnemigo.Normal:
                    DibujoNormal();
                    break;

                case TipoEnemigo.Boss:
                    DibujoBoss();
                    break;
            }
        }

        public void DibujoNormal()
        {
            Console.ForegroundColor = Color;
            int x = Posicion.X;
            int y = Posicion.Y;

            Console.SetCursorPosition(x + 1, y);
            Console.Write("▄▄");
            Console.SetCursorPosition(x, y + 1);
            Console.Write("████");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("▀  ▀");

            PosicionesEnemigo.Clear();

            PosicionesEnemigo.Add(new Point(x + 1, y));
            PosicionesEnemigo.Add(new Point(x + 2, y));
            PosicionesEnemigo.Add(new Point(x, y + 1));
            PosicionesEnemigo.Add(new Point(x + 1, y + 1));
            PosicionesEnemigo.Add(new Point(x + 2, y + 1));
            PosicionesEnemigo.Add(new Point(x + 3, y + 1));
            PosicionesEnemigo.Add(new Point(x, y + 2));
            PosicionesEnemigo.Add(new Point(x + 3, y + 2));
        }

        public void DibujoBoss()
        {
            Console.ForegroundColor = Color;
            int x = Posicion.X;
            int y = Posicion.Y;

            Console.SetCursorPosition(x + 1, y);
            Console.Write("█▄▄▄▄█");
            Console.SetCursorPosition(x, y + 1);
            Console.Write("██ ██ ██");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("█▀▀▀▀▀▀█");

            PosicionesEnemigo.Clear();

            PosicionesEnemigo.Add(new Point(x + 1, y));
            PosicionesEnemigo.Add(new Point(x + 2, y));
            PosicionesEnemigo.Add(new Point(x + 3, y));
            PosicionesEnemigo.Add(new Point(x + 4, y));
            PosicionesEnemigo.Add(new Point(x + 5, y));
            PosicionesEnemigo.Add(new Point(x + 6, y));

            PosicionesEnemigo.Add(new Point(x, y + 1));
            PosicionesEnemigo.Add(new Point(x + 1, y + 1));
            PosicionesEnemigo.Add(new Point(x + 3, y + 1));
            PosicionesEnemigo.Add(new Point(x + 4, y + 1));
            PosicionesEnemigo.Add(new Point(x + 6, y + 1));
            PosicionesEnemigo.Add(new Point(x + 7, y + 1));

            PosicionesEnemigo.Add(new Point(x, y + 2));
            PosicionesEnemigo.Add(new Point(x + 1, y + 2));
            PosicionesEnemigo.Add(new Point(x + 2, y + 2));
            PosicionesEnemigo.Add(new Point(x + 3, y + 2));
            PosicionesEnemigo.Add(new Point(x + 4, y + 2));
            PosicionesEnemigo.Add(new Point(x + 5, y + 2));
            PosicionesEnemigo.Add(new Point(x + 6, y + 2));
            PosicionesEnemigo.Add(new Point(x + 7, y + 2));
        }

        public void Borrar()
        {
            foreach (Point item in PosicionesEnemigo)
            {
                Console.SetCursorPosition(item.X, item.Y);
                Console.Write(" ");
            }
        }    

        public void Mover()
        {
            int tiempo = 30;

            if (EnemyType == TipoEnemigo.Boss)
                tiempo = 20;

            if (DateTime.Now > _tiempoMovimiento.AddMilliseconds(tiempo))
            {
                Borrar();
                DireccionAleatoria();
                Point posicionAux = Posicion;
                Movimiento(ref posicionAux);
                Colisiones(posicionAux);
                Dibujar();
                _tiempoMovimiento = DateTime.Now;
            }
            
        }

        public void Colisiones(Point posicionAux)
        {
            int ancho = 0;

            if (EnemyType == TipoEnemigo.Boss)
                ancho = 7;

            if (posicionAux.X <= VentanaC.LimiteSuperior.X)
            {
                _direccion = Direccion.Derecha;
                posicionAux.X = VentanaC.LimiteSuperior.X + 1;
            }
                
            if (posicionAux.X + ancho >= VentanaC.LimiteInferior.X)
            {
                _direccion = Direccion.Izquierda;
                posicionAux.X = VentanaC.LimiteInferior.X - 1 + ancho;
            }
                
            if (posicionAux.Y <= VentanaC.LimiteSuperior.Y)
            {
                _direccion = Direccion.Abajo;
                posicionAux.Y = VentanaC.LimiteSuperior.Y + 1;
            }
                
            if (posicionAux.Y >= VentanaC.LimiteSuperior.Y + 5)
            {
                _direccion = Direccion.Arriba;
                posicionAux.Y = VentanaC.LimiteSuperior.Y + 5 - 2;
            }
                

            Posicion = posicionAux;
        }

        public void Movimiento(ref Point posicionAux)
        {
            switch (_direccion)
            {
                case Direccion.Derecha:
                    posicionAux.X += 1;
                    break;
                case Direccion.Izquierda:
                    posicionAux.X -= 1;
                    break;
                case Direccion.Arriba:
                    posicionAux.Y -= 1;
                    break;
                case Direccion.Abajo:
                    posicionAux.Y += 1;
                    break;

            }
        }

        public void DireccionAleatoria()
        {
            if (DateTime.Now >_tiempoDireccion.AddMilliseconds(_tiempoDireccionAleatoria))
            {
                Random random = new Random();
                int numAleatorio = random.Next(1, 5);

                switch (numAleatorio)
                {
                    case 1:
                        _direccion = Direccion.Derecha;
                        break;
                    case 2:
                        _direccion = Direccion.Izquierda;
                        break;
                    case 3:
                        _direccion = Direccion.Arriba;
                        break;
                    case 4:
                        _direccion = Direccion.Abajo;
                        break;
                }

                _tiempoDireccion = DateTime.Now;
                _tiempoDireccionAleatoria = random.Next(1000, 2000);
            }
        }
    }
}
