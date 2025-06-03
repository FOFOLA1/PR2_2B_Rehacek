using Sudoku;
using System;
using System.Collections.Generic;

public class SudokuGen
{
    private static int[,] board;
    private static int size;
    private static int boxRows;
    private static int boxCols;
    private static Random rn = new Random();


    public static List<List<int>> GenerateSudoku(Difficulty diff)
    {
        int emptyCells = 0;

        //if (informal)
        //{
        //    size = 6;
        //    boxRows = 2;
        //    boxCols = 3;
        //    if (emptyCells < 0) emptyCells = 12;
        //}
        //else
        //{
        //    size = 9;
        //    boxRows = 3;
        //    boxCols = 3;
        //    if (emptyCells < 0) emptyCells = 40;
        //}

        switch (diff)
        {
            case Difficulty.Hard:
                emptyCells = 50;
                size = 9;
                boxRows = 3;
                boxCols = 3;
                break;

            case Difficulty.Easy:
                emptyCells = 40;
                size = 9;
                boxRows = 3;
                boxCols = 3;
                break;

            case Difficulty.Informal:
                size = 6;
                boxRows = 2;
                boxCols = 3;
                //emptyCells = 1;
                emptyCells = 20;
                break;
        }

        // Initialize the board
        board = new int[size, size];

        // Generate a solved Sudoku board
        FillBoard();

        // Create the puzzle by removing cells
        CreatePuzzle(emptyCells);

        // Convert the 2D array to a List<List<int>> for return
        return ConvertBoardToList();
    }

    private static bool FillBoard()
    {
        // Find an empty cell
        int row = -1;
        int col = -1;
        bool isEmpty = true;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (board[i, j] == 0)
                {
                    row = i;
                    col = j;
                    isEmpty = false;
                    break;
                }
            }
            if (!isEmpty)
            {
                break;
            }
        }

        // If no empty cell is found, the board is filled
        if (isEmpty)
        {
            return true;
        }

        // Create a shuffled list of numbers to try
        List<int> numbers = new List<int>();
        for (int num = 1; num <= size; num++)
        {
            numbers.Add(num);
        }
        ShuffleList(numbers);

        // Try each number in the shuffled list
        foreach (int num in numbers)
        {
            if (IsSafe(row, col, num))
            {
                board[row, col] = num;

                if (FillBoard())
                {
                    return true;
                }

                // If placing the number doesn't lead to a solution, backtrack
                board[row, col] = 0;
            }
        }

        // No solution found with current configuration
        return false;
    }

    private static void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rn.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    private static bool IsSafe(int row, int col, int num)
    {
        for (int j = 0; j < size; j++)
        {
            if (board[row, j] == num)
            {
                return false;
            }
        }

        for (int i = 0; i < size; i++)
        {
            if (board[i, col] == num)
            {
                return false;
            }
        }

        int boxStartRow = row - row % boxRows;
        int boxStartCol = col - col % boxCols;

        for (int i = 0; i < boxRows; i++)
        {
            for (int j = 0; j < boxCols; j++)
            {
                if (board[boxStartRow + i, boxStartCol + j] == num)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static void CreatePuzzle(int emptyCells)
    {
        // Create a list of all cell positions
        List<(int, int)> positions = new List<(int, int)>();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                positions.Add((i, j));
            }
        }

        ShuffleList(positions);

        // Remove cells (set to 0) based on the difficulty
        for (int i = 0; i < Math.Min(emptyCells, positions.Count); i++)
        {
            var (row, col) = positions[i];
            board[row, col] = 0;
        }
    }

    private static List<List<int>> ConvertBoardToList()
    {
        List<List<int>> result = new List<List<int>>();

        for (int i = 0; i < size; i++)
        {
            List<int> row = new List<int>();
            for (int j = 0; j < size; j++)
            {
                row.Add(board[i, j]);
            }
            result.Add(row);
        }

        return result;
    }
}