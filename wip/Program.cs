using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace wip
{
    class Program
    {
        static Dictionary<string, string> dicParameters = new Dictionary<string, string>();
        static void Main(string[] args)
        {
            foreach (var param in args)
            {
                var arrs = param.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                if (arrs.Length == 2)
                {
                    dicParameters[arrs[0]] = arrs[1];
                }
                else
                {
                    Error();
                    return;
                }
            }
            if (!dicParameters.Keys.Contains("-retry"))
            {
                Error();
                return;
            }
            int retry = 0;
            if (!int.TryParse(dicParameters["-retry"], out retry))
            {
                Error();
                return;
            }
            for (var i = 0; i < retry; i++)
            {
                var content = GetGeneralContent("http://ip.chinaz.com/getip.aspx");
                if (content != string.Empty)
                {
                    var cs = content.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    var ip = string.Empty;
                    var address = string.Empty;
                    try
                    {
                        ip = cs[0].Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[1].Replace("'", string.Empty);
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        address = cs[1].Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[1].Replace("'", string.Empty);
                    }
                    catch (Exception)
                    {
                    }
                    Console.WriteLine("{0}", ip);
                    break;
                }
                else
                {
                    Console.WriteLine("FAILED ({0}/{1}) [{2}]", i + 1, retry, DateTime.Now);
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        static void Error()
        {
            Console.WriteLine("wip -retry:30");
        }

        static private string GetGeneralContent(string strUrl)
        {
            var strMsg = string.Empty;
            try
            {
                var request = WebRequest.Create(strUrl);
                var response = request.GetResponse();
                var sm = response.GetResponseStream();
                var reader = new StreamReader(sm, Encoding.GetEncoding("utf-8"));

                strMsg = reader.ReadToEnd();

                reader.Close();
                reader.Dispose();
                response.Close();
            }
            catch
            {
                return string.Empty;
            }
            return strMsg;
        }
    }
}
