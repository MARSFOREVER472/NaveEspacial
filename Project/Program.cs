﻿using NaveEspacial.Project;
using System.Drawing;

Ventana ventana;
Nave nave;
bool jugar = true;
void Iniciar()
{
    ventana = new Ventana(170, 65, ConsoleColor.Black, new Point(1, 1), new Point(115, 40));
    ventana.DibujarMarco();
    nave = new Nave(new Point(55, 25), ConsoleColor.Yellow, ventana);
    nave.Dibujar();
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