using System;
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
        private double maxPercentage => MaxValue * 100 / TotalValue;
        private double percentage => Value * 100 / TotalValue;
        private double width => Math.Abs(percentage) * factor;
        private double factor => 100 / Range();
        private double xPosition => Value > 0 ? Math.Abs(minPercentage * factor) : 0;

        public XElement Rectangle()
        {
            XElement rect = new XElement("rect",
                new XAttribute("width", width.ToString() + "%"),
                new XAttribute("height", 100.ToString() + "%"),
                new XAttribute("x", xPosition.ToString() + "%"));

            return rect;
        }

        public XDocument SVG()
        {
            XDocument bar = new XDocument("svg");

            return bar;
        }

        private double Range()
        {
            double range;

            // Implementation 1: most simple, just 100%
            range = 100;

            // Implementation 2: normalize for largest value
            range = MaxValue;

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
