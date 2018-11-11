using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1_RegressaoLinear
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Coluna 2 = Preço
            //Coluna 9 = VagasGaragem
            //Coluna 3 = AreaUtil
            //Coluna 11 = ValorIPTU
            //Coluna 14 = Bairro

            var ListaPrecos = ConvertStringListToDouble(ReadExcelReturnColumn(2));
            var ListaVagasGaragem = ConvertStringListToDouble(ReadExcelReturnColumn(9));
            var ListaAreasUteis = ConvertStringListToDouble(ReadExcelReturnColumn(3));
            var ListaValorIPTU = ConvertStringListToDouble(ReadExcelReturnColumn(11));
            var ListaBairros = ReadExcelReturnColumn(14);
            //�

            List<double> NewListaPrecos = new List<double>();
            List<double> NewListaVagasGaragem = new List<double>();
            List<double> NewListaAreasUteis = new List<double>();
            List<double> NewListaValorIPTU = new List<double>();
            var AltoBarrocaLista = new List<string>();

            for (int i = 0; i < ListaBairros.Count; i++)
            {
                if (ListaBairros.ElementAt(i).Equals("Alto Barroca"))
                {
                    AltoBarrocaLista.Add(ListaBairros.ElementAt(i));
                    NewListaPrecos.Add(ListaPrecos.ElementAt(i));
                    NewListaAreasUteis.Add(ListaAreasUteis.ElementAt(i));
                    NewListaValorIPTU.Add(ListaValorIPTU.ElementAt(i));
                    NewListaVagasGaragem.Add(ListaVagasGaragem.ElementAt(i));
                }
            }

            Console.WriteLine("1) Preco x VagasGaragem (Todos os bairros): " + BaseCalculo(AltoBarrocaLista, NewListaPrecos, NewListaVagasGaragem/*, "todos"*/));
            Console.WriteLine("2) Preco x Area Util (Todos os bairros):  " + BaseCalculo(AltoBarrocaLista, NewListaPrecos, NewListaAreasUteis/*, "todos"*/));
            Console.WriteLine("3) Preco x Valor IPTU  (Todos os bairros): " + BaseCalculo(AltoBarrocaLista, NewListaPrecos, NewListaValorIPTU/*, "todos"*/)); 

            Console.WriteLine("4) Preco x Area Util (Bairro Alto Barroca ): " + BaseCalculo(AltoBarrocaLista, NewListaPrecos, ListaAreasUteis/*, "Alto Barroca"*/));

            Console.ReadKey();
        }





        public static double MinimosQuadrados(double x, double y, double x2, double xy, double qtd)
        {
            var A = ((x * y) - (qtd * xy)) / Math.Pow(x, 2) - (qtd * (x2));
            var B = ((y) - A * x) / qtd;
            var total = B + (A * x);
            return total;
        }

        public static double BaseCalculo(List<string> Bairros, List<double> Precos, List<double> ValoresVariantes/*, string Condicional*/)
        {
            double x = 0;
            double y = 0;
            double x2 = 0;
            double xy = 0;
            double qtd = 0;

            //for(int i = 0; i < Bairros.Count; i++)
            //{
            //    if (Condicional == "todos")
            //    {
            //        x += Precos.ElementAt(i); 
            //        y += Valores.ElementAt(i);
            //        x2 += Math.Pow(Precos.ElementAt(i), 2);
            //        xy += Precos.ElementAt(i) * Valores.ElementAt(i);
            //        qtd += 1;
            //    }
            //    else if (Bairros.ElementAt(i) == Condicional)
            //    {
            //        x += Precos.ElementAt(i);
            //        y += Valores.ElementAt(i);
            //        x2 += Math.Pow(Precos.ElementAt(i), 2);
            //        xy += Precos.ElementAt(i) * Valores.ElementAt(i);
            //        qtd += 1;
            //    }
            //}

            for (int i = 0; i < Bairros.Count; i++)
            {
                x += Precos.ElementAt(i);
                y += ValoresVariantes.ElementAt(i);
                x2 += Math.Pow(Precos.ElementAt(i), 2);
                xy += Precos.ElementAt(i) * ValoresVariantes.ElementAt(i);
                qtd += 1;
            }

            return MinimosQuadrados(x, y, x2, xy, qtd);
        }

        public static List<string> ReadExcelReturnColumn(int ColumnIndex)
        {
            List<string> listA = new List<string>();

            using (var reader = new StreamReader(Environment.CurrentDirectory + @"\..\..\imobiliaria_20181026100739.csv"))
            {
                var first = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    listA.Add(values[ColumnIndex]);
                }
            }

            return listA;
        }

        public static List<double> ConvertStringListToDouble(List<string> Columns)
        {
            List<double> Colunas = new List<double>();

            foreach (var item in Columns)
            {
               Colunas.Add(Convert.ToDouble(item));
            }

            return Colunas;
        }
    }
}
