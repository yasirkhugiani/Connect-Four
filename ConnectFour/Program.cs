using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class Program
    {
        /// <summary>
        /// This is the code for "connect four game", all functions are written in this class only.
        /// </summary>

        /// <summary>
        /// Global variables used in the program
        /// </summary>  
        static int rows = 0;
        static int columns = 0;
        static List<string> boardIndexes;
        static List<char> yellowAndRedCount = new List<char>();
        static List<char> diagonalCount = new List<char>();
        static string rw = "";
        static string col = "";
        static char[,] array;

        static void Main(string[] args)
        {
            CreateBoard();
            PrintBoard();
            Turns();
        }
        /// <summary>
        /// Function to instantiate the 2D array with rows and columns provided by the user.
        /// Also checks for the dimensions and only accepts 5 rows and 5 columns, condition not met, goes to Scenario Five (Invalid board dimensions).
        /// </summary>
        static void CreateBoard()
        {
            Console.WriteLine("                              WELCOME TO CONNECT FOUR\n");
            Console.WriteLine("                                     Game rules");
            Console.WriteLine("\n- Only 5 rows and 5 columns are allowed with 25 possible slots.");
            Console.WriteLine("- Choose between 0 to 24 with 0 being the first slot and 24 last.");
            Console.WriteLine("- Yellow wins if four y's are successfully placed in a row (Horizontal) or in a diagonal (left to right/right to left).");
            Console.WriteLine("- Red wins if four r's are successfully placed in a column (Vertical).");
            Console.WriteLine("- Draws if no one is successful in placing four consecutive y's or r's as above.");
            Console.WriteLine("\n\nPlease enter the board dimensions(number of rows, number of columns).");
            rows = Convert.ToInt16(Console.ReadLine());
            columns = Convert.ToInt16(Console.ReadLine());
            if (rows != 5 || columns != 5)
            {
                Console.WriteLine("Invalid board dimensions !, please enter again. \r");
                rows = Convert.ToInt16(Console.ReadLine());
                columns = Convert.ToInt16(Console.ReadLine());
            }
            array = new char[rows, columns];
            boardIndexes = new List<string>();
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    array[r, c] = 'O';
                    rw = r.ToString();
                    col = c.ToString();
                    boardIndexes.Add(rw + " " + col);
                }
            }
        }
        /// <summary>
        /// Prints board each time its called with changes shown.
        /// </summary>
        static void PrintBoard()
        {
            for (int r = 0; r < array.GetLength(0); r++)
            {
                Console.WriteLine("\r");
                for (int c = 0; c < array.GetLength(1); c++)
                {
                    Console.Write(array[r, c] + " ");
                }
            }
        }
        /// <summary>
        /// Function to create turns for each player and also checks for indeces of the board.
        /// If player enters slot position out of the possible slots, goes to Scenario Six (Invalid Move).
        /// If player tries to enter slot position already marked, goes to Scenario Six (Invalid Move).
        /// If no none of the player wins, goes to Scenario Four (Draw).
        /// </summary>
        static void Turns()
        {
            int turns = 0;
            int yellowsPosition = 0;
            int redsPosition = 0;
            while (turns < 8)
            {
                if (turns % 2 == 0)
                {
                    Console.WriteLine("\n\nYellows Turn:");
                    yellowsPosition = Convert.ToInt16(Console.ReadLine());
                    while (yellowsPosition < 0 || yellowsPosition > 24)
                    {
                        Console.WriteLine("Invalid Move!");
                        Console.WriteLine("\n\nYellows Turn:");
                        yellowsPosition = Convert.ToInt16(Console.ReadLine());
                    }
                    string pos = "";
                    pos = boardIndexes[yellowsPosition];
                    string[] Index = pos.Split(' ');
                    int rowInd = Convert.ToInt16(Index[0]);
                    int colInd = Convert.ToInt16(Index[1]);
                    if (array[rowInd, colInd] != 'r' || array[rowInd, colInd] != 'y')
                    {
                        array[rowInd, colInd] = 'y';
                    }
                    else
                    {
                        Console.WriteLine("Invalid Move!");
                        continue;
                    }
                    PrintBoard();
                    HorizontalCheck();
                    DiagonalCheck();
                }
                else
                {
                    Console.WriteLine("\n\nReds Turn:");
                    redsPosition = Convert.ToInt16(Console.ReadLine());
                    while (redsPosition < 0 || redsPosition > 24)
                    {
                        Console.WriteLine("Invalid Move!");
                        Console.WriteLine("\n\nReds Turn:");
                        redsPosition = Convert.ToInt16(Console.ReadLine());
                    }
                    string pos = "";
                    pos = boardIndexes[redsPosition];
                    string[] Index = pos.Split(' ');
                    int rowInd = Convert.ToInt16(Index[0]);
                    int colInd = Convert.ToInt16(Index[1]);
                    if (array[rowInd, colInd] != 'y' || array[rowInd, colInd] != 'r')
                    {
                        array[rowInd, colInd] = 'r';
                    }
                    else
                    {
                        Console.WriteLine("Invalid Move!");
                        continue;
                    }
                    PrintBoard();
                    VerticalCheck();
                }
                turns++;
            }
            Console.WriteLine("\nDraw !");
            Console.WriteLine("Press any button to close the application.");
            Console.ReadKey();
            Environment.Exit(0);
        }
        /// <summary>
        /// Fills the generic list from each row of the 2D array. Scenario One (Yellow wins Horizontal).
        /// </summary>
        static void HorizontalCheck()
        {
            for (var r = 0; r < array.GetLength(0); r++)
            {
                for (var c = 0; c < array.GetLength(1); c++)
                {
                    yellowAndRedCount.Add(array[r, c]);
                }
                HorizontalCheckHelper();
                yellowAndRedCount.Clear();
            }
        }
        /// <summary>
        /// Checks the list for possibility of 4 y's each time the function is called by HorizontalCheck function.
        /// If finds 4 y's in a row, goes to Scenario One (Yellow wins Horizontal). 
        /// </summary>
        static void HorizontalCheckHelper()
        {
            for (var i = 0; i < yellowAndRedCount.Count; i++)
            {
                if (yellowAndRedCount[i] == 'y')
                {
                    switch (i)
                    {
                        case 0:
                            if (yellowAndRedCount[i + 1] == 'y' && yellowAndRedCount[i + 2] == 'y' && yellowAndRedCount[i + 3] == 'y')
                            {
                                YellowWins();
                            }
                            break;
                        case 1:
                            if (yellowAndRedCount[i - 1] == 'y' && yellowAndRedCount[i + 1] == 'y' && yellowAndRedCount[i + 2] == 'y')
                            {
                                YellowWins();
                            }
                            break;
                        case 2:
                            if (yellowAndRedCount[i - 2] == 'y' && yellowAndRedCount[i - 1] == 'y' && yellowAndRedCount[i + 1] == 'y')
                            {
                                YellowWins();
                            }
                            break;
                        case 3:
                            if (yellowAndRedCount[i - 3] == 'y' && yellowAndRedCount[i - 2] == 'y' && yellowAndRedCount[i - 1] == 'y')
                            {
                                YellowWins();
                            }
                            break;
                        case 4:
                            if (yellowAndRedCount[i - 3] == 'y' && yellowAndRedCount[i - 2] == 'y' && yellowAndRedCount[i - 1] == 'y')
                            {
                                YellowWins();
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// Fills the generic list from each column of the 2D array. Scenario Two (Red wins Vertical)
        /// </summary>
        static void VerticalCheck()
        {
            for (var c = 0; c < array.GetLength(1); c++)
            {
                for (var r = 0; r < array.GetLength(0); r++)
                {
                    yellowAndRedCount.Add(array[r, c]);
                }
                VerticalHelper();
                yellowAndRedCount.Clear();
            }
        }
        /// <summary>
        /// Checks the list for possibility of 4 r's each time the function is called by VerticalCheck function.
        /// If finds 4 r's in the column, goes to Scenario Two (Red wins Vertical). 
        /// </summary>
        static void VerticalHelper()
        {
            for (var i = 0; i < yellowAndRedCount.Count; i++)
            {
                if (yellowAndRedCount[i] == 'r')
                {
                    switch (i)
                    {
                        case 0:
                            if (yellowAndRedCount[i + 1] == 'r' && yellowAndRedCount[i + 2] == 'r' && yellowAndRedCount[i + 3] == 'r')
                            {
                                RedWins();
                            }
                            break;
                        case 1:
                            if (yellowAndRedCount[i - 1] == 'r' && yellowAndRedCount[i + 1] == 'r' && yellowAndRedCount[i + 2] == 'r')
                            {
                                RedWins();
                            }
                            break;
                        case 2:
                            if (yellowAndRedCount[i - 2] == 'r' && yellowAndRedCount[i - 1] == 'r' && yellowAndRedCount[i + 1] == 'r')
                            {
                                RedWins();
                            }
                            break;
                        case 3:
                            if (yellowAndRedCount[i - 3] == 'r' && yellowAndRedCount[i - 2] == 'r' && yellowAndRedCount[i - 1] == 'r')
                            {
                                RedWins();
                            }
                            break;
                        case 4:
                            if (yellowAndRedCount[i - 3] == 'r' && yellowAndRedCount[i - 2] == 'r' && yellowAndRedCount[i - 1] == 'r')
                            {
                                RedWins();
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// Fills the list from all elements of the 2D array (the board) for diagonal checking Scenario Three (Yellow winds diagonal).
        /// </summary>
        static void DiagonalCheck()
        {
            for (var r = 0; r < array.GetLength(0); r++)
            {
                for (var c = 0; c < array.GetLength(1); c++)
                {
                    diagonalCount.Add(array[r, c]);
                }
            }
            LeftToRightDiagonalHelper();
            RightToLeftDiagonalHelper();
            diagonalCount.Clear();
        }
        /// <summary>
        /// Checks the list for possibility of 4 y's on diagonal indeces left to right each time the function is called by DiagonalCheck function.
        /// If finds 4 y's in the diagonal, goes to Scenario Three (Yellow wins diagonal). 
        /// </summary>
        static void LeftToRightDiagonalHelper()
        {
            for (var i = 0; i < diagonalCount.Count; i++)
            {
                if (diagonalCount[i] == 'y')
                {
                    switch (i)
                    {
                        case 0:
                            if (diagonalCount[i + 6] == 'y' && diagonalCount[i + 12] == 'y' && diagonalCount[i + 18] == 'y')
                            {
                                YellowWins();
                            }
                            break;
                        case 6:
                            if (diagonalCount[i - 6] == 'y' && diagonalCount[i + 6] == 'y' && diagonalCount[i + 12] == 'y')
                            {
                                YellowWins();
                            }
                            break;
                        case 12:
                            if (diagonalCount[i - 12] == 'y' && diagonalCount[i - 6] == 'y' && diagonalCount[i + 6] == 'y')
                            {
                                YellowWins();
                            }
                            break;
                        case 18:
                            if (diagonalCount[i - 18] == 'y' && diagonalCount[i - 12] == 'y' && diagonalCount[i - 6] == 'y')
                            {
                                YellowWins();
                            }
                            break;
                        case 24:
                            if (diagonalCount[i - 18] == 'y' && diagonalCount[i - 12] == 'y' && diagonalCount[i - 6] == 'y')
                            {
                                YellowWins();
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// Checks the list for possibility of 4 y's on diagonal indeces right to left each time the function is called by DiagonalCheck function.
        /// If finds 4 y's in the diagonal, goes to Scenario Three (Yellow wins diagonal). 
        /// </summary>
        static void RightToLeftDiagonalHelper()
        {
            for (var i = 0; i < diagonalCount.Count; i++)
            {
                if (diagonalCount[i] == 'y')
                {
                    switch (i)
                    {
                        case 4:
                            if (diagonalCount[i + 4] == 'y' && diagonalCount[i + 8] == 'y' && diagonalCount[i + 12] == 'y')
                            {
                                YellowWins();
                            }
                            break;
                        case 8:
                            if (diagonalCount[i - 4] == 'y' && diagonalCount[i + 4] == 'y' && diagonalCount[i + 8] == 'y')
                            {
                                YellowWins();
                            }
                            break;
                        case 12:
                            if (diagonalCount[i - 8] == 'y' && diagonalCount[i - 4] == 'y' && diagonalCount[i + 4] == 'y')
                            {
                                YellowWins();
                            }
                            break;
                        case 16:
                            if (diagonalCount[i - 12] == 'y' && diagonalCount[i - 8] == 'y' && diagonalCount[i - 4] == 'y')
                            {
                                YellowWins();
                            }
                            break;
                        case 20:
                            if (diagonalCount[i - 4] == 'y' && diagonalCount[i - 8] == 'y' && diagonalCount[i - 12] == 'y')
                            {
                                YellowWins();
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// When Red wins.
        /// </summary>
        static void RedWins()
        {
            Console.WriteLine("\nRed Wins !");
            Console.WriteLine("Press any button to close the application.");
            Console.ReadKey();
            Environment.Exit(0);
        }
        /// <summary>
        /// When Yellow wins.
        /// </summary>
        static void YellowWins()
        {
            Console.WriteLine("\nYellow Wins !");
            Console.WriteLine("Press any button to close the application.");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
