
using FluentAssertions;
using static nk.RunnerModule;
using static Sudoku.BoardModule;
using static Sudoku.RunnerModule;
using Sudoku.Tests.Utils;

namespace Sudoku.Tests;
using static ValidatorModule;

[Collection("RenderedSudoku")]
public class Given_Board_1x1
{
    [Fact]
    public void ItResolvesAll()
    {
        var res = RunSudoku(1, (x) => With(
            x[0]
        )).AssertValid().TakeAll();
        
        res.Count.Should().Be(1);

        ("\n" + res[0].AsString(true)).Should().Be( "\n" +
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
        var res = RunSudoku(4, (x) => With(
            x[0], x[1],
            x[2], x[3]
        )).AssertValid().TakeAll();
        
        res.Count.Should().Be(2);
        
        ("\n" + res[0].AsString(true)).Should().Be( "\n" +
            "┏━┯━┓\n" +
            "┃0│1┃\n" +
            "┠─┼─┨\n" +
            "┃1│0┃\n" +
            "┗━┷━┛ 1\n"
        );
        
        ("\n" + res[1].AsString()).Should().Be( "\n" +
            "┏━┯━┓\n" +
            "┃1│0┃\n" +
            "┠─┼─┨\n" +
            "┃0│1┃\n" +
            "┗━┷━┛ 2\n"
        );    
    }
}
