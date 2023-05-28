using System;

public class Board
{
    private char[,] grid;
    private char currentPlayer;

    public char CurrentPlayer
    {
        get { return currentPlayer; }
    }

    public Board()
    {
        grid = new char[3, 3];
        currentPlayer = 'X';
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                grid[row, col] = '-';
            }
        }
    }

    public void PrintBoard()
    {
        Console.WriteLine("-------------");
        for (int row = 0; row < 3; row++)
        {
            Console.Write("| ");
            for (int col = 0; col < 3; col++)
            {
                Console.Write(grid[row, col] + " | ");
            }
            Console.WriteLine();
            Console.WriteLine("-------------");
        }
    }

    public bool PlaceMarker(int position)
    {
        if (position < 1 || position > 9)
        {
            return false;
        }

        int row = (position - 1) / 3;
        int col = (position - 1) % 3;

        if (grid[row, col] != '-')
        {
            return false;
        }

        grid[row, col] = currentPlayer;
        return true;
    }

    public bool IsGameOver()
    {
        return IsWinningMove('X') || IsWinningMove('O') || IsBoardFull() && !IsWinningMove('X') && !IsWinningMove('O');
    }

    public bool IsWinningMove(char marker)
    {
        // Check rows
        for (int row = 0; row < 3; row++)
        {
            if (grid[row, 0] == marker && grid[row, 1] == marker && grid[row, 2] == marker)
            {
                return true;
            }
        }

        // Check columns
        for (int col = 0; col < 3; col++)
        {
            if (grid[0, col] == marker && grid[1, col] == marker && grid[2, col] == marker)
            {
                return true;
            }
        }

        // Check diagonals
        if ((grid[0, 0] == marker && grid[1, 1] == marker && grid[2, 2] == marker) ||
            (grid[0, 2] == marker && grid[1, 1] == marker && grid[2, 0] == marker))
        {
            return true;
        }

        return false;
    }

    public bool IsBoardFull()
    {
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (grid[row, col] == '-')
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void SwitchPlayer()
    {
        currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Board board = new Board();
        bool gameover = false;
        bool draw = false;

        while (!gameover)
        {
            board.PrintBoard();
            Console.WriteLine("Player {0}'s turn. Enter position (1-9):", board.CurrentPlayer);
            int position = int.Parse(Console.ReadLine());

            if (board.PlaceMarker(position))
            {
                if (board.IsGameOver())
                {
                    gameover = true;
                    board.PrintBoard();
                    if (board.IsWinningMove('X'))
                    {
                        Console.WriteLine("Player X wins!");
                    }
                    else if (board.IsWinningMove('O'))
                    {
                        Console.WriteLine("Player O wins!");
                    }
                    else
                    {
                        draw = true;
                    }
                }
                else
                {
                    board.SwitchPlayer();
                }
            }
            else
            {
                Console.WriteLine("Invalid move. Try again.");
            }
        }

        if (draw)
        {
            Console.WriteLine("It's a draw!");
        }
        Console.ReadLine();
    }
}
