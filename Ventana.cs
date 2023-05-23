using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaveEspacial
{
    internal class Ventana
    {
        public int Ancho { get; set; }
        public int Altura { get; set; }
        public Ventana(int ancho, int altura)
        {
            Ancho = ancho;
            Altura = altura;
            Init();

        }

        private void Init()
        {
            Console.SetWindowSize(Ancho,Altura);
            Console.Title = "Nave";
        }
    }
}
