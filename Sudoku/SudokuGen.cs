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

        board = new int[size, size];
        GenerateSolution();
        RemoveCellsForPuzzle(emptyCells);
        return ConvertBoardToList();
    }

    private static bool GenerateSolution()
    {
        // find empty cell
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

        // if no empty cell found, completed
        if (isEmpty)
        {
            return true;
        }

        // shuffeled list of possible numbers
        List<int> numbers = new List<int>();
        for (int num = 1; num <= size; num++)
        {
            numbers.Add(num);
        }
        ShuffleList(numbers);

        // try to put number from list to board
        foreach (int num in numbers)
        {
            if (IsSafe(row, col, num))
            {
                board[row, col] = num;

                if (GenerateSolution())
                {
                    return true;
                }

                // backtrack if number does not lead to solution
                board[row, col] = 0;
            }
        }

        // no solution found
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

    private static void RemoveCellsForPuzzle(int emptyCells)
    {
        // list of all board positions
        List<(int, int)> positions = new List<(int, int)>();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                positions.Add((i, j));
            }
        }

        ShuffleList(positions);

        // remove number of cells
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