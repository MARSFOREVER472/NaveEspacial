
using NaveEspacial;

Ventana ventana = new Ventana(45,20);

Console.WriteLine("Ancho Máximo:" +Console.LargestWindowWidth);
Console.WriteLine("Altura Máxima:" + Console.LargestWindowHeight);

Console.SetCursorPosition(5, 5);
Console.Write("Estamos trabajando para solucionarlo");

Console.SetCursorPosition(5, 8);
Console.Write("Vuelve a intentarlo más tarde");

Console.ReadKey();
