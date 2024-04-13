using static Sudoku.BoardModule;

namespace Sudoku.Tests.Utils;

public static class ValidatorModule
{
    // PRIVATE

    public static object[][] Transform(object[][] board)
    {
        var dim = board.Length;
        var res = new object[dim][];

        for (int r = 0; r < dim; ++r)
        {
            res[r] = new object[dim];
        }

        for (int r = 0; r < dim; ++r)
        {
            for (int c = 0; c < dim; ++c)
            {
                res[r][c] = board[c][r];
            }
        }

        return res;
    }

    public static void ValidateDims(object[][] board)
    {
        var dim = board.Length;

        if (!board.ToList().TrueForAll(row => row.Length == dim))
        {
            throw new ApplicationException($"ValidateDim() Not a square board.");
        }
    }

    public static void ValidateRows(object[][] board)
    {
        foreach (var row in board)
        {
            ValidateArray(row);
        }
    }

    public static void AssertValidPeerGroups(IEnumerator<IEnumerator<object>> peerGroups, uint boardDim)
    {
        while(peerGroups.MoveNext())
        {
            var counts = new uint[boardDim];
            var peerCells = peerGroups.Current;

            while (peerCells.MoveNext())
            {
                if (peerCells.Current is int num)
                {
                    ++counts[num - 1];

                    if (counts[num - 1] > 1)
                    {
                        throw new ApplicationException($"ValidateRow() Count of number {num} is {counts[num]}.");
                    }
                }
            }
        }
    }

    public static void ValidateCols(object[][] board)
    {
        var board2 = Transform(board);

        foreach (var row in board2)
        {
            ValidateArray(row);
        }
    }

    public static void ValidateArray(object[] arr)
    {
        var dim = arr.Length;
        var counts = new int[dim];

        foreach (object o in arr)
        {
            if (o is int i)
            {
                ++counts[i];

                if (counts[i] > 1)
                {
                    throw new ApplicationException($"ValidateRow() Count of number {i} is {counts[i]}.");
                }
            }
        }
    }

    public static void ValidateBoard(object[][] board)
    {
        ValidateDims(board);
        ValidateRows(board);
        ValidateCols(board);
    }

    private static void AssertValid(Board board)
    {
        AssertValidPeerGroups(board.Rows(), board.BoardDim);
        AssertValidPeerGroups(board.Cols(), board.BoardDim);
        AssertValidPeerGroups(board.Boxs(), board.BoardDim);
    }

    // PUBLIC

    public static void ValidateBoards(List<object[][]> boards)
    {
        foreach (var board in boards)
        {
            ValidateBoard(board);
        }
    }

    public static IEnumerator<Board> AssertValid(this IEnumerator<Board> boards)
    {
        while (boards.MoveNext())
        {
            AssertValid(boards.Current);
            yield return boards.Current;
        }
    }
}

