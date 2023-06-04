using NaveEspacial.Project;
using NaveEspacial.Project.CPU;
using System.Drawing;

Ventana ventana;
Nave nave;
Enemigo enemigo1; //Para el enemigo normal 1
Enemigo enemigo2; // Para el enemigo normal 2
Enemigo enemigoBoss; // Para el jefe final del juego.
bool jugar = true;
void Iniciar()
{
    ventana = new Ventana(170, 65, ConsoleColor.Black, new Point(1, 1), new Point(115, 40));
    ventana.DibujarMarco();
    nave = new Nave(new Point(55, 25), ConsoleColor.Yellow, ventana);
    nave.Dibujar();
    enemigo1 = new Enemigo(new Point(30, 10), ConsoleColor.Green, ventana, TipoEnemigo.Normal); // Dibujaremos el enemigo normal 1.
    enemigo1.Dibujar();
    enemigo2 = new Enemigo(new Point(70, 10), ConsoleColor.DarkYellow, ventana, TipoEnemigo.Normal); // Dibujaremos el enemigo normal 2.
    enemigo2.Dibujar();
    enemigoBoss = new Enemigo(new Point(50, 10), ConsoleColor.DarkRed, ventana, TipoEnemigo.Boss); // Dibujaremos al jefe final.
    enemigoBoss.Dibujar();
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