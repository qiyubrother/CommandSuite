using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace gridcol
{
    class Program
    {
        static void Main(string[] args)
        {
            var param = new CommandParameterHelper(args);
            if (!(param.Parameters.ContainsKey("file")
                && param.Parameters.ContainsKey("gridsplit")
                && param.Parameters.ContainsKey("col")
                && param.Parameters.ContainsKey("newsplit")))
            {
                Error();
                return;
            }

            var fileName = param.Parameters["file"];
            var gridsplit = param.Parameters["gridsplit"];
            var col = param.Parameters["col"];
            var newsplit = param.Parameters["newsplit"];

            if (col.Length < 3 || col[0] != '[' || col[col.Length - 1] != ']')
            {
                Error();
                return;
            }
            if (newsplit.Length < 2 || newsplit[0] != '[' || newsplit[newsplit.Length - 1] != ']')
            {
                Error();
                return;
            }
            col = col.Substring(1, col.Length - 2);
            newsplit = newsplit.Substring(1, newsplit.Length - 2);
            var arrCol = col.Split(new[] { ',' });
            var split = string.Empty;
            if (gridsplit == "tab")
            {
                split = "\t";
            }
            else if (gridsplit == "comma")
            {
                split = ",";
            }
            else if (gridsplit == "space")
            {
                split = " ";
            }
            else
            {
                Error();
                return;
            }
            if (newsplit == "tab")
            {
                newsplit = "\t";
            }
            else if (newsplit == "comma")
            {
                newsplit = ",";
            }
            else if (newsplit == "space")
            {
                newsplit = " ";
            }
            else
            {
                Error();
                return;
            }
            foreach (var colIndex in arrCol)
            {
                var i = 0;
                if (!int.TryParse(colIndex, out i))
                {
                    Error();
                    return;
                }
            }
            using (var sr = new StreamReader(fileName))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (line == null || line.Trim() == string.Empty)
                    {
                        Console.WriteLine(string.Empty);
                        continue;
                    }
                    var items = line.Split(new[] {split}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var index in arrCol)
                    {
                        Console.Write(items[Convert.ToInt32(index) - 1]);
                        Console.Write(newsplit);
                    }
                    Console.WriteLine(string.Empty);
                }
            }
        }

        private static void Error()
        {
            Console.Error.WriteLine("gridcol -file:data.txt -gridsplit:tab   -col:[2,6] -newsplit:[\"|\"]");
            Console.Error.WriteLine("gridcol -file:data.txt -gridsplit:comma -col:[2,6] -newsplit:[\"|\"]");
            Console.Error.WriteLine("gridcol -file:data.txt -gridsplit:space -col:[2,6] -newsplit:[\"|\"]");
            return;
        }
    }
}
