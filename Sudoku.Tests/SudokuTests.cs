
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
        string? res;
        
        RunAll(1, (q, x) => {
            var board = new object[][]
            {
                Row( x[0] )
            };

            return Conj(Equal(q, board));
        }).AsBoards().AsStrings().TryTakeFirst(out res);

        res.Should().Be(
            "┏━┓\n" +
            "┃b┃\n" +
            "┗━┛ 1\n"
        );
    }
}
