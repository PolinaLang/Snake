using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(1, 1);
            Console.SetBufferSize(80, 25);
            Console.SetWindowSize(80, 25);

            //отрисовка рамки
            Walls walls = new Walls(80, 25);
            walls.Draw();

            //отрисовка точек
            Point p = new Point(4, 5, '*');
            Snake snake = new Snake(p, 4, Direction.RIGHT);
            snake.Drow();

            FoodCreator foodCreator = new FoodCreator(80, 25, '$');
            Point food = foodCreator.CreateFood();
            food.Draw();

            Console.CursorVisible = false;

            while (true)
            {
                if (walls.IsHit(snake) || snake.IsHitTail())
                {
                    break;
                }
                if (snake.Eat(food))
                {
                    food = foodCreator.CreateFood();
                    food.Draw();
                    //snake.Drow();
                } else
                {
                    snake.Move();
                }
                Thread.Sleep(100);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.HandleKey(key.Key);
                }
                Thread.Sleep(100);
            }
            WriteGameOver();
            Console.ReadLine();

            Console.ReadKey();
        }

        static void WriteGameOver()
        {
            int xOffset = 33;
            int yOffset = 8;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(xOffset, yOffset++);
            WriteText("==============", xOffset, yOffset++);
            WriteText("GAME    OVER", xOffset + 1, yOffset++);
            WriteText("==============", xOffset, yOffset++);
        }

        static void WriteText(String text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }
    }
}
