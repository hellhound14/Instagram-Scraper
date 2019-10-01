using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace Web
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Input Account Name: ");
            Gethtml(Console.ReadLine());
            Console.ReadKey();
        }
        private static async void Gethtml(string acc)
        {
            var ul = "https://www.instagram.com/"+acc;
            Console.WriteLine();
            Console.SetWindowSize(170, 62);
            HttpClient http = new HttpClient();
            string htmlreturn = await http.GetStringAsync(ul);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlreturn);
            FindCorrectTag(doc);
        }
        private static void FindCorrectTag(HtmlDocument document) {
            var metatags = document.DocumentNode.SelectNodes("//meta")
                .Where(node=>node.GetAttributeValue("name","")
                .Equals("description"));

            foreach (var metatag in metatags)
            {
                var content = metatag.GetAttributeValue("content", "");
                Console.Write(BreakString(content,'-'));
            }
        }
        private static string BreakString(string inco,char wheretobreak) {
            int indextobreak = inco.IndexOf(wheretobreak);
            return inco.Substring(0,indextobreak);
        }
    }
} 