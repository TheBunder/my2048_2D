using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace my2048_2D
{
    class My2048
    {
        private int[,] array;
        private int Score;
        

        public My2048(int size)
        {
            array = new int[size,size];
            reset(array);
            Score = 0;
            addNum();
            addNum();
        }
        public void addNum()
        {
            Random rand = new Random();
            int num2or4 = rand.Next(100);
            int num = 0;
            if (num2or4 < 85)
                num = 2;
            else
                num = 4;


            int place = rand.Next(cnt0s());
            bool found = false;
            int index0 = 0;
            int corantIndex = 0;
            for (int i = 0; i < array.GetLength(0) && !found; i++)
            {
                for (int j = 0; j < array.GetLength(0)&&!found; j++)
                {
                    if (array[j,i] == 0)
                    {
                        if (index0 == place)
                        {
                            array[j,i] = num;
                            found = true;
                        }
                        index0++;
                    }
                    corantIndex++;
                }
            }

        }
        public void Right2048()
        {
            if (isMovedRight())
            {
                array = moveRight(array);
                ScoreState scoreState = fusionRight();
                array = scoreState.array;
                Score += scoreState.Score;
                array = moveRight(array);
                addNum();
            }
        }
        public void Down2048()
        {
            if (isMovedDown())
            {
                array = moveDown(array);
                ScoreState scoreState = fusionDown();
                array = scoreState.array;
                Score += scoreState.Score;
                array = moveDown(array);
                addNum();
            }
        }

        public void Left2048()
        {
            if (isMovedLeft())
            {
                array = moveLeft(array);
                ScoreState scoreState = fusionLeft();
                array = scoreState.array;
                Score += scoreState.Score;
                array = moveLeft(array);
                addNum();
            }
        }
        public void Up2048()
        {
            if (isMovedUp())
            {
                array = moveUp(array);
                ScoreState scoreState  = fusionUp();
                array = scoreState.array;
                Score += scoreState.Score;
                array = moveUp(array);
                
                addNum();
            }
        }
        private int[,] moveRight(int[,] arr)
        {
            int[,] array = (int[,])arr.Clone();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int u = array.GetLength(0) - 2; u >= 0; u--)
                {
                    bool blocked = false;
                    for (int j = 0; j < array.GetLength(0) && !blocked; j++)
                    {
                        if (u + 1 + j < array.GetLength(0)  && array[i,u + 1 + j] == 0)
                        {
                            array[i,u + 1 + j] = array[i,u + j];
                            array[i,u + j] = 0;
                        }
                        else
                            blocked = true;
                    }
                }

            }
            return array;
        }
        private int[,] moveDown(int[,] arr)
        {
            int[,] array = (int[,])arr.Clone();
            for (int i = array.GetLength(0) - 2; i >= 0; i--)
            {
                for (int u = 0; u < array.GetLength(0) ; u++)
                {
                    bool blocked = false;
                    for (int j = 0; j < array.GetLength(0) && !blocked; j++)
                    {
                        if (i + 1 + j < array.GetLength(0) && array[i+1+j, u] == 0)
                        {
                            array[i + 1 + j, u] = array[i + j, u];
                            array[i + j, u] = 0;
                        }
                        else
                            blocked = true;
                    }
                }

            }
            return array;
        }
        private int[,] moveLeft(int[,] arr)
        {
            int[,] array = (int[,])arr.Clone();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int u = 1; u < array.GetLength(0) ; u++)
                {
                    bool blocked = false;
                    for (int j = 0; j < array.GetLength(0) && !blocked; j++)
                    {
                        if (u - 1 - j >= 0 && array[i,u - 1 - j] == 0)
                        {
                            array[i,u - 1 - j] = array[i,u - j];
                            array[i,u - j] = 0;
                        }
                        else
                            blocked = true;
                    }
                }

            }
            return array;
        }
        private int[,] moveUp(int[,] arr)
        {
            int[,] array = (int[,])arr.Clone();
            for (int i = 1; i < array.GetLength(0); i++)
            {
                for (int u = 0; u < array.GetLength(0); u++)
                {
                    bool blocked = false;
                    for (int j = 0; j < array.GetLength(0) && !blocked; j++)
                    {
                        if (i - 1 - j >= 0 && array[i - 1 - j, u] == 0)
                        {
                            array[i - 1 - j, u] = array[i - j, u];
                            array[i - j, u] = 0;
                        }
                        else
                            blocked = true;
                    }
                }

            }
            return array;
        }
        private ScoreState fusionRight()
        {
            int points = 0;
            int[,] array = (int[,])this.array.Clone();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = array.GetLength(0) - 2; j >= 0; j--)
                {
                    if (array[i,j] == array[i,j + 1])
                    {
                        array[i,j + 1] += array[i, j];
                        points += array[i, j + 1];
                        array[i, j] = 0;
                    }
                }
            }
            return new ScoreState(array, points);
        }
        private ScoreState fusionDown()
        {
            int points = 0;
            int[,] array = (int[,])this.array.Clone();
            
            for (int i = array.GetLength(0) - 2; i >= 0; i--)
            {
                for (int j = 0; j < array.GetLength(0); j++)
                {
                    if (array[i, j] == array[i + 1, j])
                    {
                        array[i + 1, j] += array[i, j];
                        points += array[i + 1, j];
                        array[i, j] = 0;
                    }
                }
            }
            return new ScoreState(array, points);
        }
        private ScoreState fusionLeft()
        {
            int points = 0;
            int[,] array = (int[,])this.array.Clone();
            
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 1; j < array.GetLength(0); j++)
                {
                    if (array[i,j] == array[i,j - 1])
                    {
                        array[i,j - 1] += array[i, j];
                        points += array[i, j - 1];
                        array[i, j] = 0;
                    }
}
}
            return new ScoreState(array, points);
        }
        private ScoreState fusionUp()
        {
            int points = 0;
            int[,] array = (int[,])this.array.Clone();
            for (int i = 1; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(0); j++)
                {
                    if (array[i, j] == array[i - 1, j])
                    {
                        array[i - 1, j] += array[i, j];
                        points += array[i - 1, j];
                        array[i, j] = 0;
                    }
                }
            }

            return new ScoreState(array, points);
        }
       

        public bool isMovedRight()
        {
            return !(equal(array, fusionRight().array) && equal(array, moveRight(array)));
        }
        public bool isMovedLeft()
        {
            return !(equal(array, fusionLeft().array) && equal(array, moveLeft(array)));
        }
        public bool isMovedUp()
        {
            return !(equal(array, fusionUp().array) && equal(array, moveUp(array)));
        }
        public bool isMovedDown()
        {

            return !(equal(array, fusionDown().array) && equal(array, moveDown(array)));
        }
        public bool equal(int[,] array1, int[,] array2)
        {
            bool q = true;
            for (int i = 0; i < array1.GetLength(0) && q; i++)
            {
                for (int j = 0; j < array1.GetLength(0) && q; j++)
                {
                    if (array1[i,j] != array2[i,j])
                    {
                        q = false;
                    }
                }
            }
            return q;
        }
        private int cnt0s()
        {
            int cnt = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(0); j++)
                {
                    if (array[i,j] == 0)
                    {
                        cnt++;
                    }
                }
            }

            return cnt;
        }
        private void reset(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(0); j++)
                {
                    array[i,j] = 0;
                }
        }
        public void Draw2048()
        {

            TRect piec = new TRect(0, 0, 8, 4, ConsoleColor.White);
            Console.ForegroundColor = ConsoleColor.White;
           
            for (int i = 0; i < array.GetLength(0); i++)
            {
                piec.SetY(4 * i);
                for (int j = 0; j < array.GetLength(0); j++)
                {
                    piec.SetX(8 * j);
                    if (array[i, j] != 0)
                    {
                        piec.SetFcolor((ConsoleColor)Math.Log(array[i, j], 2));
                        piec.Draw();
                        Console.SetCursorPosition(j * 8 + 1, i * 4 + 1);
                        
                        Console.WriteLine((""+array[i, j]).PadRight(5,' '));
                    }
                    else
                    {
                        piec.SetFcolor(ConsoleColor.White);
                        piec.Draw();
                        Console.SetCursorPosition(j * 8 + 1, i * 4 + 1);
                        Console.Write("     ");
                    }
                }
                
            }
            TRect scr = new TRect(array.GetLength(0) * 8 + 3, 1, 22, 3, ConsoleColor.Yellow);
            scr.Draw();
            Console.SetCursorPosition(array.GetLength(0) * 8 + 4, 2);
            Console.WriteLine("SCORE: {0} POINTS", Score);
        }
       
        public bool GameOver()
        {
            return !(isMovedLeft() || isMovedRight() || isMovedDown() || isMovedUp());

        }
        
    }
     class ScoreState
    {
        public int Score;
        public int[,] array;

        public ScoreState(int[,] array, int score)
        {
            this.array = array;
            Score = score;
        }
    }
}
