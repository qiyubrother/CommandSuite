using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace col
{
    class Program
    {
        static void Main(string[] args)
        {
            var param = new CommandParameterHelper(args);
            if (!(param.Parameters.ContainsKey("file")
                && param.Parameters.ContainsKey("col")
                && param.Parameters.ContainsKey("split")))
            {
                Error();
                return;
            }
            var fileName = param.Parameters["file"];
            var col = param.Parameters["col"];
            var split = param.Parameters["split"];

            if (col.Length < 3 || col[0] != '[' || col[col.Length - 1] != ']')
            {
                Error();
                return;
            }
            col = col.Substring(1, col.Length - 2);
            var arrCol = col.Split(new[] {','});
            var fields = new List<FieldRange>();
            foreach (var arr in arrCol)
            {
                var range = arr.Split(new[] {'-'});
                if (range.Length == 1)
                {
                    fields.Add(new FieldRange{
                        From = Convert.ToInt32(range[0]),
                        To = Convert.ToInt32(range[0])
                    } );
                }
                else if (range.Length == 2)
                {
                    fields.Add(new FieldRange
                    {
                        From = Convert.ToInt32(range[0]),
                        To = Convert.ToInt32(range[1])
                    });
                }
                else
                {
                    Error();
                    return;
                }
            }
            if (split.Length < 3 || split[0] != '[' || split[split.Length - 1] != ']')
            {
                Error();
                return;
            }
            split = split.Substring(1, split.Length - 2);

            using (var sr = new StreamReader(fileName))
            {
                var lineNumber = 1;
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    foreach (var field in fields)
                    {
                        try
                        {
                            var v = line.Substring(field.From - 1, field.To - field.From + 1);
                            Console.Write(v);
                            Console.Write(split);
                        }
                        catch 
                        {
                            Console.Error.Write("[E][{0}]{1}", lineNumber, line);
                            break;
                        }
                    }
                    Console.WriteLine(string.Empty);
                    lineNumber++;
                }
            }
        }

        private static void Error()
        {
            Console.Error.WriteLine("col -file:data.txt -col:[2-4,6-8] -split:[\"|\"]");
            Console.Error.WriteLine("col -file:data.txt -col:[2-4,6-8] -split:[\",\"]");
            Console.Error.WriteLine("col -file:data.txt -col:[2-4,6-8] -split:[\" \"]");
            return;
        }
    }

    class FieldRange 
    {
        public int From;
        public int To;
    }
}
