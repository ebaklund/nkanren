﻿using nk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soduko.Utils;


public static class BoardExt
{
    // PRIVATE

    private static int _renderCount = 0;

    // PUBLIC

    public static object[] Row(params object[] row)
    {
        return row;
    }
    
    private static string AsString(object o)
    {
        return o switch
        {
            int i => $"{i}",
            Key k => $"{(char)('a' + k.Idx)}",
            _ => " ",
        };
    }

    private static string[] AsStrings(this object[] objs)
    {
        string[] strings = new string[objs.Length];

        for (int i = 0; i < objs.Length; ++i)
        {
            strings[i] = AsString(objs[i]);
        }

        return strings;
    }

    private static void AppendRow(this StringBuilder sb, object[] row)
    {
        var dim = row.Length;
        var strRow = AsStrings(row);

        sb.Append("┃");

        for (int i = 0; i < dim - 1; ++i)
        {
            sb.Append(strRow[i] + "│");
        }

        sb.Append(strRow[dim - 1] + "┃\n");
    }

    public static void Render(this object[][] board)
    {
        var dim = board.Dim();

        var sb = new StringBuilder();
        sb.Append("┏" + string.Concat(Enumerable.Repeat("━┯", dim - 1)) + "━┓\n");

        for (var i = 0; i < dim; i++)
        {
            sb.AppendRow(board.Row(i));

            if (i < dim - 1)
            {
                sb.Append("┠" + string.Concat(Enumerable.Repeat("─┼", dim - 1)) + "─┨\n");
            }
        }

        sb.Append("┗" + string.Concat(Enumerable.Repeat("━┷", dim - 1)) + $"━┛ {++_renderCount}\n\n");
        Console.Write(sb.ToString());
    }

    public static void Render(this IEnumerator<object> e)
    {
        while (e.MoveNext())
        {
            if (e.Current is not object[][] board)
            {
                throw new ApplicationException($">>>    This is not a board! Cannot render a {e.Current.GetType().Name}.    <<<");
            }

            board.Render();
        }
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

    public static object[] Col(this object[][] board, int c)
    {
        var col = new object[board.Dim()];

        for (var r = 0; r < board.Dim(); ++r)
        {
            col[r] = board.Row(r)[c];
        }

        return col;
    }

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
}

