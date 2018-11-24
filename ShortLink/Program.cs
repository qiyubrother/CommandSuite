using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace ShortLink
{
    class Program
    {
        static void Main(string[] args)
        {
            var dic = new Dictionary<string, string>();
            foreach (var s in args)
            {
                var arr = s.Split(new [] {":="}, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length != 2)
                {
                    Error();
                    return;
                }
                else
                {
                    dic[arr[0]] = arr[1];
                }
            }
            if (!dic.ContainsKey("-url"))
            {
                Error();
                return;
            }
            if (!dic.ContainsKey("-out"))
            {
                Error();
                return;
            }
            var url = "https://www.baidu.com/";
            var urlTemplate = "http://api.t.sina.com.cn/short_url/shorten.xml?source=3271760578&url_long={0}";
            url = string.Format(urlTemplate, url);
            if (url.Contains("-url"))
            {

                url = string.Format("{0}&rndkey={1}", url, Guid.NewGuid().ToString().Substring(0, 10));
            }
            else
            {
                url = string.Format("{0}?rndkey={1}", url, Guid.NewGuid().ToString().Substring(0, 10));
            }
            var shortURL = GetGeneralContent(url);
            if (shortURL == string.Empty)
            {
                Console.WriteLine("System error.");
                Environment.Exit(-2);
            }

            var fn = "short.link";
            var sw = new StreamWriter(fn, false);
            sw.Write(shortURL);
            sw.Close();

            Console.WriteLine(shortURL);
        }

        static void Error()
        {
            Console.WriteLine("ShortLink -url:=http://www.baidu.com -out:=short.link");
            Environment.Exit(-1);
        }

        static string GetGeneralContent(string strUrl)
        {
            var strMsg = string.Empty;
            try
            {
                var request = WebRequest.Create(strUrl);
                var response = request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312"));

                strMsg = reader.ReadToEnd();

                reader.Close();
                reader.Dispose();
                response.Close();
            }
            catch{ }

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(strMsg);

            var node = xmlDoc.SelectSingleNode("urls/url/url_short");
            if (node != null)
            {
                return node.InnerText;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
