using nk;

namespace Sudoku;
using static Sudoku.RunnerModule;

public static partial class BoardModule
{
    // PRIVATE

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
        return new Board(cells);
    }

    public record Board(object[] Cells) : IResolvable
    {
        public uint Dim
        {
            get => (uint) Math.Sqrt(Cells.Length);
        }

        public uint BDim
        {
            get => (uint) Math.Sqrt(this.Dim);
        }

        public uint BCnt
        {
            get => Dim / BDim;
        }

        public IEnumerator<object> Row(uint r)
        {
            var dim = this.Dim;
            var c0 = r*dim;

            for (int c = 0; c < dim; c++)
            {
                var i = c0 + c;
                yield return Cells[i];
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
                yield return Cells[r*dim + c];
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
            var bdim = this.BDim;
            var bcnt = this.BCnt;

            var i0r = (b / bcnt) * bcnt * bdim; // Index (i) to first (0) cell in row (r) that intersects with box.
            var i0b = i0r + (b % bcnt) * bdim;  // Index (i) to first (0) cell in box (b)

            for (var r = 0; r < bdim; ++r)
            {
                var ir = i0r + r * dim;

                for (var c = 0; c < bdim; ++c)
                {
                    var i = ir + c;
                    yield return Cells[i];
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
            var bdim = this.BDim;
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
            return this.Cells;
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
