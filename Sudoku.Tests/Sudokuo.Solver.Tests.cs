
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
            "┃1┃\n" +
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
            "┃1│2┃\n" +
            "┠─┼─┨\n" +
            "┃2│1┃\n" +
            "┗━┷━┛ 1\n"
        );
        
        ("\n" + res[1].AsString()).Should().Be( "\n" +
            "┏━┯━┓\n" +
            "┃2│1┃\n" +
            "┠─┼─┨\n" +
            "┃1│2┃\n" +
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
            "┃2│1│3┃\n" +
            "┠─┼─┼─┨\n" +
            "┃1│3│2┃\n" +
            "┠─┼─┼─┨\n" +
            "┃3│2│1┃\n" +
            "┗━┷━┷━┛ 1\n"
        );

        ("\n" + res[1].AsString()).Should().Be( "\n" +
            "┏━┯━┯━┓\n" +
            "┃1│3│2┃\n" +
            "┠─┼─┼─┨\n" +
            "┃2│1│3┃\n" +
            "┠─┼─┼─┨\n" +
            "┃3│2│1┃\n" +
            "┗━┷━┷━┛ 2\n"
        );

        ("\n" + res[2].AsString()).Should().Be( "\n" +
            "┏━┯━┯━┓\n" +
            "┃1│2│3┃\n" +
            "┠─┼─┼─┨\n" +
            "┃3│1│2┃\n" +
            "┠─┼─┼─┨\n" +
            "┃2│3│1┃\n" +
            "┗━┷━┷━┛ 3\n"
        );
    }
}

[Trait("Solver", "")]
[Collection("RenderedSudoku")]
public class Given_Board_4x4
{
    [Fact]
    public void ItResolves_One()
    {
        var res = RunSudoku(16, (x) => With(
             x[0],  x[1],  x[2],  x[3],
             x[4],  x[5],  x[6],  x[7],
             x[8],  x[9], x[10], x[11],
            x[12], x[13], x[14], x[15]
        )).AssertValid().TakeMax(1);

        ("\n" + res[0].AsString(resetRenderCount: true)).Should().Be( "\n" +
            "┏━┯━┯━┯━┓\n" +
            "┃1│2│3│4┃\n" +
            "┠─┼─┼─┼─┨\n" +
            "┃3│4│1│2┃\n" +
            "┠─┼─┼─┼─┨\n" +
            "┃2│1│4│3┃\n" +
            "┠─┼─┼─┼─┨\n" +
            "┃4│3│2│1┃\n" +
            "┗━┷━┷━┷━┛ 1\n"
        );
    }
}

[Trait("Solver", "")]
[Collection("RenderedSudoku")]
public class Given_Board_5x5
{
    [Fact]
    public void ItResolves_One()
    {
        var res = RunSudoku(25, (x) => With(
             x[0],  x[1],  x[2],  x[3],  x[4],
             x[5],  x[6],  x[7],  x[8],  x[9],
            x[10], x[11], x[12], x[13], x[14],
            x[15], x[16], x[17], x[18], x[19],
            x[20], x[21], x[22], x[23], x[24]
        )).AssertValid().TakeMax(1);

        ("\n" + res[0].AsString(resetRenderCount: true)).Should().Be( "\n" +
            "┏━┯━┯━┯━┯━┓\n" +
            "┃4│2│1│3│5┃\n" +
            "┠─┼─┼─┼─┼─┨\n" +
            "┃1│3│2│5│4┃\n" +
            "┠─┼─┼─┼─┼─┨\n" +
            "┃2│1│5│4│3┃\n" +
            "┠─┼─┼─┼─┼─┨\n" +
            "┃3│5│4│1│2┃\n" +
            "┠─┼─┼─┼─┼─┨\n" +
            "┃5│4│3│2│1┃\n" +
            "┗━┷━┷━┷━┷━┛ 1\n"
        );
    }
}

[Trait("Solver", "")]
[Collection("RenderedSudoku")]
public class Given_Board_6x6
{
    [Fact]
    public void ItResolves_One()
    {
        var res = RunSudoku(36, (x) => With(
             x[0],  x[1],  x[2],  x[3],  x[4], x[5],
             x[6],  x[7],  x[8],  x[9], x[10], x[11],
            x[12], x[13], x[14], x[15], x[16], x[17],
            x[18], x[19], x[20], x[21], x[22], x[23], 
            x[24], x[25], x[26], x[27], x[28], x[29],
            x[30], x[31], x[32], x[33], x[34], x[35]            
        )).AssertValid().TakeMax(1);

        ("\n" + res[0].AsString(resetRenderCount: true)).Should().Be( "\n" +
            "┏━┯━┯━┯━┯━┯━┓\n" +
            "┃3│4│1│2│5│6┃\n" +
            "┠─┼─┼─┼─┼─┼─┨\n" +
            "┃1│2│5│6│3│4┃\n" +
            "┠─┼─┼─┼─┼─┼─┨\n" +
            "┃4│3│2│1│6│5┃\n" +
            "┠─┼─┼─┼─┼─┼─┨\n" +
            "┃5│6│3│4│1│2┃\n" +
            "┠─┼─┼─┼─┼─┼─┨\n" +
            "┃2│1│6│5│4│3┃\n" +
            "┠─┼─┼─┼─┼─┼─┨\n" +
            "┃6│5│4│3│2│1┃\n" +
            "┗━┷━┷━┷━┷━┷━┛ 1\n"
        );
    }
}

[Trait("Solver", "")]
[Collection("RenderedSudoku")]
public class Given_Board_7x7
{
    [Fact]
    public void ItResolves_One()
    {
        var res = RunSudoku(49, (x) => With(
             x[0],  x[1],  x[2],  x[3],  x[4],  x[5],  x[6],  
             x[7],  x[8],  x[9], x[10], x[11], x[12], x[13],
            x[14], x[15], x[16], x[17], x[18], x[19], x[20],
            x[21], x[22], x[23], x[24], x[25], x[26], x[27],
            x[28], x[29], x[30], x[31], x[32], x[33], x[34],
            x[35], x[36], x[37], x[38], x[39], x[40], x[41],           
            x[42], x[43], x[44], x[45], x[46], x[47], x[48]          
        )).AssertValid().TakeMax(1);

        ("\n" + res[0].AsString(resetRenderCount: true)).Should().Be( "\n" +
            "┏━┯━┯━┯━┯━┯━┯━┓\n" +
            "┃2│3│4│1│5│6│7┃\n" +
            "┠─┼─┼─┼─┼─┼─┼─┨\n" +
            "┃1│4│3│5│2│7│6┃\n" +
            "┠─┼─┼─┼─┼─┼─┼─┨\n" +
            "┃4│1│2│6│7│3│5┃\n" +
            "┠─┼─┼─┼─┼─┼─┼─┨\n" +
            "┃3│2│1│7│6│5│4┃\n" +
            "┠─┼─┼─┼─┼─┼─┼─┨\n" +
            "┃6│5│7│2│1│4│3┃\n" +
            "┠─┼─┼─┼─┼─┼─┼─┨\n" +
            "┃5│7│6│3│4│1│2┃\n" +
            "┠─┼─┼─┼─┼─┼─┼─┨\n" +
            "┃7│6│5│4│3│2│1┃\n" +
            "┗━┷━┷━┷━┷━┷━┷━┛ 1\n"
        );
    }
}

[Trait("Solver", "")]
[Collection("RenderedSudoku")]
public class Given_Board_8x8
{
    [Fact]
    public void ItResolves_One()
    {
        var res = RunSudoku(64, (x) => With(
              x[0],  x[1],  x[2],  x[3], x[4],  x[5],   x[6],  x[7], 
              x[8],  x[9], x[10], x[11], x[12], x[13], x[14], x[15],
             x[16], x[17], x[18], x[19], x[20], x[21], x[22], x[23],
             x[24], x[25], x[26], x[27], x[28], x[29], x[30], x[31],
             x[32], x[33], x[34], x[35], x[36], x[37], x[38], x[39],
             x[40], x[41], x[42], x[43], x[44], x[45], x[46], x[47],         
             x[48], x[49], x[50], x[51], x[52], x[53], x[54], x[55],          
             x[56], x[57], x[58], x[59], x[60], x[61], x[62], x[63]
        )).AssertValid().TakeMax(1);

        ("\n" + res[0].AsString(resetRenderCount: true)).Should().Be( "\n" +
            "┏━┯━┯━┯━┯━┯━┯━┯━┓\n" +
            "┃1│2│3│4│5│6│7│8┃\n" +
            "┠─┼─┼─┼─┼─┼─┼─┼─┨\n" +
            "┃3│4│1│2│7│8│5│6┃\n" +
            "┠─┼─┼─┼─┼─┼─┼─┼─┨\n" +
            "┃2│1│4│3│6│5│8│7┃\n" +
            "┠─┼─┼─┼─┼─┼─┼─┼─┨\n" +
            "┃4│3│2│1│8│7│6│5┃\n" +
            "┠─┼─┼─┼─┼─┼─┼─┼─┨\n" +
            "┃5│6│7│8│1│2│3│4┃\n" +
            "┠─┼─┼─┼─┼─┼─┼─┼─┨\n" +
            "┃7│8│5│6│3│4│1│2┃\n" +
            "┠─┼─┼─┼─┼─┼─┼─┼─┨\n" +
            "┃6│5│8│7│2│1│4│3┃\n" +
            "┠─┼─┼─┼─┼─┼─┼─┼─┨\n" +
            "┃8│7│6│5│4│3│2│1┃\n" +
            "┗━┷━┷━┷━┷━┷━┷━┷━┛ 1\n"
        );
    }
}

[Trait("Solver", "")]
[Collection("RenderedSudoku")]
public class Given_Board_9x9
{
    [Fact]
    public void ItResolves_One()
    {
        var res = RunSudoku(81, (x) => With(
#if true
              x[0],  x[1],  x[2],  x[3],  x[4],  x[5],  x[6],  x[7],  x[8],
              x[9], x[10], x[11], x[12], x[13], x[14], x[15], x[16], x[17],
             x[18], x[19], x[20], x[21], x[22], x[23], x[24], x[25], x[26],
             x[27], x[28], x[29], x[30], x[31], x[32], x[33], x[34], x[35],
             x[36], x[37], x[38], x[39], x[40], x[41], x[42], x[43], x[44],
             x[45], x[46], x[47], x[48], x[49], x[50], x[51], x[52], x[53],       
             x[54], x[55], x[56], x[57], x[58], x[59], x[60], x[61], x[62],        
             x[63], x[64], x[65], x[66], x[67], x[68], x[69], x[70], x[71],
             x[72], x[73], x[74], x[75], x[76], x[77], x[78], x[79], x[80]
#else
            x[80], x[79], x[78],   x[77], x[76], x[75],   x[74], x[73], x[72],
            x[71], x[70], x[69],   x[68], x[67], x[66],   x[65], x[64], x[63],
            x[62], x[61], x[60],   x[59], x[58], x[57],   x[56], x[55], x[54],

            x[53], x[52], x[51],   x[50], x[49], x[48],   x[47], x[46], x[45],
            x[44], x[43], x[42],   x[41], x[40], x[39],   x[38], x[37], x[36],
            x[35], x[34], x[33],   x[32], x[31], x[30],   x[29], x[28], x[27],

            x[26], x[25], x[24],   x[23], x[22], x[21],   x[20], x[19], x[18],
            x[17], x[16], x[15],   x[14], x[13], x[12],   x[11], x[10],  x[9],
             x[8],  x[7],  x[6],    x[5],  x[4],  x[3],    x[2],  x[1],  x[0]
#endif
        )).AssertValid().TakeMax(1);

        ("\n" + res[0].AsString(resetRenderCount: true)).Should().Be( "\n" +
            "┏━┯━┯━┯━┯━┯━┯━┯━┯━┓\n" +
            "┃2│4│6│1│3│5│8│7│9┃\n" +
            "┠─┼─┼─┼─┼─┼─┼─┼─┼─┨\n" +
            "┃1│3│5│8│7│9│2│4│6┃\n" +
            "┠─┼─┼─┼─┼─┼─┼─┼─┼─┨\n" +
            "┃8│7│9│2│4│6│1│3│5┃\n" +
            "┠─┼─┼─┼─┼─┼─┼─┼─┼─┨\n" +
            "┃5│6│3│4│1│2│7│9│8┃\n" +
            "┠─┼─┼─┼─┼─┼─┼─┼─┼─┨\n" +
            "┃4│1│2│7│9│8│5│6│3┃\n" +
            "┠─┼─┼─┼─┼─┼─┼─┼─┼─┨\n" +
            "┃7│9│8│5│6│3│4│1│2┃\n" +
            "┠─┼─┼─┼─┼─┼─┼─┼─┼─┨\n" +
            "┃6│5│4│3│2│1│9│8│7┃\n" +
            "┠─┼─┼─┼─┼─┼─┼─┼─┼─┨\n" +
            "┃3│2│1│9│8│7│6│5│4┃\n" +
            "┠─┼─┼─┼─┼─┼─┼─┼─┼─┨\n" +
            "┃9│8│7│6│5│4│3│2│1┃\n" +
            "┗━┷━┷━┷━┷━┷━┷━┷━┷━┛ 1\n"
        );
    }
}
