using NaveEspacial.Project;
using System.Drawing;

Ventana ventana = new Ventana(170, 65, ConsoleColor.Black, new Point(1, 1), new Point(115, 45));
ventana.DibujarMarco();
Nave nave = new Nave(new Point(55, 25), ConsoleColor.Yellow, ventana);
nave.Dibujar();

Console.ReadKey();