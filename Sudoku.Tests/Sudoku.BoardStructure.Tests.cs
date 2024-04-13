

using FluentAssertions;
using static nk.RunnerModule;
using static Sudoku.BoardModule;
using static Sudoku.RunnerModule;
using Sudoku.Tests.Utils;
using nk;

namespace Sudoku.Tests;
using static ValidatorModule;


[Trait("Board", "Structure")]
public class Board_Structure
{
    public class Has : Board_Structure
    {
        // PUBLIC

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
        public void CellCount(int cellCount, uint expected)
        {
            object[] cells = Enumerable.Range(0, cellCount).Select(i => new Key((uint)i)).ToArray();
            var board = Board.With(cells);
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
        public void Dim(int cellCount, uint expected)
        {
            object[] cells = Enumerable.Range(0, cellCount).Select(i => new Key((uint)i)).ToArray();
            var board = Board.With(cells);
            board.BoardDim.Should().Be(expected);
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
        public void BoxDim(int cellCount, uint expected)
        {
            object[] cells = Enumerable.Range(0, cellCount).Select(i => new Key((uint)i)).ToArray();
            var board = Board.With(cells);
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
        public void BoxCnt(int cellCount, uint expected)
        {
            object[] cells = Enumerable.Range(0, cellCount).Select(i => new Key((uint)i)).ToArray();
            var board = Board.With(cells);
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
        public void DimBoxCnt(int cellCount, uint expected)
        {
            object[] cells = Enumerable.Range(0, cellCount).Select(i => new Key((uint)i)).ToArray();
            var board = Board.With(cells);
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
