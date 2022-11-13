namespace Pract6
{
    public class Figure
    {
        public string name;
        public int height;
        public int width;

        public Figure()
        {
            name = "";
            height = 0;
            width = 0;
        }
        public Figure(string nameParam, int heightParam, int widthParam)
        {
            name = nameParam;
            height = heightParam;
            width = widthParam;
        }
    }
}
