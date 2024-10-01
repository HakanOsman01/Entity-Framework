using System;
using System.Linq;

namespace _05._Paths_in_Labyrinth
{
    internal class Program
    {
        static void Main(string[] args)
        {
           int rowsCount=int.Parse(Console.ReadLine());
           int colsCount=int.Parse(Console.ReadLine());
           char[,]labirinth= ReadMatrix(rowsCount, colsCount);
           bool[,]wasThere=new bool[rowsCount, colsCount];
            string currentPath = string.Empty;
            int currentRow = 0;
            int currentCol=0;
          FindAllPathsInLabirinth(currentRow,currentCol,labirinth, wasThere,currentPath);
            
        }

        private static void FindAllPathsInLabirinth(int currentRow, 
            int currentCol, char[,] labirinth, bool[,] wasThere, string currentPath)
        {
            if (labirinth[currentRow,currentCol]=='e')
            {
                Console.WriteLine(currentPath);
                return;
            }
            wasThere[currentRow,currentCol] = true;

            if (IsValidPath(labirinth, currentRow+1, currentCol, wasThere))
            {
                FindAllPathsInLabirinth(currentRow + 1, currentCol, 
                    labirinth, wasThere, currentPath + 'D');
            }
            if(IsValidPath(labirinth,currentRow-1, currentCol, wasThere))
            {
                FindAllPathsInLabirinth(currentRow - 1, currentCol,
                    labirinth, wasThere, currentPath + 'U');
            }
            if (IsValidPath(labirinth, currentRow , currentCol+1, wasThere))
            {
                FindAllPathsInLabirinth(currentRow, currentCol+1,
                    labirinth, wasThere, currentPath + 'R');
            }
            if (IsValidPath(labirinth, currentRow, currentCol - 1, wasThere))
            {
                FindAllPathsInLabirinth(currentRow, currentCol - 1,
                    labirinth, wasThere, currentPath + 'L');
            }
            wasThere[currentRow,currentCol] = false;


        }
        private static bool IsValidPath(char[,] labirinth,int currentRow,
            int currentCol, bool[,] wasThere)
        {
            if(currentRow<0 || currentRow>=labirinth.GetLength(0) || currentCol<0 || currentCol >= labirinth.GetLength(1))
            {
                return false;
            }
            if (labirinth[currentRow, currentCol] == '*')
            {
                return false;
            }
            if (wasThere[currentRow, currentCol]==true)
            {
                return false;
            }
            return true;
        }

        public static char[,] ReadMatrix(int rows,int cols)
        {
            char[,]matrix = new char[rows,cols];
            for (int row = 0; row < rows; row++)
            {
                string line=Console.ReadLine();
                for (int col = 0; col < line.Length; col++)
                {
                    matrix[row,col] = line[col];

                }

            }
            return matrix;

        }


    }
}