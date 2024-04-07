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

        public uint CellCount
        {
            get => (uint) _cells.Length;
        }

        public uint Dim
        {
            get => (uint) Math.Sqrt(CellCount);
        }

        public uint BoxDim
        {
            get
            {
                var boxDim = (uint) Math.Sqrt(Dim);
                var boxCnt = Dim / boxDim;
                var boardDim = boxDim * boxCnt;

                return (boardDim == this.Dim) switch
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
            get => Dim / BoxDim;
        }

        public IEnumerator<object> Row(uint r)
        {
            var dim = this.Dim;
            var c0 = r*dim;

            for (int c = 0; c < dim; c++)
            {
                var i = c0 + c;
                yield return _cells[i];
            }
        }

        public IEnumerator<IEnumerator<object>> Rows()
        {
            for (uint r = 0; r < this.Dim; r++)
            {
                yield return this.Row(r);
            }
        }

        public IEnumerator<object> Col(uint c)
        {
            var dim = this.Dim;

            for (int r = 0; r < dim; r++)
            {
                yield return _cells[r*dim + c];
            }
        }

        public IEnumerator<IEnumerator<object>> Cols()
        {
            for (uint c = 0; c < this.Dim; ++c)
            {
                yield return this.Col(c);
            }
        }

        public IEnumerator<object> Box(uint b)
        {
            var dim = this.Dim;
            var bdim = this.BoxDim;
            var bcnt = this.DimBoxCnt;

            var i0r = (b / bcnt) * bcnt * bdim; // Index (i) to first (0) cell in row (r) that intersects with box.
            var i0b = i0r + (b % bcnt) * bdim;  // Index (i) to first (0) cell in box (b)

            for (var r = 0; r < bdim; ++r)
            {
                var ir = i0r + r * dim;

                for (var c = 0; c < bdim; ++c)
                {
                    var i = ir + c;
                    yield return _cells[i];
                }
            }
        }

        public IEnumerator<IEnumerator<object>> Boxs()
        {
            for (uint b = 0; b < this.Dim; ++b)
            {
                yield return this.Box(b);
            }
        }

        public IEnumerator<object> PeersOfCellAt(uint i)
        {
            var bdim = this.BoxDim;
            var r = i / this.Dim;
            var c = i % this.Dim;
            var b = (r / bdim) * bdim + c / bdim;
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
