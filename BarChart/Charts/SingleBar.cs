using System;
using System.Xml.Linq;

namespace BarChart.Charts
{
    public class SingleBar
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public double TotalValue { get; set; }
        public double Percentage => Value * 100 / TotalValue;
        public double Width => Math.Abs(Percentage);

        public XElement Rectangle()
        {
            XElement rect = new XElement("rect",
                new XAttribute("width", Width.ToString() + "%"),
                new XAttribute("height", 100.ToString() + "%"));

            return rect;
        }

        public XDocument SVG()
        {
            XDocument bar = new XDocument("svg");

            return bar;
        }
    }
}
