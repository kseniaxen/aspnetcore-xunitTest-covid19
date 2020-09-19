using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.AccessControl;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using System.Text;

namespace SimpleApp.Models {

    public class Covid19 {

        public int infected { get; set; }
        public int dead { get; set; }
        public int recovered { get; set; }
        public double deadPt { get; set; }
        public double recPt { get; set; }

        public static List<Covid19> Get(){
             HttpWebRequest reqw =
              (HttpWebRequest)HttpWebRequest.Create("https://ru.wikipedia.org/wiki/%D0%92%D1%81%D0%BF%D1%8B%D1%88%D0%BA%D0%B0_COVID-19");
            HttpWebResponse resp = (HttpWebResponse)reqw.GetResponse(); //создаем объект отклика
            StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.Default);

            var parser = new HtmlParser();
            var document = parser.ParseDocument(resp.GetResponseStream());
            //Console.WriteLine(document.DocumentElement.OuterHtml);
            IElement tableElement =
                document.QuerySelector("#covid19-container>table");
            int count = 0;
            int totalInfected = 0;
            int totalDead = 0;
            int totalRecovered = 0;
            var rows = tableElement.QuerySelectorAll("tbody > tr");
            foreach (var item in rows)
            {
                if (count != 0 && count != rows.Count() - 1)
                {
                    try
                    {
                        if (!item.Children[0].InnerHtml.Contains("Макао"))
                        {
                            if (count != 1)
                            {
                                totalInfected +=
                                (item.Children[2].InnerHtml != "") ? Int32.Parse(string.Join("", item.Children[2].InnerHtml.Where(c => char.IsDigit(c)))) : 0;
                            }
                            totalDead +=
                                (item.Children[4].InnerHtml != "") ? Int32.Parse(string.Join("", item.Children[4].InnerHtml.Where(c => char.IsDigit(c)))) : 0;
                            totalRecovered +=
                                (item.Children[3].InnerHtml != "") ? Int32.Parse(string.Join("", item.Children[3].InnerHtml.Where(c => char.IsDigit(c)))) : 0;
                        }
                    }
                    catch (Exception)
                    {
                        // throw;
                    }
                    // Console.WriteLine();
                }
                      
                count++;
            }
            Covid19 covid = new Covid19();
            covid.infected = totalInfected;
            covid.dead = totalDead;
            covid.recovered = totalRecovered;
            // Console.WriteLine(totalInfected);
            // Console.WriteLine(totalDead);
            // Console.WriteLine(totalRecovered);
            

            var deadPt = ((double)totalDead / (double)totalInfected) * 100d;
            var recPt = ((double)totalRecovered / (double)totalInfected) * 100d;
            // Console.WriteLine(deadPt);
            // Console.WriteLine(recPt);
            covid.deadPt = Math.Round(deadPt,2);
            covid.recPt = Math.Round(recPt,2);
            return new List<Covid19> {covid};
        }
    }
}