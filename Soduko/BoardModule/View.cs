
using nk;
using System.Text;

namespace Sudoku;


public static partial class BoardModule
{
    // PRIVATE

    private static int _renderCount = 0;
    
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

    // PUBLIC

    public static void ResetRenderCount()
    {
        _renderCount = 0;
    }

    public static string AsString(this object[][] board)
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

        sb.Append("┗" + string.Concat(Enumerable.Repeat("━┷", dim - 1)) + $"━┛ {++_renderCount}\n");

        return sb.ToString();
    }

    public static IEnumerator<object[][]> AsBoards(this IEnumerator<object> stream)
    {
        while (stream.MoveNext())
        {
            if (stream.Current is object[][] matrix)
            {
                yield return matrix;
                continue;
            }

            if (stream.Current is not object?[] candidate)
            {
                throw new ApplicationException($">>>    This cannot be a Sudoku board! Unexpected type: \"{stream.Current.GetType().Name}\".    <<<");
            }

            int dim = (int)Math.Sqrt(candidate.Length);

            if (dim*dim != candidate.Length)
            {
                throw new ApplicationException($">>>    This cannot be a Sudoku board! Not squareable array length: {candidate.Length}.    <<<");
            }

            var board = new object?[dim][];
            var i = 0;

            for (var r = 0; r < dim; r++)
            {
                var row = new object?[dim];

                for (var c = 0; c < dim; c++)
                {
                    row[c] = candidate[i++];
                }

                board[r] = row;
            }

            yield return board;
        }
    }

    public static IEnumerator<string> AsStrings(this IEnumerator<object[][]> boards)
    {
        _renderCount = 0;

        while (boards.MoveNext())
        {
            yield return AsString(boards.Current);
        }
    }
    
    public static void ToConsole(this IEnumerator<string> strings)
    {
        while (strings.MoveNext())
        {
            Console.WriteLine(strings.Current, "\n");
        }
    }
}

