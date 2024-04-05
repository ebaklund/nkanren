
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;

using static nk.RunModule;
using static nk.GoalsModule;
using static Sudoku.GoalsModule;
using static Sudoku.BoardModule;

namespace Sudoku.Tests;

[Collection("RenderedSudoku")]
public class Given_Board_1x1
{
    [Fact]
    public void ItResolvesAll()
    {
        var res = RunAll(4, (q, x) => {
            var board = new object[][]
            {
                Row( x[0] )
            };

            return Conj
            (
                Equal(q, board),
                Once(x[0], board)
            );
        }).AsBoards().AsStrings().TakeAll();
    }
}

[Collection("RenderedSudoku")]
public class Given_Board_2x2
{
    [Fact]
    public void ItResolvesAll()
    {
        var res = RunAll(4, (q, x) => {
            var board = new object[][]
            {
                Row( x[0], x[1] ),
                Row( x[2], x[3] )
            };

            return Conj
            (
                Equal(q, board),
                Once(x[0], board.Row(0), board.Col(0)),
                Once(x[1], board.Row(0), board.Col(1)),
                Once(x[2], board.Row(1), board.Col(0)),
                Once(x[3], board.Row(1), board.Col(1))
            );
        }).AsBoards().AsStrings().TakeAll();
        
        res.Count.Should().Be(2);
        
        res[0].Should().Be(
            "┏━┯━┓\n" +
            "┃0│1┃\n" +
            "┠─┼─┨\n" +
            "┃1│0┃\n" +
            "┗━┷━┛ 1\n"
        );
        
        res[1].Should().Be(
            "┏━┯━┓\n" +
            "┃1│0┃\n" +
            "┠─┼─┨\n" +
            "┃0│1┃\n" +
            "┗━┷━┛ 2\n"
        );    
    }
}
