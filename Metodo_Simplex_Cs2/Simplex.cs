namespace Metodo_Simplex_Cs2;

using System.Text.RegularExpressions;

public class Simplex {
  // string?[] arregloEntrada;
  private float[,] tablaSimplex;
  private bool dosFases;
  // private List<string> entradaLista;

  // public Simplex(string[] valoresEntrada){

  // }

  public Simplex(float[,] arregloNumero, bool dosFases) {
    this.tablaSimplex = arregloNumero;
    this.dosFases = dosFases;
  }
  /*
    public Simplex(List<string> formaExtendida) {
      entradaLista = formaExtendida;
      tablaSimplex =
          new float[formaExtendida.Count, formaExtendida.Count];
      dosFases = true;
    }*/

  public void obtenerResultado() {
    if (dosFases) {
      manejoDosFases();
    } else {
      manejoSimple();
    }
  }

  public void IdentificarMatriz() {
    /*for (int i = 0; i < 2; i++) {
      MatchCollection ecuacion1 = Regex.Matches(
          entradaLista[i], @"\b(?<coeficientes>\d+(\.\d+)?)\s*\*\s*x\d+\b");

      for (int x = 0; x < ecuacion1.Count; x++)
      {
        Console.WriteLine(ecuacion1[x].Groups["coeficientes"].Value);
      }
    }*/
    /*
    for (int i = 0; i < entradaLista.Count; i++) {

      Match match = Regex.Match(
          entradaLista[i],
          @"([-+]?[0-9]*\.?[0-9]+)([a-zA-Z]+)\s*(=)\s*([-+]?[0-9]*\.?[0-9]+)(" +
          @"[a-zA-Z]+)\s*([-+])\s*([-+]?[0-9]*\.?[0-9]+)([a-zA-Z]+)");
      Match match2 = Regex.Match(
          entradaLista[i],
          @"([-+]?[0-9]*\.?[0-9]+)\s*(<=|>=|<|>)\s*([-+]?[0-9]*\.?[0-9]+)([a-" +
          @"zA-Z]+)\s*(<=|>=|<|>)\s*([-+]?[0-9]*\.?[0-9]+)");
      Match match3 = Regex.Match(
          entradaLista[i],
          @"([-+]?[0-9]*\.?[0-9]+)([a-zA-Z]+)\s*([-+])\s*([-+]?[0-9]*\.?[0-9]" +
          @"+)([a-zA-Z]+)\s*(<=|>=|<|>)\s*([-+]?[0-9]*\.?[0-9]+)");
      if (match.Success) {
        tablaSimplex[i, 0] = int.Parse(match.Groups[1].Value);
        tablaSimplex[i, 1] = -int.Parse(match.Groups[4].Value);
        tablaSimplex[i, 2] = -int.Parse(match.Groups[7].Value);
      }
      if (match2.Success) {
        if (i == 1) {
          tablaSimplex[i, 0] = int.Parse(match2.Groups[1].Value);
          tablaSimplex[i, 1] = int.Parse(match2.Groups[3].Value);
          tablaSimplex[i, entradaLista.Count + 2] =
              int.Parse(match2.Groups[6].Value);
          tablaSimplex[i, i + 2] = 1;
        } else {
          tablaSimplex[i, 1] = int.Parse(match2.Groups[1].Value);
          tablaSimplex[i, 2] = int.Parse(match2.Groups[3].Value);
          tablaSimplex[i, entradaLista.Count + 2] =
              int.Parse(match2.Groups[6].Value);
          tablaSimplex[i, i + 2] = 1;
        }
      }
      if (match3.Success) {
        tablaSimplex[i, 1] = int.Parse(match3.Groups[1].Value);
        tablaSimplex[i, 2] = int.Parse(match3.Groups[4].Value);
        tablaSimplex[i, entradaLista.Count + 2] =
            int.Parse(match3.Groups[7].Value);
        tablaSimplex[i, i + 2] = 1;
      }
    }*/
  }

  public void manejoSimple() {
    float[] filaPivot, columnaPivot;
    float numColumna;
    bool unaVez = false;
    int fila, columna;
    bool continua = exiteNegativo();
    while (continua) {
      columnaPivot = obtenerColumnaMenor();
      filaPivot = obtenerFila((int)columnaPivot[1]);

      fila = (int)filaPivot[1];
      unaVez = false;

      while (fila < tablaSimplex.GetLength(0)) {

        numColumna = tablaSimplex[fila, (int)columnaPivot[1]];
        columna = 1;

        while (columna < tablaSimplex.GetLength(1)) {

          if (fila == (int)filaPivot[1] && unaVez == false) {
            tablaSimplex[fila, columna] =
                (float)(tablaSimplex[fila, columna] / numColumna);
          }

          else if (fila != (int)filaPivot[1]) {
            tablaSimplex[fila, columna] =
                tablaSimplex[fila, columna] -
                (tablaSimplex[(int)filaPivot[1], columna] * numColumna);
          }

          columna += 1;
        }

        if (fila == (int)filaPivot[1] && columna == tablaSimplex.GetLength(1) &&
            unaVez == false) {
          fila = 0;
          unaVez = true;
        } else {
          fila += 1;
        }
      }

      continua = exiteNegativo();
    }
  }

  private void manejoDosFases() {
    int numArtificial = 2;
    int numHolgura = 3;
    int numVariables = 4;

    // Primera fase
    /*for (int i = tablaSimplex.GetLength(1) - 1; i >= 1 + numVariables ; i--)
    {


    } */
    float[] columnaPivot, filaPivot;
    int fila, columna;

    float numColumna;
    bool unaVez = false;
    parteUnoFaseUno();
    List<int> entradaColumna = new List<int>();
    List<int> salidaFila = new List<int>();
    while (exiteNegativo()) {
      columnaPivot = obtenerColumnaMenor();
      entradaColumna.Add((int)columnaPivot[1]);
      filaPivot = obtenerFila((int)columnaPivot[1]);
      salidaFila.Add((int)filaPivot[1]);


      fila = (int)filaPivot[1];
      unaVez = false;

      while (fila < tablaSimplex.GetLength(0)) {

        numColumna = tablaSimplex[fila, (int)columnaPivot[1]];
        columna = 1;

        while (columna < tablaSimplex.GetLength(1)) {

          if (fila == (int)filaPivot[1] && unaVez == false) {
            tablaSimplex[fila, columna] =
                (float)(tablaSimplex[fila, columna] / numColumna);
          }

          else if (fila != (int)filaPivot[1]) {
            tablaSimplex[fila, columna] =
                tablaSimplex[fila, columna] -
                (tablaSimplex[(int)filaPivot[1], columna] * numColumna);
          }

          columna += 1;
        }

        if (fila == (int)filaPivot[1] && columna == tablaSimplex.GetLength(1) &&
            unaVez == false) {
          fila = 0;
          unaVez = true;
        } else {
          fila += 1;
        }
      }
      mostrarTabla();
    }
  }

  private void parteUnoFaseUno() {
    List<int> arregloColumnas = new List<int>();
    List<int> arregloFilas = new List<int>();

    for (int i = 1; i < tablaSimplex.GetLength(1); i++) {
      if (tablaSimplex[0, i] == 1) {
        arregloColumnas.Add(i);
      }
    }
    for (int columna = 0; columna < arregloColumnas.Count; columna++) {
      for (int fila = 1; fila < tablaSimplex.GetLength(0); fila++) {

        if (tablaSimplex[fila, arregloColumnas[columna]] == 1) {
          arregloFilas.Add(fila);
        }
      }
    }
    Console.WriteLine(arregloColumnas[1]);
    Console.WriteLine(arregloFilas[1]);
    for (int fila = 0; fila < arregloFilas.Count; fila++) {
      for (int columna = 1; columna < tablaSimplex.GetLength(1); columna++) {
        tablaSimplex[0, columna] = tablaSimplex[0, columna] -
                                   tablaSimplex[arregloFilas[fila], columna];
      }
    }
  }

  private bool exitePositivo() {
    for (int i = 1; i < tablaSimplex.GetLength(1) - 1; i++) {
      if (tablaSimplex[0, i] < 0) {
        return true;
      }
    }
    return false;
  }

  private float[] obtenerColumnaMayor() {
    float[] arregloColumna = new float[] { tablaSimplex[0, 1], 1 };
    for (int i = 1; i < tablaSimplex.GetLength(1); i++) {
      if (tablaSimplex[0, i] < arregloColumna[0]) {
        arregloColumna[0] = tablaSimplex[0, i];
        arregloColumna[1] = i;
      }
    }
    return arregloColumna;
  }

  private float[] obtenerColumnaMenor() {
    float[] arregloColumna = new float[] { tablaSimplex[0, 1], 1 };
    for (int i = 1; i < tablaSimplex.GetLength(1)-1; i++) {
      if (tablaSimplex[0, i] < arregloColumna[0]) {
        arregloColumna[0] = tablaSimplex[0, i];
        arregloColumna[1] = i;
      }
    }
    return arregloColumna;
  }

  private float[] obtenerFila(int columnaPivote) {
    float actual;
    int numColumna = tablaSimplex.GetLength(1);
    float[] arregloFila =
        new float[] { obtenerDivision(tablaSimplex[1, numColumna - 1],
                                      tablaSimplex[1, columnaPivote]),
                      1 };

    for (int i = 2; i < tablaSimplex.GetLength(0); i++) {
      actual = obtenerDivision(tablaSimplex[i, numColumna - 1],
                               tablaSimplex[i, columnaPivote]);
      if (actual < arregloFila[0] && actual > 0) {
        arregloFila[0] = actual;
        arregloFila[1] = i;
      }
    }

    return arregloFila;
  }

  private float obtenerDivision(float numerador, float denominador) {
    try {
      return (float)(numerador / denominador);
    } catch (DivideByZeroException) {
      return float.MaxValue;
    }
  }

  public void mostrarTabla() {
    if (dosFases) {
      mostrarDosFases();
    } else {
      mostrarUnaFase();
    }
  }
  private void mostrarDosFases() {
    for (int fila = 0; fila < tablaSimplex.GetLength(0); fila++) {
      /*
      if (fila == 0){
        Console.Write("Z{0,2}", nombre[5]);
      }else{
        Console.Write("h{0,2}", nombre[5]);
      }*/
      Console.Write("|");
      for (int columna = 0; columna < tablaSimplex.GetLength(1); columna++) {
        Console.Write(" {0,6:F2} |", this.tablaSimplex[fila, columna]);
      }
      Console.WriteLine();
    }
    Console.WriteLine();
  }

  private void mostrarUnaFase() {
    /*int numH = tablaSimplex.GetLength(0);
     int numX = tablaSimplex.GetLength(1) - 2 - numH;
     string[] nombre = new string[] { "VB", "Z", "X", "H", "LD", "" };
     for (int i = 0; i < tablaSimplex.GetLength(1); i++) {
       if (i <= 1) {
         Console.Write("{0,-10}", nombre[i]);
       }
       if (i >= 1 && i < numX + 2) {
         Console.Write("X{0,-10}");
       }
       if (i >= numX + 2 && i < tablaSimplex.GetLength(1)-1) {
         Console.Write("h{0,3:C}", i-2 );
       }
     }
     Console.WriteLine("{0,-3}",nombre[4] );*/
    for (int fila = 0; fila < tablaSimplex.GetLength(0); fila++) {
      /*
      if (fila == 0){
        Console.Write("Z{0,2}", nombre[5]);
      }else{
        Console.Write("h{0,2}", nombre[5]);
      }*/
      Console.Write("|");
      for (int columna = 0; columna < tablaSimplex.GetLength(1); columna++) {
        Console.Write(" {0,6:F2} |", this.tablaSimplex[fila, columna]);
      }
      Console.WriteLine();
    }
    Console.WriteLine();
  }

  private bool exiteNegativo() {
    for (int i = 1; i < tablaSimplex.GetLength(1) - 1; i++) {
      if (tablaSimplex[0, i] < 0) {
        return true;
      }
    }
    return false;
  }
}
