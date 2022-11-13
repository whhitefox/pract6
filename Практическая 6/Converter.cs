using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Pract6
{
    internal class Converter
    {
        private List<string> ToText(List<Figure> figures)
        {
            List<string> lines = new List<string>();
            foreach (Figure f in figures)
            {
                lines.Add(f.name);
                lines.Add(f.height.ToString());
                lines.Add(f.width.ToString());
            }
            return lines;
        }
        private List<Figure> FromText(List<string> lines)
        {
            List<Figure> figures = new List<Figure>();

            for (int i = 0; i < lines.Count / 3; i++)
            {
                int group = i * 3;
                string name = lines[group];
                string height = lines[group + 1];
                string width = lines[group + 2];
                if (name.Length == 0 || height.Length == 0 || width.Length == 0)
                {
                    continue;
                }
                Figure figure = new Figure(name, Convert.ToInt32(height), Convert.ToInt32(width));
                figures.Add(figure);
            }

            return figures;
        }
        private string ToJSON(List<Figure> figures)
        {
            string json = JsonConvert.SerializeObject(figures);
            return json;
        }
        private List<Figure> FromJSON(string text)
        {
            List<Figure> figures = JsonConvert.DeserializeObject<List<Figure>>(text);
            return figures;
        }
        private void SaveToXML(List<Figure> figures, string put)
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<Figure>));
            using (FileStream fs = new FileStream(put, FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, figures);
            }
        }
        private List<Figure> FromXMLFile(string put)
        {
            List<Figure> figures;
            XmlSerializer xml = new XmlSerializer(typeof(List<Figure>));
            using (FileStream fs = new FileStream(put, FileMode.Open))
            {
                figures = (List<Figure>)xml.Deserialize(fs);
            }
            return figures;
        }
        public List<string> ReadFile(string put)
        {
            if (put.EndsWith(".xml"))
            {
                List<Figure> figures = FromXMLFile(put);
                return ToText(figures);
            }
            else if (put.EndsWith(".json"))
            {
                string text = File.ReadAllText(put);
                List<Figure> figures = FromJSON(text);
                return ToText(figures);
            }
            else
            {
                return File.ReadAllLines(put).ToList();
            }
        }

        public void SaveToFile(List<string> lines, string put)
        {
            if (put.EndsWith(".xml"))
            {
                List<Figure> figures = FromText(lines);
                SaveToXML(figures, put);
            }
            else if (put.EndsWith(".json"))
            {
                List<Figure> figures = FromText(lines);
                string json = ToJSON(figures);
                File.WriteAllText(put, json);
            }
            else
            {
                List<Figure> figures = FromText(lines);
                List<string> newLines = ToText(figures);
                File.WriteAllLines(put, newLines);
            }
        }
    }
}
