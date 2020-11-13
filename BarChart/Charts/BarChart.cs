using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace BarChart.Charts
{
    public class BarChart
    {
        private JObject data;
        private List<SingleBar> bars;
        private double totalValue = 0;
        private double maxValue = 0;
        private double minValue = 0;

        public BarChart(JObject json)
        {
            bars = new List<SingleBar>();
            data = json;
            foreach (var bar in data["Bars"])
            {
                SingleBar item = new SingleBar();
                item.Name = bar["Name"].ToString();
                item.Value = (double)bar["Value"];
                bars.Add(item);
            }

            bars = bars.OrderByDescending(bar => Math.Abs(bar.Value)).ToList();

            totalValue = bars.Aggregate((double)0, (total, next) => total + next.Value);
            maxValue = bars.Aggregate((double)0, (max, next) => max < next.Value ? next.Value : max);
            minValue = bars.Aggregate((double)0, (min, next) => min > next.Value ? next.Value : min);
            
            bars.ForEach(bar => bar.TotalValue = totalValue);
            bars.ForEach(bar => bar.MaxValue = maxValue);
            bars.ForEach(bar => bar.MinValue = minValue);
        }

        public XDocument Chart()
        {
            XDocument svg;
            svg = new XDocument(new XElement("svg", new XAttribute("class", "barchart")));
            int index = 0;
            foreach (SingleBar bar in bars)
            {
                double height = (double)100 / bars.Count();
                svg.Element("svg").Add(bar.Rectangle(height, index * height));
                index++;
            }

            return svg;
        }
    }
}
