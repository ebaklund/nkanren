
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;

using static nk.RunModule;
using static nk.GoalsModule;
using static Sudoku.GoalsModule;
using static Sudoku.BoardModule;

namespace Sudoku.Tests;

public class SudokuTests
{
    [Fact]
    public void Test_1x1()
    {
        var board = new object?[]
        {
            null
        };

        string? res;
        
        RunAll(board, (q, x) => Conj(
            Equal(q, x),
            Once(x[0], board)
        )).AsBoards().AsStrings().TryTakeFirst(out res);

        res.Should().Be(
            "┏━┓\n" +
            "┃0┃\n" +
            "┗━┛ 1\n"
        );
    }
}
