using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
namespace ClearComments
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                MessageBox.Show("Error Path");
                return;
            }

            var path = args[0];

            var csFiles = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);
            var cnt = 0;
            foreach(var fileName in csFiles)
            {
                var tmpFileName = fileName + "_";
                var sr = new StreamReader(fileName);
                var sw = new StreamWriter(tmpFileName, false, Encoding.UTF8);
                while(!sr.EndOfStream)
                {
                    var str = sr.ReadLine();
                    var fmtStr = str.Trim();
                    if (fmtStr.Length >= 2 && fmtStr.Substring(0, 2) == "//")
                    {
                        // Continue;
                    }
                    else if (fmtStr.Length >= 7 && fmtStr.Substring(0, 7) == "#region")
                    {
                        // Continue;
                    }
                    else if (fmtStr.Length >= 10 && fmtStr.Substring(0, 10) == "#endregion")
                    {
                        // Continue;
                    }
                    else
                    {
                        sw.WriteLine(str);
                    }
                }
                sr.Close();
                sw.Close();
                cnt++;
                try
                {
                    File.Delete(fileName);
                    File.Move(tmpFileName, fileName);
                }
                catch
                {
                    ;
                }
            }
            MessageBox.Show(string.Format("执行完毕。共处理 {0} 个cs文件！", cnt));
        }
    }
}
