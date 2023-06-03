using NaveEspacial.Project;
using NaveEspacial.Project.CPU;
using System.Drawing;

Ventana ventana;
Nave nave;
Enemigo enemigo1;
bool jugar = true;
void Iniciar()
{
    ventana = new Ventana(170, 65, ConsoleColor.Black, new Point(1, 1), new Point(100, 40));
    ventana.DibujarMarco();
    nave = new Nave(new Point(55, 25), ConsoleColor.Yellow, ventana);
    nave.Dibujar();
    enemigo1 = new Enemigo(new Point(50, 10), ConsoleColor.Green, ventana, TipoEnemigo.Normal);
    enemigo1.Dibujar();
}
void Game()
{
    while (jugar)
    {
        nave.Mover(2);
        nave.Disparar();
        
        if (nave.Vida <= 0)
        {
            jugar = false;
            nave.Muerte();
        }
    }
}

Iniciar();
Game();
Console.ReadKey();