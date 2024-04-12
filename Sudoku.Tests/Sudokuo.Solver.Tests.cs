
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

[Trait("Solver", "")]
[Collection("RenderedSudoku")]
public class Given_Board_9x9
{
    [Fact]
    public void ItResolvesAll()
    {
        var res = RunSudoku(81, (x) => With(
            x[80], x[79], x[78],   x[77], x[76], x[75],   x[74], x[73], x[72],
            x[71], x[70], x[69],   x[68], x[67], x[66],   x[15], x[65], x[64],
            x[63], x[61], x[60],   x[59], x[58], x[57],   x[56], x[55], x[54],

            x[53], x[52], x[51],   x[50], x[49], x[48],   x[47], x[46], x[45],
            x[44], x[43], x[42],   x[41], x[40], x[39],   x[38], x[37], x[36],
            x[35], x[34], x[33],   x[32], x[31], x[30],   x[29], x[28], x[27],

            x[26], x[25], x[24],   x[23], x[22], x[21],   x[20], x[19], x[18],
            x[17], x[16], x[15],   x[14], x[13], x[12],   x[11], x[10],  x[9],
             x[8],  x[7],  x[6],    x[5],  x[4],  x[3],    x[2],  x[1],  x[0]
        )).AssertValid().TakeMax(2);
 
        res.Count.Should().Be(81);

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
