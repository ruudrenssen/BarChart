using System;
using System.Globalization;
using System.Xml.Linq;

namespace BarChart.Charts
{
    public class SingleBar
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public double TotalValue { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        private double minPercentage => MinValue * 100 / TotalValue;
        private double percentage => Value * 100 / TotalValue;
        private double width => Math.Abs(percentage) * factor;
        private double factor => TotalValue / Range();
        private double xPosition => Value > 0 ? Math.Abs(minPercentage * factor) : 0;

        public XElement Rectangle(double height = 100, double y = 0)
        {
            CultureInfo ci = new CultureInfo("en-us");

            XElement rect = new XElement("rect",
                new XAttribute("width", width.ToString("F01", ci) + "%"),
                new XAttribute("height", height.ToString("F01", ci) + "%"),
                new XAttribute("x", xPosition.ToString("F01", ci) + "%"),
                new XAttribute("y", y.ToString("F01", ci) + "%"));

            return rect;
        }

        public XDocument SVG()
        {
            XDocument bar = new XDocument("svg");
            bar.Element("svg").Add(Rectangle());

            return bar;
        }

        private double Range()
        {
            double range;

            // Implementation 3: take negative values into account
            if (MaxValue < 0)
            {
                range = Math.Abs(MinValue);
            }
            else if (MinValue > 0)
            {
                range = MaxValue;
            }
            else
            {
                range = MaxValue + Math.Abs(MinValue);
            }

            return range;
        }
    }
}
