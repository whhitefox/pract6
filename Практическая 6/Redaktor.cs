namespace Pract6
{
    internal class Redaktor
    {
        private List<string> lines;
        private int xPos;
        private int yPos;
        public Redaktor(List<string> linesParam)
        {
            lines = linesParam;
            if (lines.Count == 0)
            {
                lines.Add("");
            }
            yPos = lines.Count - 1;
            xPos = lines[yPos].Length;
        }

        public bool StartEdit()
        {
            ConsoleKeyInfo key;
            do
            {
                Console.Clear();
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
                Console.SetCursorPosition(xPos, yPos);
                key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        PrevLine();
                        break;
                    case ConsoleKey.DownArrow:
                        NextLine();
                        break;
                    case ConsoleKey.LeftArrow:
                        MoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        MoveRight();
                        break;
                    case ConsoleKey.Backspace:
                        Backspace();
                        break;
                    case ConsoleKey.Enter:
                        NewLine();
                        break;
                    default:
                        if (key.Key != ConsoleKey.F1 && key.Key != ConsoleKey.Escape)
                        {
                            AddSymbol(key.KeyChar.ToString());
                        }
                        break;
                }
            } while (key.Key != ConsoleKey.F1 && key.Key != ConsoleKey.Escape);
            Console.Clear();

            bool closed = false;
            if (key.Key == ConsoleKey.Escape)
            {
                closed = true;
            }
            return closed;
        }

        private void NextLine()
        {
            yPos++;
            if (yPos >= lines.Count)
            {
                yPos = lines.Count - 1;
            }
            if (xPos > lines[yPos].Length)
            {
                xPos = lines[yPos].Length;
            }
        }

        private void PrevLine()
        {
            yPos--;
            if (yPos < 0)
            {
                yPos = 0;
            }
            if (xPos > lines[yPos].Length)
            {
                xPos = lines[yPos].Length;
            }
        }
        private void MoveRight()
        {
            xPos++;
            if (xPos > lines[yPos].Length)
            {
                yPos++;
                if (yPos >= lines.Count)
                {
                    yPos = lines.Count - 1;
                    xPos = lines[yPos].Length;
                }
                else
                {
                    xPos = 0;
                }
            }
        }
        private void MoveLeft()
        {
            xPos--;
            if (xPos < 0)
            {
                yPos--;
                if (yPos < 0)
                {
                    yPos = 0;
                    xPos = 0;
                }
                else
                {
                    xPos = lines[yPos].Length;
                }
            }
        }
        private void Backspace()
        {
            if (xPos > 0)
            {
                lines[yPos] = lines[yPos].Remove(xPos - 1, 1);
                MoveLeft();
            }
            else if (yPos > 0)
            {
                xPos = lines[yPos - 1].Length;
                lines[yPos - 1] += lines[yPos];
                lines.RemoveAt(yPos);
                yPos--;
            }
        }
        public void NewLine()
        {
            string prev = lines[yPos].Substring(0, xPos);
            string next = "";
            if (xPos < lines[yPos].Length)
            {
                next = lines[yPos].Substring(xPos);
            }
            lines[yPos] = prev;
            if (yPos < lines.Count - 1)
            {
                lines.Insert(yPos + 1, next);
            }
            else
            {
                lines.Add(next);
            }
            xPos = 0;
            yPos += 1;
        }
        public void AddSymbol(string ch)
        {
            lines[yPos] = lines[yPos].Insert(xPos, ch);
            xPos++;
        }
    }
}
