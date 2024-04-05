﻿
using nk;

namespace Sudoku;


public static partial class BoardModule
{
    // PUBLIC

    public static object[] Row(params object[] row)
    {
        return row;
    }

    public static int Dim(this object[][] board)
    {
        return board.Length;
    }

    public static object[] Row(this object[][] board, int r)
    {
        var row = board[r];

        return row;
    }
/*
    public static object?[] Row(this object?[] board, int r)
    {
        var dim = (int) Math.Sqrt(board.Length);
        var row = new object[dim];

        for (int c = 0; c < dim; ++c)
        {
            row[c] = board[r*dim + c];
        }

        return row;
    }
*/
    public static object[] Col(this object[][] board, int c)
    {
        var col = new object[board.Dim()];

        for (var r = 0; r < board.Dim(); ++r)
        {
            col[r] = board.Row(r)[c];
        }

        return col;
    }
/*
    public static object?[] Col(this object?[] board, int c)
    {
        var dim = (int) Math.Sqrt(board.Length);
        var col = new object[dim];

        for (var r = 0; r < dim; ++r)
        {
            col[r] = board[r*dim + c];
        }

        return col;
    }
*/
    public static object[] Box4(this object[][] board, int b)
    {
        var box = new object[4];
        int r0 = (b / 2) * 2;
        int c0 = (b % 2) * 2;
        int i = 0;

        for (int r = r0; r < (r0 + 2); ++r)
        {
            for (int c = c0; c < (c0 + 2); ++c)
            {
                box[i++] = board[r][c];
            }
        }

        return box;
    }

    public static object[] Box9(this object[][] board, int b)
    {
        var box = new object[9];
        int r0 = (b / 3) * 3;
        int c0 = (b % 3) * 3;
        int i = 0;

        for (int r = r0; r < (r0 + 3); ++r)
        {
            for (int c = c0; c < (c0 + 3); ++c)
            {
                box[i++] = board[r][c];
            }
        }

        return box;
    }
    
    public static object[][] SudokuBoard(params object[] objects)
    {
        var dim = (int) Math.Sqrt(objects.Length);

        if (dim*dim != objects.Length)
        {
            throw new ApplicationException("Cannot build Sudoku board from structure not square.");
        }

        int i = 0;
        var board = new object[dim][];

        for(var r = 0; r < dim; ++r)
        {
            board[r] = new object[dim];

            for( var c = 0; c < dim; ++c)
            {
                board[r][c] = objects[i++];
            }
        }

        return board;
    }
}
