﻿
using nk;
using System.Text;

namespace Sudoku;


public static partial class BoardModule
{
    // PRIVATE

    private static int _renderCount = 0;
   
    private static string AsString(this object value)
    {
        return value switch
        {
            SudokuNumber n => $"{n.Value}",
            Key k => $"{(char)('a' + k.Idx)}",
            _ => throw new ArgumentException($"Unexpected type \"{value.GetType().Name}\" in Sudoku cell.")
        };
    }

    private static IEnumerable<string> AsStrings(this IEnumerable<object> values)
    {
        return values.Select(values => values.AsString());
    }

    private static void AppendRow(this StringBuilder sb, IEnumerable<object> row)
    {
        var strings = row.AsStrings();

        sb.Append("┃");

        foreach (var str in strings)
        {
            sb.Append(str + "│");
        }

        sb.Remove(sb.Length - 1, 1);
        sb.Append("┃\n");
    }

    // PUBLIC

    public static string AsString(this Board board, bool resetRenderCount = false)
    {
        if (resetRenderCount)
        {
            _renderCount = 0;
        }

        var dim = board.BoardDim;
        int rcount = ((int) dim) -1;

        var sb = new StringBuilder();
        sb.Append("┏" + string.Concat(Enumerable.Repeat("━┯", rcount)) + "━┓\n");

        for (uint i = 0; i < dim; i++)
        {
            sb.AppendRow(board.Row(i));

            if (i < dim - 1)
            {
                sb.Append("┠" + string.Concat(Enumerable.Repeat("─┼", rcount)) + "─┨\n");
            }
        }

        sb.Append("┗" + string.Concat(Enumerable.Repeat("━┷", rcount)) + $"━┛ {++_renderCount}\n");

        return sb.ToString();
    }
}

