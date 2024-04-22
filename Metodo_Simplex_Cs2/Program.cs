// See https://aka.ms/new-console-template for more information
namespace Metodo_Simplex_Cs2;

using System.Collections.Generic;

class Program {
  public static void Main() {
    /*
    float[,] contenido = new float[,] { { 1, -2, -2, 0, 0, 0, 0, 0 },
                                        { 0, 1, 0, 1, 0, 0, 0, 45 },
                                        { 0, 0, 1, 0, 1, 0, 0, 100 },
                                        { 0, 2, 1, 0, 0, 1, 0, 100 },
                                        { 0, 1, 3, 0, 0, 0, 1, 80 } };*/

    float[,] contenido =
        new float[,] { { 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0 },
                       { 0, 1, 0, 1, 0, -1, 1, 0, 0, 0, 0, 0, 40 },
                       { 0, 0, 1, 0, 1, 0, 0, -1, 1, 0, 0, 0, 70 },
                       { 0, 2, -1, 2, -1, 0, 0, 0, 0, 1, 0, 0, 0 },
                       { 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 180 },
                       { 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 45 } };

    /*List<string> ecuaciones =
        new List<string> { "Z = 0.12*x1 +0.15*x2", "60 * x1 + 60 * x2 >= 300",
                           "12 * x1 + 6 * x2 >= 36",
                           "10 * x1 + 30 * x2 >= 90" };*/
    Simplex Ejemplo = new Simplex(contenido, true);
    // Ejemplo.IdentificarMatriz();
    Ejemplo.obtenerResultado();
    Ejemplo.mostrarTabla();
  }
}
