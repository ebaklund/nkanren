using nk;

namespace Sudoku;
using static Sudoku.RunnerModule;

public static partial class BoardModule
{
    // PRIVATE

    private static void AssertValidContent(object[] cells)
    {
        var dim = (int) Math.Sqrt(cells.Length);

        if (dim*dim != cells.Length)
        {
            throw new ArgumentException("Cell array does not make a square.");
        }

        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i] is Key)
            {
                continue;
            }

            if (cells[i] is SudokuNumber)
            {
                continue;
            }

            if (cells[i] is int num)
            {
                cells[i] = SudokuNumber.From((uint)num);
                continue;
            }

            throw new ArgumentException("Unexpected cell content.");
        }
    }

    // PUBLIC
    
    public static object[] AsArray(this IEnumerator<object> objects)
    {
        var res = new List<object>();

        while (objects.MoveNext())
        {
            res.Add(objects.Current);
        }

        return res.ToArray();
    }

    public static Board With(params object[] cells)
    {
        return Board.With(cells);
    }

    public class Board : IResolvable
    {
        // PRIVATE

        private object[] _cells;

        private Board(object[] cells)
        {
            _cells = cells;
        }

        // PUBLIC

        //          +-----------------------+
        //          V                       |
        // +-----+------+-------++------+------+------+------+
        // | Dim | BDim | BDCnt || √Dim | bDim |D/bDim| Dim? |
        // +-----+------+-------++------+------+------+------+
        // |   1 |    1 |     1 ||  1.0 |    1 |    1 |    1 |
        // +-----+------+-------++------+------+------+------+
        // |   2 |    1 |     2 ||  1.4 |    1 |    2 |    2 |
        // +-----+------+-------++------+------+------+------+
        // |   3 |  ! 1 |     3 ||  1.7 |    1 |    3 |    3 | Prime!
        // +-----+------+-------++------+------+------+------+
        // |   4 |    2 |     2 ||  2.0 |    2 |    2 |    4 |
        // +-----+------+-------++------+------+------+------+
        // |   5 |  ! 1 |   ! 5 ||  2.2 |    2 |    2 | 👎 4 | Prime!
        // +-----+------+-------++------+------+------+------+
        // |   6 |    2 |     3 ||  2.4 |    2 |    3 |    6 |
        // +-----+------+-------++------+------+------+------+
        // |   7 |  ! 1 |   ! 7 ||  2.6 |    2 |    3 | 👎 6 | Prime!
        // +-----+------+-------++------+------+------+------+
        // |   8 |    2 |     4 ||  2.8 |    2 |    4 |    8 |
        // +-----+------+-------++------+------+------+------+
        // |   9 |    3 |     3 ||  8.0 |    3 |    3 |    9 |
        // +-----+------+-------++------+------+------+------+
        //          |        ^
        //          +--------+ 


        public static Board With(params object[] cells)
        {
            AssertValidContent(cells);
            return new Board(cells);
        }

        public object GetCellValue(uint i)
        {
            return _cells[i];
        }

        public uint CellCount
        {
            get => (uint) _cells.Length;
        }

        public uint BoardDim
        {
            get => (uint) Math.Sqrt(CellCount);
        }

        public uint BoxDim
        {
            get
            {
                var boardDim = this.BoardDim;
                var boxDim = (uint) Math.Sqrt(this.BoardDim);
                var boxCnt = boardDim / boxDim;

                return ((boxDim * boxCnt) == boardDim) switch
                {
                    true => boxDim,
                    _ => 1
                };
            }
        }

        public uint BoxCnt
        {
            get => CellCount / (BoxDim * BoxDim);
        }

        public uint DimBoxCnt
        {
            get => BoardDim / BoxDim;
        }

        public IEnumerator<object> Row(uint r)
        {
            var dim = this.BoardDim;
            var c0 = r*dim;

            for (int c = 0; c < dim; c++)
            {
                var i = c0 + c;
                yield return _cells[i];
            }
        }

        public IEnumerator<IEnumerator<object>> Rows()
        {
            var dim = this.BoardDim;

            for (uint r = 0; r < dim; r++)
            {
                yield return this.Row(r);
            }
        }

        public IEnumerator<object> Col(uint c)
        {
            var dim = this.BoardDim;

            for (int r = 0; r < dim; r++)
            {
                yield return _cells[r*dim + c];
            }
        }

        public IEnumerator<IEnumerator<object>> Cols()
        {
            var dim = this.BoardDim;

            for (uint c = 0; c < dim; ++c)
            {
                yield return this.Col(c);
            }
        }

        public IEnumerator<object> Box_old(uint b)
        {
            var dim = this.BoardDim;
            var bdim = this.BoxDim;
            var bcnt = this.DimBoxCnt;

            var i0r = (b / bcnt) * bcnt * bdim; // Index (i) to first (0) cell in row (r) that intersects with box.
            var i0b = i0r + (b % bcnt) * bdim;  // Index (i) to first (0) cell in box (b)

            for (var r = 0; r < bdim; ++r)
            {
                var ibr = i0b + r * dim;

                for (var c = 0; c < bdim; ++c)
                {
                    var i = ibr + c;
                    yield return _cells[i];
                }
            }
        }

        // Box: b
        //     bri━┯━bi━━┯━┓ b: 0, br: 0, bri: 0, bi:  0
        //       ┃0│1 │ 2│3┃ b: 1, br: 0, bri: 0, bi:  2
        // br: 0 ┠─0──┼──1─┨ b: 2, br: 1, bri: 8, bi:  8
        //       ┃4│5 │ 6│7┃ b: 3, br: 1, bri: 8, bi: 10
        //     bri─┼─bi──┼─┨ bvol: bdim * bdim
        //       ┃8│9 │ a│b┃ brvol: dim * bdim
        // br: 1 ┠─2──┼──3─┨ br = b / dbcnt
        //       ┃c│d │ e│f┃ bri = br * brvol
        //       ┗━┷━━┷━━┷━┛ bi = bri + (b % dbcnt) * bdim
        
        public IEnumerator<object> Box(uint b)
        {
            var dim = this.BoardDim;
            var bdim = this.BoxDim;
            var dbcnt = this.DimBoxCnt;
            var brvol = dim * bdim;
            var br = b / dbcnt;
            var bri = br * brvol;
            var bi = bri + (b % dbcnt) * bdim; // Index (i) to first (0) cell in row (r) that intersects with box.

            var jend = bi + brvol;

            for (var j = bi; j < jend; j += dim)
            {
                var iend = j + bdim;

                for (var i = j; i < iend; ++i)
                {
                    yield return _cells[i];
                }
            }
        }

        public IEnumerator<IEnumerator<object>> Boxs()
        {
            for (uint b = 0; b < this.BoardDim; ++b)
            {
                yield return this.Box(b);
            }
        }


        public IEnumerator<object> PeersOfCellAt(uint i)
        {
            var dim = this.BoardDim;
            var bdim = this.BoxDim;
            var dbcnt = this.DimBoxCnt;
            var r = i / dim;
            var c = i % dim;
            var brvol = dim * bdim;
            var br = i / brvol;
            var b = br * dbcnt + (i % dim) / bdim;
            var row = this.Row(r);
            var col = this.Col(c);
            var box = this.Box(b);

            while(row.MoveNext())
            {
                yield return row.Current;
            }

            while(col.MoveNext())
            {
                yield return col.Current;
            }   
            
            while(box.MoveNext())
            {
                yield return box.Current;
            }
        }

        public object GetResolvable()
        {
            return this._cells;
        }

        public IResolvable Wrap(object resolved)
        {
            if (resolved is object[] cells)
            {
                return new Board(cells);
            }
            
            throw new ApplicationException("Could not convert to array of cells.");
        }
    }
}
