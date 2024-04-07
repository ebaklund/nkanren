
using FluentAssertions;
using static nk.RunnerModule;
using static Sudoku.BoardModule;
using static Sudoku.RunnerModule;
using Sudoku.Tests.Utils;

namespace Sudoku.Tests;
using static ValidatorModule;

[Trait("Solver", "")]
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

        ("\n" + res[0].AsString(resetRenderCount: true)).Should().Be( "\n" +
            "┏━┓\n" +
            "┃0┃\n" +
            "┗━┛ 1\n"
        );
    }
}

[Trait("Solver", "")]
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
        
        ("\n" + res[0].AsString(resetRenderCount: true)).Should().Be( "\n" +
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

[Trait("Solver", "")]
[Collection("RenderedSudoku")]
public class Given_Board_3x3
{
    [Fact]
    public void ItResolvesAll()
    {
        var res = RunSudoku(9, (x) => With(
            x[0], x[1], x[2],
            x[3], x[4], x[5],
            x[6], x[7], x[8]
        )).AssertValid().TakeAll();
 
        res.Count.Should().Be(12);

        ("\n" + res[0].AsString(resetRenderCount: true)).Should().Be( "\n" +
            "┏━┯━┯━┓\n" +
            "┃1│0│2┃\n" +
            "┠─┼─┼─┨\n" +
            "┃0│2│1┃\n" +
            "┠─┼─┼─┨\n" +
            "┃2│1│0┃\n" +
            "┗━┷━┷━┛ 1\n"
        );

        ("\n" + res[1].AsString()).Should().Be( "\n" +
            "┏━┯━┯━┓\n" +
            "┃0│2│1┃\n" +
            "┠─┼─┼─┨\n" +
            "┃1│0│2┃\n" +
            "┠─┼─┼─┨\n" +
            "┃2│1│0┃\n" +
            "┗━┷━┷━┛ 2\n"
        );
    }
}
