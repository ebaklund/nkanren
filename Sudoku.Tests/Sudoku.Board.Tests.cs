

using FluentAssertions;
using static nk.RunnerModule;
using static Sudoku.BoardModule;
using static Sudoku.RunnerModule;
using Sudoku.Tests.Utils;

namespace Sudoku.Tests;
using static ValidatorModule;


public class Board_given_cells 
{
    [Trait("Board", "Structure")]
    public class Has : Board_given_cells
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(4, 4)]
        [InlineData(9, 9)]
        [InlineData(16, 16)]
        [InlineData(25, 25)]
        [InlineData(36, 36)]
        [InlineData(49, 49)]
        [InlineData(64, 64)]
        [InlineData(81, 81)]
        public void CellCount(uint cellCount, uint expected)
        {
            var board = Board.With(new object[cellCount]);
            board.CellCount.Should().Be(expected);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(4, 2)]
        [InlineData(9, 3)]
        [InlineData(16, 4)]
        [InlineData(25, 5)]
        [InlineData(36, 6)]
        [InlineData(49, 7)]
        [InlineData(64, 8)]
        [InlineData(81, 9)]
        public void Dim(uint cellCount, uint expected)
        {
            var board = Board.With(new object[cellCount]);
            board.Dim.Should().Be(expected);
        }
        
        [Theory]
        [InlineData(1, 1)]
        [InlineData(4, 1)]
        [InlineData(9, 1)]
        [InlineData(16, 2)]
        [InlineData(25, 1)]
        [InlineData(36, 2)]
        [InlineData(49, 1)]
        [InlineData(64, 2)]
        [InlineData(81, 3)]
        public void BoxDim(uint cellCount, uint expected)
        {
            var board = Board.With(new object[cellCount]);
            board.BoxDim.Should().Be(expected);
        }
        
        [Theory]
        [InlineData(1, 1)]
        [InlineData(4, 4)]
        [InlineData(9, 9)]
        [InlineData(16, 4)]
        [InlineData(25, 25)]
        [InlineData(36, 9)]
        [InlineData(49, 49)]
        [InlineData(64, 16)]
        [InlineData(81, 9)]
        public void BoxCnt(uint cellCount, uint expected)
        {
            var board = Board.With(new object[cellCount]);
            board.BoxCnt.Should().Be(expected);
        }
        
        [Theory]
        [InlineData(1, 1)]
        [InlineData(4, 2)]
        [InlineData(9, 3)]
        [InlineData(16, 2)]
        [InlineData(25, 5)]
        [InlineData(36, 3)]
        [InlineData(49, 7)]
        [InlineData(64, 4)]
        [InlineData(81, 3)]
        public void DimBoxCnt(uint cellCount, uint expected)
        {
            var board = Board.With(new object[cellCount]);
            board.DimBoxCnt.Should().Be(expected);
        }
    }
}


[Trait("Board", "Structure")]
[Collection("RenderedSudoku")]
public class With_2_cells
{
    [Fact]
    public void ThrowsException()
    {
        Action act = () => Board.With(new object[2]);
        act.Should().Throw<ArgumentException>().WithMessage("Cell array does not make a square.");
    }
}

[Trait("Board", "Structure")]
[Collection("RenderedSudoku")]
public class With_3_cells
{
    [Fact]
    public void ThrowsException()
    {
        Action act = () => Board.With(new object[3]);
        act.Should().Throw<ArgumentException>().WithMessage("Cell array does not make a square.");
    }
}
