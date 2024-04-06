using nk;

namespace Sudoku;


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

    public record Board(params object[] Cells)
    {
        public uint Dim
        {
            get => (uint) Math.Sqrt(Cells.Length);
        }

        public uint BDim
        {
            get => (uint) Math.Sqrt(this.Dim);
        }

        public IEnumerator<object> Row(uint r)
        {
            var dim = this.Dim;

            for (int c = 0; c < dim; c++)
            {
                yield return Cells[r*dim + c];
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
            var r0 = (b / bdim) * bdim;
            var c0 = (b % bdim) * bdim;
            var rn = r0 + bdim;
            var cn = c0 + bdim;

            for (var r = r0; r < rn; ++r)
            {
                for (var c = c0; c < cn; ++c)
                {
                    yield return Cells[r*dim + c];
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

        public IEnumerator<object> Siblings(uint i)
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
    }
}
