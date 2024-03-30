
using static nk.Runners;
using static nk.Goals;
using static nk.Freshes;
using static nk.ListConstructor;
using static SodukoBoard;

using System;
using System.Text;
using nk;

Console.OutputEncoding = System.Text.Encoding.UTF8;

RunAll(1, (q, x) => {
    var board = Board
    (
        Row( 1 )
    );

    return Eqo(q, board);
}).Render();

RunAll(4, (q, x) => {
    var board = Board
    (
        Row( x[0], x[1] ),
        Row( x[2], x[3] )
    );

    return Eqo(q, board);
}).Render();

return 0;

// HELPERS

static class SodukoBoard
{
    // PRIVATE

    private static string AsString(this object o)
    {
        return o switch
        {
            int i => $"{i}",
              Key => " ",
                _ => " ",
        };
    }

    private static string[] AsStrings(this object[] objs)
    {
        string[] strings = new string[objs.Length];

        for(int i = 0; i < objs.Length; ++i)
        {
            strings[i] = objs[i].AsString();
        }

        return strings;
    }

    private static void AppendRow(this StringBuilder sb, object[] row)
    {
        var dim = row.Length;
        var strRow = row.AsStrings();

        sb.Append("┃");

        for (int i = 0; i < (dim - 1); ++i)
        {
            sb.Append(strRow[dim - 1] + "│");
        }
        
        sb.Append(strRow[dim - 1] + "┃\n");
    }

    // PUBLIC

    public static object[] Row(params object[] row)
    {
        return row;
    }

    public static object[] Board(params object[][] board)
    {
        return board;
    }

    public static void Render(this IEnumerator<object> e)
    {
        while (e.MoveNext())
        {
            var board = (object[][])e.Current;
            var dim = board.Length;

            var sb = new StringBuilder();
            sb.Append("┏" + string.Concat(Enumerable.Repeat("━┯", dim - 1)) + "━┓\n");

            for ( var i = 0; i < dim; i++ )
            {
                sb.AppendRow(board[i]);

                if (i < dim - 1)
                {
                    sb.Append("┠" + string.Concat(Enumerable.Repeat("─┼", dim - 1)) + "─┨\n");
                }
            }
    
            sb.Append("┗" + string.Concat(Enumerable.Repeat("━┷", dim - 1)) + "━┛\n");
            Console.Write(sb.ToString());
        }
    }
}
