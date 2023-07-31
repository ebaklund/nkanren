
using Xunit;
using FluentAssertions;

namespace nkanren.Tests;

public class UnderTheHood
{
    [Fact]
    public void Test_10_5()
    {
        var x = new Key();
        var y = new Key();
        var z = new Key();

        x.Should().BeOfType<Key>();
        y.Should().BeOfType<Key>();
        z.Should().BeOfType<Key>();
    }

    [Fact]
    public void Test_10_11()
    {
        new Subst().Should().BeOfType<Subst>();
    }

    [Fact]
    public void Test_10_12()
    {
        var x = new Key();
        var z = new Key();

        new Subst().Extend((z, null), (x, null), (z, null)).Item2?.Message.Should().Contain("[0], [2]");
    }

    [Fact]
    public void Test_10_13()
    {
        var x = new Key();
        var y = new Key();
        var z = new Key();
        var w = new Key();

        Subst s = new();
        s.Extend((z, "a"), (x, w), (y, z));
        s.Walk(z).Item1?.ToString().Should().Match("a");
    }

    [Fact]
    public void Test_10_14()
    {
        var x = new Key();
        var y = new Key();
        var z = new Key();
        var w = new Key();

        Subst s = new();
        s.Extend((z, "a"), (x, w), (y, z));
        s.Walk(y).Item1?.ToString().Should().Match("a");
    }

    [Fact]
    public void Test_10_15()
    {
        var x = new Key();
        var y = new Key();
        var z = new Key();
        var w = new Key();

        Subst s = new();
        s.Extend((z, "a"), (x, w), (y, z));
        s.Walk(x).Item1?.Should().Be(w);
    }

    [Fact]
    public void Test_10_16()
    {
        var x = new Key();
        var y = new Key();
        var v = new Key();
        var w = new Key();

        Subst s = new();
        s.Extend((x, y), (v, x), (w, x));
        s.Walk(x).Item1?.Should().Be(y);
        s.Walk(v).Item1?.Should().Be(y);
        s.Walk(w).Item1?.Should().Be(y);
    }

    [Fact]
    public void Test_10_17()
    {
        var x = new Key();
        var y = new Key();
        var z = new Key();
        var w = new Key();

        Subst s = new();
        s.Extend((x, "b"), (z, y), (w, (x, "e", z)));
        s.Walk(w).Item1?.Should().BeEquivalentTo((x, "e", z));
    }

    [Fact]
    public void Test_10_21()
    {
        var x = new Key();
        var y = new Key();
        var z = new Key();
        var t = ((z, "a"), (x, x), (y, z));

        Subst s = new();
        s.Occurs(x, t).Should().BeTrue();

        s.Extend((x, "b"), (z, y), (w, (x, "e", z)));
        s.Walk(w).Item1?.Should().BeEquivalentTo((x, "e", z));
    }


    /*
    [Fact]
    public void Test_7()
    {
        NK.RunAll((q) => NK.Result.Failure).Should().BeEquivalentTo(NK.E);
    }

    [Fact]
    public void Test_10()
    {
        NK.RunAll((q) => NK.AreEqual("pea", "pod")).Should().BeEquivalentTo(NK.E);
    }

    [Fact]
    public void Test_11()
    {
        NK.RunAll((q) => NK.AreEqual(q, "pea")).Should().BeEquivalentTo("pea");
    }
    */
}
