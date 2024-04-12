
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

    [InlineData(4, 0, new uint[] { 0, 1, 2, 3, 0, 4, 8, 12, 0, 1, 4, 5 })]
    [InlineData(4, 1, new uint[] { 0, 1, 2, 3, 1, 5, 9, 13, 0, 1, 4, 5 })]
    [InlineData(4, 2, new uint[] { 0, 1, 2, 3, 2, 6, 10, 14, 2, 3, 6, 7 })]
    [InlineData(4, 3, new uint[] { 0, 1, 2, 3, 3, 7, 11, 15, 2, 3, 6, 7 })]
    [InlineData(4, 4, new uint[] { 4, 5, 6, 7, 0, 4, 8, 12, 0, 1, 4, 5 })]
    [InlineData(4, 5, new uint[] { 4, 5, 6, 7, 1, 5, 9, 13, 0, 1, 4, 5 })]
    [InlineData(4, 6, new uint[] { 4, 5, 6, 7, 2, 6, 10, 14, 2, 3, 6, 7 })]
    [InlineData(4, 7, new uint[] { 4, 5, 6, 7, 3, 7, 11, 15, 2, 3, 6, 7 })]

    [InlineData(4, 8, new uint[] { 8, 9, 10, 11, 0, 4, 8, 12, 8, 9, 12, 13 })]
    [InlineData(4, 9, new uint[] { 8, 9, 10, 11, 1, 5, 9, 13, 8, 9, 12, 13 })]
    [InlineData(4, 10, new uint[] { 8, 9, 10, 11, 2, 6, 10, 14, 10, 11, 14, 15 })]
    [InlineData(4, 11, new uint[] { 8, 9, 10, 11, 3, 7, 11, 15, 10, 11, 14, 15 })]
    [InlineData(4, 12, new uint[] { 12, 13, 14, 15, 0, 4, 8, 12, 8, 9, 12, 13 })]
    [InlineData(4, 13, new uint[] { 12, 13, 14, 15, 1, 5, 9, 13, 8, 9, 12, 13 })]
    [InlineData(4, 14, new uint[] { 12, 13, 14, 15, 2, 6, 10, 14, 10, 11, 14, 15 })]
    [InlineData(4, 15, new uint[] { 12, 13, 14, 15, 3, 7, 11, 15, 10, 11, 14, 15 })]

    public void Where(uint boardDim, uint cellIdx, uint[] expectedIndexes)
    {
        var board = GetBoardWithDim(boardDim);
        var expectedKeys = GetExpectedKeysWith(expectedIndexes);


        board.PeersOfCellAt(cellIdx).TakeAll().Should().BeEquivalentTo(expectedKeys);
    }
}
