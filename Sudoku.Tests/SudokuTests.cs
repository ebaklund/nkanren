
using FluentAssertions;

using static nk.RunModule;
using static nk.GoalsModule;
using static Sudoku.GoalsModule;
using static Sudoku.BoardModule;

namespace Sudoku.Tests;
using static ValidatorModule;

[Collection("RenderedSudoku")]
public class Given_Board_1x1
{
    [Fact]
    public void ItResolvesAll()
    {
        var boards = RunAll(4, (q, x) => {
            var board = new object[][]
            {
                Row( x[0] )
            };

            return Conj
            (
                Equal(q, board),
                Once(x[0], board)
            );
        }).AsBoards().TakeAll();

        boards.Count.Should().Be(1);
        ValidateBoards(boards);
        ResetRenderCount();

        ("\n" + boards[0].AsString()).Should().Be( "\n" +
            "┏━┓\n" +
            "┃0┃\n" +
            "┗━┛ 1\n"
        );
    }
}

[Collection("RenderedSudoku")]
public class Given_Board_2x2
{
    [Fact]
    public void ItResolvesAll()
    {
        var boards = RunAll(4, (q, x) => {
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
        }).AsBoards().TakeAll();
        
        boards.Count.Should().Be(2);
        ValidateBoards(boards);
        ResetRenderCount();
        
        ("\n" + boards[0].AsString()).Should().Be( "\n" +
            "┏━┯━┓\n" +
            "┃0│1┃\n" +
            "┠─┼─┨\n" +
            "┃1│0┃\n" +
            "┗━┷━┛ 1\n"
        );
        
        ("\n" + boards[1].AsString()).Should().Be( "\n" +
            "┏━┯━┓\n" +
            "┃1│0┃\n" +
            "┠─┼─┨\n" +
            "┃0│1┃\n" +
            "┗━┷━┛ 2\n"
        );    
    }
}
