namespace Simplex
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    class Simplex
    {
        private List<string> entradaLista;
        private double[,] matrizEcuaciones;
        public int variable;
        int filMat, colMat;
        public double[] ValoresFila0;
        public Simplex(List<string> nuevaEntrada)
        {
            entradaLista = nuevaEntrada;
            filMat = entradaLista.Count;
            colMat = 3 + entradaLista.Count;
            variable = 1;
            ValoresFila0 = new double[colMat];
            matrizEcuaciones = new double[filMat, colMat];
            IdentificarMatriz();
            Actualizarfila0();
            IdentificarVariable(variable);



        }

        public List<string> EntradaLista { get => entradaLista; set => entradaLista = value; }
        public double[,] MatrizEcuaciones { get => matrizEcuaciones; set => matrizEcuaciones = value; }


        /// <summary>
        /// Funcion para imprimir la matriz de valores
        /// </summary>
        public void PrintMatrizEcuaciones()
        {
            Console.WriteLine("Z   X1  X2   h1->hn          LD");
            for (int i = 0; i < filMat; i++)
            {
                for (int j = 0; j < colMat; j++)
                {
                    Console.Write(MatrizEcuaciones[i, j]);
                    Console.Write("   ");
                }
                Console.Write('\n');
            }
        }




        /// <summary>
        /// Funcion que transforma el texto recibido por la clase y lo transforma a una matriz con valores correspondientes
        /// </summary>
        public void IdentificarMatriz()
        {

            for (int i = 0; i < entradaLista.Count; i++)
            {

                Match match = Regex.Match(entradaLista[i], @"([-+]?[0-9]*\.?[0-9]+)([a-zA-Z]+)\s*(=)\s*([-+]?[0-9]*\.?[0-9]+)([a-zA-Z]+)\s*([-+])\s*([-+]?[0-9]*\.?[0-9]+)([a-zA-Z]+)");
                Match match2 = Regex.Match(entradaLista[i], @"([-+]?[0-9]*\.?[0-9]+)\s*(<=|>=|<|>)\s*([-+]?[0-9]*\.?[0-9]+)([a-zA-Z]+)\s*(<=|>=|<|>)\s*([-+]?[0-9]*\.?[0-9]+)");
                Match match3 = Regex.Match(entradaLista[i], @"([-+]?[0-9]*\.?[0-9]+)([a-zA-Z]+)\s*([-+])\s*([-+]?[0-9]*\.?[0-9]+)([a-zA-Z]+)\s*(<=|>=|<|>)\s*([-+]?[0-9]*\.?[0-9]+)");
                if (match.Success)
                {
                    matrizEcuaciones[i, 0] = int.Parse(match.Groups[1].Value);
                    matrizEcuaciones[i, 1] = -int.Parse(match.Groups[4].Value);
                    matrizEcuaciones[i, 2] = -int.Parse(match.Groups[7].Value);
                }
                if (match2.Success)
                {
                    if (i == 1)
                    {
                        matrizEcuaciones[i, 0] = int.Parse(match2.Groups[1].Value);
                        matrizEcuaciones[i, 1] = int.Parse(match2.Groups[3].Value);
                        matrizEcuaciones[i, filMat + 2] = int.Parse(match2.Groups[6].Value);
                        matrizEcuaciones[i, i + 2] = 1;
                    }
                    else
                    {
                        matrizEcuaciones[i, 1] = int.Parse(match2.Groups[1].Value);
                        matrizEcuaciones[i, 2] = int.Parse(match2.Groups[3].Value);
                        matrizEcuaciones[i, filMat + 2] = int.Parse(match2.Groups[6].Value);
                        matrizEcuaciones[i, i + 2] = 1;
                    }
                }
                if (match3.Success)
                {
                    matrizEcuaciones[i, 1] = int.Parse(match3.Groups[1].Value);
                    matrizEcuaciones[i, 2] = int.Parse(match3.Groups[4].Value);
                    matrizEcuaciones[i, filMat + 2] = int.Parse(match3.Groups[7].Value);
                    matrizEcuaciones[i, i + 2] = 1;
                }
            }
        }

        /// <summary>
        ///  Funcion que actualiza la columna de la variable para usarse en los pivotes
        /// </summary>
        /// <param name="posVar">Posicion Actual de la variable en la columna de la matriz</param>
        public void IdentificarVariable(int posVar)
        {
            for (int i = 1; i < colMat; i++)
            {
                if (MatrizEcuaciones[0, posVar] > MatrizEcuaciones[0, i]) { variable = i; }
            }


        }

        /// <summary>
        /// Devuelve la Fila Pivote
        /// </summary>
        /// <returns>Numero de Fila pivote</returns>
        public int MenorDivision()
        {

            double[] listaBuscar = ListaMenorDivision();
            double t = int.MaxValue;
            int pos = 0;
            for (int i = 0; i < listaBuscar.Length; i++)
            {
                if (listaBuscar[i] < t && listaBuscar[i] > 0)
                {
                    t = listaBuscar[i];
                    pos = i;

                }

            }
            Console.WriteLine("Lista de Divisores LD/ColVariable");
            foreach (var item in listaBuscar)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine(" ");
            return pos + 1;
        }

        /// <summary>
        ///  Devuelve una Lista con las divisiones de LD/columnaPivote
        /// </summary>
        /// <returns>Lista con los valores LD/colPiv</returns>
        double[] ListaMenorDivision()
        {

            double[] temp = new double[filMat - 1];
            for (int j = 1; j < filMat; j++)
            {

                if (MatrizEcuaciones[j, variable] != 0)
                {
                    // añade a la lista el resultado de LD/columna pivote
                    temp[j - 1] = MatrizEcuaciones[j, colMat - 1] / MatrizEcuaciones[j, variable];
                }
                else { temp[j - 1] = 0; }

            }

            return temp;
        }

        /// <summary>
        ///  Reduce todos los terminos de la fila pivote
        /// </summary>
        /// <param name="filPivote">Fila pivote </param>
        public void ReduccionFilaPivote(int filPivote)
        {
            double valcolumna = MatrizEcuaciones[filPivote, variable];
            for (int i = 0; i < colMat; i++)
            {

                MatrizEcuaciones[filPivote, i] = MatrizEcuaciones[filPivote, i] / valcolumna;
            }

        }

        /// <summary>
        /// Actualiza la lista con valores de la fila inicial
        /// Para luego encontrar los valores negativos en ella
        /// </summary>
        public void Actualizarfila0()
        {
            for (int i = 0; i < colMat; i++)
            {
                ValoresFila0[i] = MatrizEcuaciones[0, i];
            }

        }

        /// <summary>
        /// Reduce todos los terminos de las filas NO pivotes
        /// </summary>
        /// <param name="filPivote">Fila pivote que ignora</param>
        public void ReducirNoPivotes(int filPivote)
        {
            for (int i = 0; i < filMat; i++)
            {
                double valcolumna = MatrizEcuaciones[i, variable];
                for (int j = 0; j < colMat; j++)
                {

                    if (i != filPivote)
                    {
                        MatrizEcuaciones[i, j] = MatrizEcuaciones[i, j] - (MatrizEcuaciones[filPivote, j] * valcolumna);

                    }
                }
            }

        }

        /// <summary>
        /// Funcion para hacer funcionar el algoritmo
        /// </summary>
        public void EjecutarAlgoritmo()
        {
            Console.WriteLine("Matriz de valores iniciales");
            PrintMatrizEcuaciones();
            bool ejec = true;
            int cont = 1;
            while (ejec)
            {
                Thread.Sleep(2000);
                Console.WriteLine(" ");
                Console.WriteLine("Iteracion " + cont);
                Console.WriteLine("Columna de Variable: " + variable);
                int x = MenorDivision();
                Console.WriteLine("Posicion Fila pivote: " + x);
                Console.WriteLine(" ");
                ReduccionFilaPivote(x);
                ReducirNoPivotes(x);
                PrintMatrizEcuaciones();
                Actualizarfila0();
                IdentificarVariable(variable);
                ejec = false;
                for (int i = 0; i < colMat; i++)
                {
                    if (ValoresFila0[i] < 0) { ejec = true; }
                }
                cont++;


            }

        }

    }
}