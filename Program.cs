namespace Simplex
{
    using System;

    class testSimplex
    {
        static void Main(string[] args)
        {
            List<string> entrada = new List<string> {
                "1Z = 2x + 2y",
                "0 <= 1x <= 45",
                "0 <= 1y <= 100",
                "2x + 1y <= 100",
                "1x + 3y <= 80"
            };
            // Z=  112  X=44  Y=12

            //List<string> entrada = new List<string>
            //{
            //    "1Z = 25x + 12y",
            //    "0 <= 1x <= 15",
            //    "0 <= 1y <= 20",
            //    "1x + 2y <= 20",
            //    "1x + 1y <= 10"

            //};
            // Z = 399  X= 15  Y=2

            //List<string> entrada = new List<string>
            //{
            //    "1Z = 200x + 150y",
            //    "0 <= 1x <= 75",
            //    "0 <= 1y <= 75",
            //    "3x + 2y <= 120",
            //    "1x + 2y <= 80"
            //};
            // Z=8500  x=20  y=30
            Simplex hola = new Simplex(entrada);
            hola.EjecutarAlgoritmo();
            //hola.PrintMatrizEcuaciones();

            //Console.WriteLine("Iteracion 1");

            //int x = hola.MenorDivision();

            //hola.ReduccionFilaPivote(x);
            //hola.ReducirNoPivotes(x);
            //hola.PrintMatrizEcuaciones();
            //hola.Actualizarfila0();

            //Console.WriteLine("Iteracion 2");

            //hola.IdentificarVariable(hola.variable);

            //x = hola.MenorDivision();

            //hola.ReduccionFilaPivote(x);
            //hola.ReducirNoPivotes(x);
            //hola.PrintMatrizEcuaciones();
            //hola.Actualizarfila0();

            //Console.WriteLine("Iteracion 3");

            //hola.IdentificarVariable(hola.variable);
            //Console.WriteLine(hola.variable);
            //x = hola.MenorDivision();
            //hola.ReduccionFilaPivote(x);
            //hola.ReducirNoPivotes(x);
            //hola.PrintMatrizEcuaciones();
            //hola.Actualizarfila0();



        }

    }
}