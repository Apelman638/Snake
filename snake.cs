using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

// currently does not end game if you make self contact
// needs to run without input

namespace Snake
{
    static class Globals
    {
        public const int WIDTH = 20;
        public const int HEIGHT = 20;
        public static Random random = new Random();
    }
    class Cords
    {
        public int x;
        public int y;
        public Cords(int x1, int y1)
        {
            x = x1;
            y = y1;
        }
    }
    class Snake
    {
        public List<Cords> snakeSegs = new List<Cords>(); 
        public int score => snakeSegs.Count;
        private Cords start = new Cords(10,10);
        public Snake() {
            snakeSegs.Add(start);
        }
        public void movey(int dir)
        {
            snakeSegs.Insert(0, new Cords(snakeSegs[0].x, snakeSegs[0].y - dir)); 
            if (snakeSegs[0].x >= Globals.WIDTH || snakeSegs[0].x < 0 || 
            snakeSegs[0].y >= Globals.HEIGHT || snakeSegs[0].y < 0)
            {
                Console.WriteLine("out of bounds, game over");
                Console.WriteLine("Score: " + score);
                Environment.Exit(0);
            }
            if (snakeSegs[0].x == Progwram.screen.location.x && snakeSegs[0].y == Program.screen.location.y)
            {
                Program.screen.newApple();

            } else
            {
                snakeSegs.RemoveAt(snakeSegs.Count - 1);
            }
        }
        public void movex(int dir)
        {
            snakeSegs.Insert(0, new Cords(snakeSegs[0].x + dir, snakeSegs[0].y)); 
            if (snakeSegs[0].x >= Globals.WIDTH || snakeSegs[0].x < 0 || 
            snakeSegs[0].y >= Globals.HEIGHT || snakeSegs[0].y < 0)
            {
                Console.WriteLine("out of bounds, game over");
                Console.WriteLine("Score: " + score);
                Environment.Exit(0);
            }
            if (snakeSegs[0].x == Program.screen.location.x && snakeSegs[0].y == Program.screen.location.y)
            {
                Program.screen.newApple();
            } else
            {
                snakeSegs.RemoveAt(snakeSegs.Count - 1);
            }
        }
    }
    class Screen
    {
        public Cords location = null!;
        public List<string> elements = new List<string>();
        public Screen()
        {
            for(int i = 1; i <= Globals.WIDTH*Globals.HEIGHT; i++)
            {
                elements.Add(" ");
            }
        }

        public void newApple()
        {
            int spot = Globals.random.Next(Globals.WIDTH*Globals.HEIGHT);
            location = new Cords(spot%Globals.WIDTH, spot / Globals.WIDTH);
            if (elements[spot] == " ")
            {
                elements[spot] = "\uf8ff";
            } else
            {
                newApple();
            }
        }

        public void clearScreen()
        {
            for(int i = 0; i < elements.Count; i++)
            {
                if (elements[i] == "\uf8ff")
                {
                    elements[i] = "\uf8ff";
                } else
                {
                    elements[i] = " ";
                }
            }
        }

        public void updateScreen(Snake snake)
        {
            elements[location.y * Globals.WIDTH + location.x] = "\uf8ff";
            foreach(Cords part in snake.snakeSegs)
            {
                elements[part.y*Globals.WIDTH + part.x] = "\u25A0"; 
            }
        }

        public void printScreen()
        {
            int i = 1;
            foreach(string part in elements)
            {
                Console.Write(part + " ");
                if (i % Globals.WIDTH == 0)
                {
                    Console.WriteLine();
                }
                i += 1;
            }
        }
    }
    class Program
    {
        public static Screen screen = null!;
        public static Snake userSnake = null!;
        public static void init()
        {
            screen = new Screen();
            userSnake = new Snake();
            screen.newApple();
            screen.updateScreen(userSnake);
            screen.printScreen();
        }
        public static void update()
        {
            screen.clearScreen();
            screen.updateScreen(userSnake);
            screen.printScreen();
        }
        static void Main()
        {
            init();
            while (true) {
                Console.Write("wasd: ");
                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                    continue;

                char dir = input[0];              
                switch (dir)
                {
                    case 'w' : userSnake.movey(1); break;
                    case 's' : userSnake.movey(-1); break;
                    case 'a' : userSnake.movex(-1); break;
                    case 'd' : userSnake.movex(1); break;
                    default : Console.WriteLine("Imput not known"); break;
                }
                update();
            }
        }
    }
}