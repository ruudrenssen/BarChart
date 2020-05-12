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
        private double range = 0;

        public BarChart(JObject json)
        {
            data = json;
            totalValue = bars.Aggregate((double)0, (total, next) => total + next.Value);
            maxValue = bars.Aggregate((double)0, (max, next) => max < next.Value ? next.Value : max);
            minValue = bars.Aggregate((double)0, (min, next) => min > next.Value ? next.Value : min);
            range = Range(minValue, maxValue);
        }

        public XDocument Chart()
        {
            XDocument svg;
            svg = new XDocument(new XElement("svg",
                new XAttribute("class", "barchart"),
                new XAttribute("data-total", totalValue),
                new XAttribute("data-min", minValue),
                new XAttribute("data-max", maxValue),
                new XAttribute("data-range", range)
                ));
            foreach (SingleBar bar in bars)
            {
                svg.Element("svg").Add(bar.Rectangle());
            }

            return svg;
        }

        private double Range(double min, double max)
        {
            double range;

            // Implementation 1: most simple, just 100%
            range = 100;

            // Implementation 2: normalize for largest value
            range = max;

            // Implementation 3: take negative values into account
            if (max < 0)
            {
                range = Math.Abs(min);
            }
            else if (min > 0)
            {
                range = max;
            }
            else
            {
                range = max + Math.Abs(min);
            }

            return range;
        }
    }
}
