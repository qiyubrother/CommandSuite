using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace getlink
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.Error.WriteLine("getlink -all http://www.sina.com.cn");
                Console.Error.WriteLine("getlink -e:jpg http://www.sina.com.cn");
                Console.Error.WriteLine("getlink -c:163 http://www.sina.com.cn");
                Console.Error.WriteLine("getlink -chunked http://www.sina.com.cn");
                return;
            }
            var param = args[0];
            var url = args[1];

            try
            {
                var links = GetHyperLinks(GetPageSource(url));
                foreach (var link in links)
                {
                    if (param == "-all")
                    {
                        Console.WriteLine(link);
                    }
                    else if (param.StartsWith("-e:"))
                    {
                        var s = param.Substring(3);
                        if (link.EndsWith(s, StringComparison.CurrentCultureIgnoreCase))
                        {
                            Console.WriteLine(link);
                        }
                    }
                    else if (param.StartsWith("-c:"))
                    {
                        var s = param.Substring(3);
                        if (link.ToLower().Contains(s.ToLower()))
                        {
                            Console.WriteLine(link);
                        }
                    }
                    else if (param.StartsWith("-chunked"))
                    {
                        var l = link.ToLower();
                        if (l.EndsWith("jpg")
                            || l.EndsWith("js")
                            || l.EndsWith("gif")
                            || l.EndsWith("css")
                            )
                        {
                            continue;
                        }
                        var header = GetPageHeader(link).ToLower();
                        if (header.Contains("chunked"))
                        {
                            Console.WriteLine(link);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

        }

        // 获取指定网页的HTML代码
        static string GetPageSource(string url)
        {
            var uri = new Uri(url);
            var hwReq = (HttpWebRequest)WebRequest.Create(uri);
            var hwRes = (HttpWebResponse)hwReq.GetResponse();
            hwReq.Method = "Get";
            hwReq.KeepAlive = false;
            var reader = new StreamReader(hwRes.GetResponseStream(), System.Text.Encoding.GetEncoding("GB2312"));

            var str = reader.ReadToEnd();
            reader.Close();
            hwRes.Close();

            return str;
        }
        // 提取HTML代码中的网址
        static IEnumerable<string> GetHyperLinks(string htmlCode)
        {
            var lst = new List<string>();
            const string strRegex = @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";

            var r = new Regex(strRegex, RegexOptions.IgnoreCase);
            var m = r.Matches(htmlCode);

            for (var i = 0; i <= m.Count - 1; i++)
            {
                lst.Add(m[i].ToString());
            }

            return lst.Distinct();
        }

        static string GetPageHeader(string url)
        {
            var uri = new Uri(url);
            var hwReq = (HttpWebRequest)WebRequest.Create(uri);
            var hwRes = (HttpWebResponse)hwReq.GetResponse();
            var buffer = hwRes.Headers.ToByteArray();
            var header = Encoding.UTF8.GetString(buffer, 0, buffer.Length);

            return header;
        }
    }
}
