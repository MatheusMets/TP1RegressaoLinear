using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

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

            var Precos = ConvertStringListToDouble(ReadExcelReturnColumn(2));
            var VagasGaragem = ConvertStringListToDouble(ReadExcelReturnColumn(9));
            var AreasUteis = ConvertStringListToDouble(ReadExcelReturnColumn(3));
            var ValorIPTU = ConvertStringListToDouble(ReadExcelReturnColumn(11));
            var Bairros = ReadExcelReturnColumn(14);

            foreach (var a in Bairros)
            {
                Console.WriteLine(a);
            }

            Console.ReadKey();
        }

        public static double MinimosQuadrados(double x, double y, double x2, double xy, double qtd)
        {
            var A = ((x * y) - (qtd * xy)) / Math.Pow(x, 2) - (qtd * (x2));
            var B = ((y) - A * x) / qtd;
            var total = B + A * x;
            return total;
        }

        //public static double BaseCalculos(string Bairro, string Preco, string Valor, string Condicional)
        //{
        //    Dictionary<string, string> Items = new Dictionary<string, string>()
        //        {
        //            {"Bairro" , Bairro},
        //            {"Preco" , Preco},
        //            {"Valor" , Valor}
        //        };
            

        //    var x = 0;
        //    var y = 0.0;
        //    var x2 = 0;
        //    var xy = 0;
        //    var qtd = 0;

        //    foreach(string Item in Items.Keys)
        //    {
        //        if (Condicional == "todos")
        //        {
        //            x += preco;
        //            y += valor;
        //            x2 += Math.Pow(preco, 2);
        //            xy += preco * valor;
        //            qtd += 1;
        //        }
        //        else if (bairro == Condicional)
        //        {
        //            x += preco;
        //            y += valor;
        //            x2 += Math.Pow(preco, 2);
        //            xy += preco * valor;
        //            qtd += 1;
        //        }
        //    }
        //    return MinimosQuadrados(x, y, x2, xy, qtd);
        //}

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
                    //break;
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
