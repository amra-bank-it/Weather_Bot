using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsering
{
    public class Parser
    {
        public static void Parse(string url, ref string city, ref string temp, ref string feels, ref string wind, ref string humidity, ref string pressure, ref string water)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            city = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[2]/h2").First().InnerText;
            temp = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[2]/div[2]/div/div[1]/div[2]/span[2]/text()").First().InnerText;
            feels = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[2]/div[2]/div/div[1]/div[2]/span[4]").First().InnerText;
            wind = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[2]/div[2]/div/div[1]/div[2]/span[8]").First().InnerText;
            humidity = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[2]/div[2]/div/div[1]/div[2]/span[7]").First().InnerText;
            pressure = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[2]/div[2]/div/div[1]/div[2]/span[6]").First().InnerText;
            water = doc.DocumentNode.SelectNodes("/html/body/div[2]/div[2]/div[2]/div/div[1]/div[2]/span[5]").First().InnerText;
        }
    }
}
