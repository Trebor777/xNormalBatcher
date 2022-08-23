using System.Windows.Media;
using System.Xml;

namespace XnormalBatcher.Helpers
{
    internal static class XmlHelper
    {
        public static void SetXmlColor(XmlElement xmlElement, Color colorValue)
        {
            xmlElement.SetAttribute("R", colorValue.R.ToString());
            xmlElement.SetAttribute("G", colorValue.G.ToString());
            xmlElement.SetAttribute("B", colorValue.B.ToString());
        }
        /// <summary>
        /// Parse xml RGB fields as Color
        /// </summary>
        /// <param name="xmlElement"></param>
        /// <returns>a Color</returns>
        public static Color GetXmlColor(XmlElement xmlElement)
        {
            return new Color
            {
                R = byte.Parse(xmlElement.GetAttribute("R")),
                G = byte.Parse(xmlElement.GetAttribute("G")),
                B = byte.Parse(xmlElement.GetAttribute("B")),
                A = 255
            };           
        }

    }
}
