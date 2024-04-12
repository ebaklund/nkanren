
using FluentAssertions;
using static nk.RunnerModule;
using static Sudoku.BoardModule;
using static Sudoku.RunnerModule;
using Sudoku.Tests.Utils;
using nk;

namespace Sudoku.Tests;
using static ValidatorModule;


[Trait("Board", "Peers")]
public class Board_should_provide_cell_peers
{
    protected Key[] GetExpectedKeysWith(uint[] expectedIndexes)
    {
        var expectedKeys = new Key[expectedIndexes.Length];

        for (int i = 0; i < expectedIndexes.Length; ++i)
        {
            expectedKeys[i] = new Key(expectedIndexes[i]);
        }

        return expectedKeys;
    }

    protected Board GetBoardWithDim(uint dim)
    {
        var cells = new Key[dim*dim];

        for (Key k = new Key(0); k.Idx < cells.Length; k = new Key(k.Idx + 1))
        {
            cells[k.Idx] = k;
        }

        return Board.With(cells);
    }

    [Theory]
    [InlineData(1, 0, new uint[] { 0, 0, 0 })]

    [InlineData(2, 0, new uint[] { 0, 1, 0, 2, 0 })]
    [InlineData(2, 1, new uint[] { 0, 1, 1, 3, 1 })]
    [InlineData(2, 2, new uint[] { 2, 3, 0, 2, 2 })]
    [InlineData(2, 3, new uint[] { 2, 3, 1, 3, 3 })]

    [InlineData(3, 0, new uint[] { 0, 1, 2, 0, 3, 6, 0 })]
    [InlineData(3, 1, new uint[] { 0, 1, 2, 1, 4, 7, 1 })]
    [InlineData(3, 2, new uint[] { 0, 1, 2, 2, 5, 8, 2 })]
    [InlineData(3, 3, new uint[] { 3, 4, 5, 0, 3, 6, 3 })]
    [InlineData(3, 4, new uint[] { 3, 4, 5, 1, 4, 7, 4 })]
    [InlineData(3, 5, new uint[] { 3, 4, 5, 2, 5, 8, 5 })]
    [InlineData(3, 6, new uint[] { 6, 7, 8, 0, 3, 6, 6 })]
    [InlineData(3, 7, new uint[] { 6, 7, 8, 1, 4, 7, 7 })]
    [InlineData(3, 8, new uint[] { 6, 7, 8, 2, 5, 8, 8 })]
    public void Where(uint boardDim, uint cellIdx, uint[] expectedIndexes)
    {
        var board = GetBoardWithDim(boardDim);
        var expectedKeys = GetExpectedKeysWith(expectedIndexes);


        board.PeersOfCellAt(cellIdx).TakeAll().Should().BeEquivalentTo(expectedKeys);
    }
}
