
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace nk.Tests;

using static nk.Runners;
using static nk.Goals;
using static nk.Freshs;
using static nk.ListConstructor;

public class Paythings
{
    protected readonly ITestOutputHelper _output;

    protected void WriteLine(string line)
    {
        _output.WriteLine(line);
    }

    public Paythings(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Test_1_07() // p 3
    {
        var res = RunAll((Key q) => Fail()).AsString();
        WriteLine(res);

        res.Should().Be("()");
    }

    [Fact]
    public void Test_1_10() // p 3
    {
        var res = RunAll((Key q) => Eqo("pea", "pod")).AsString();
        WriteLine(res);

        res.Should().Be("()");
    }

    [Fact]
    public void Test_1_11() // p 4
    {
        var res = RunAll((Key q) => Eqo(q, "pea")).AsString();
        WriteLine(res);

        res.Should().Be("(\"pea\")");
    }

    [Fact]
    public void Test_1_12() // p 4
    {
        var res = RunAll((Key q) => Eqo("pea", q)).AsString();
        WriteLine(res);

        res.Should().Be("(\"pea\")");
    }

    [Fact]
    public void Test_1_17() // p 6
    {
        var res = RunAll((Key q) => Succ()).AsString();
        WriteLine(res);

        res.Should().Be("(_0)");
    }

    [Fact]
    public void Test_1_19() // p 6
    {
        var res = RunAll((Key q) => Eqo("pea", "pea")).AsString();
        WriteLine(res);

        res.Should().Be("(_0)");
    }
    
    [Fact]
    public void Test_1_20() // p 6
    {
        var res = RunAll((Key q) => Eqo(q, q)).AsString();
        WriteLine(res);

        res.Should().Be("(_0)");
    }

    [Fact]
    public void Test_1_24()
    {
        var res = RunAll((Key q) => Fresh((Key x) => Eqo("pea", x))).AsString();
        WriteLine(res);

        res.Should().Be("(_0)");
    }

    [Fact]
    public void Test_1_31()
    {
        var res = RunAll((Key q) => Fresh((Key x) => Eqo(q, x))).AsString();
        WriteLine(res);

        res.Should().Be("(_1)");
    }

    [Fact]
    public void Test_1_32()
    {
        var res = RunAll((Key q) => Eqo(l(l(l("pea")), "pod"), l(l(l("pea")), "pod"))).AsString();
        WriteLine(res);

        res.Should().Be("(_0)");
    }

    [Fact]
    public void Test_1_33()
    {
        var res = RunAll((Key q) => Eqo(l(l(l("pea")), "pod"), l(l(l("pea")), q))).AsString();
        WriteLine(res);

        res.Should().Be("(\"pod\")");
    }

    [Fact]
    public void Test_1_34()
    {
        var res = RunAll((Key q) => Eqo(l(l(l(q)), "pod"), l(l(l("pea")), "pod"))).AsString();
        WriteLine(res);

        res.Should().Be("(\"pea\")");
    }

    [Fact]
    public void Test_1_35()
    {
        var res = RunAll((Key q) => 
            Fresh((Key x) => 
                Eqo(l(l(l(q)), "pod"), l(l(l(x)), "pod")))).AsString();

        WriteLine(res);

        res.Should().Be("(_1)");
    }

    [Fact]
    public void Test_1_36()
    {
        var res = RunAll((Key q) => 
            Fresh((Key x) => 
                Eqo(l(l(l(q)), x), l(l(l(x)), "pod")))).AsString();

        WriteLine(res);

        res.Should().Be("(\"pod\")");
    }

    [Fact]
    public void Test_1_37()
    {
        var res = RunAll((Key q) => 
            Fresh((Key x) => 
                Eqo(l(x, x), q))).AsString();

        WriteLine(res);

        res.Should().Be("((_1, _1))");
    }

    [Fact]
    public void Test_1_38()
    {
        var res = RunAll((Key q) => 
            Fresh((Key x) => 
                Fresh((Key y) => 
                    Eqo(l(q, y), l(l(x, y), x))))).AsString();

        WriteLine(res);

        res.Should().Be("((_1, _2))");
    }

    [Fact]
    public void Test_1_41()
    {
        var res = RunAll((Key q) => 
            Fresh((Key x) => 
                Fresh((Key y) => 
                    Eqo(l(x, y), q)))).AsString();

        WriteLine(res);

        res.Should().Be("((_1, _2))");
    }

    [Fact]
    public void Test_1_42()
    {
        var res = RunAll((Key s) => 
            Fresh((Key t) => 
                Fresh((Key u) => 
                    Eqo(l(t, u), s)))).AsString();

        WriteLine(res);

        res.Should().Be("((_1, _2))");
    }

    [Fact]
    public void Test_1_43()
    {
        var res = RunAll((Key q) => 
            Fresh((Key x) => 
                Fresh((Key y) => 
                    Eqo(l(x, y, x), q)))).AsString();

        WriteLine(res);

        res.Should().Be("((_1, _2, _1))");
    }

    [Fact]
    public void Test_1_50()
    {
        var res = RunAll((Key q) => 
            Conj2(Succ(), Succ())).AsString();

        WriteLine(res);

        res.Should().Be("(_0)");
    }

    [Fact]
    public void Test_1_51()
    {
        var res = RunAll((Key q) => 
            Conj2(Succ(), Eqo("corn", q))).AsString();

        WriteLine(res);

        res.Should().Be("(\"corn\")");
    }

    [Fact]
    public void Test_1_52()
    {
        var res = RunAll((Key q) => 
            Conj2(Fail(), Eqo("corn", q))).AsString();

        WriteLine(res);

        res.Should().Be("()");
    }

    [Fact]
    public void Test_1_53()
    {
        var res = RunAll((Key q) => 
            Conj2(Eqo("corn", q), Eqo("meal", q))).AsString();

        WriteLine(res);

        res.Should().Be("()");
    }

    [Fact]
    public void Test_1_54()
    {
        var res = RunAll((Key q) => 
            Conj2(Eqo("corn", q), Eqo("corn", q))).AsString();

        WriteLine(res);

        res.Should().Be("(\"corn\")");
    }

    [Fact]
    public void Test_1_55()
    {
        var res = RunAll((Key q) => 
            Conj2(Fail(), Fail())).AsString();

        WriteLine(res);

        res.Should().Be("()");
    }

    [Fact]
    public void Test_1_56()
    {
        var res = RunAll((Key q) => 
            Disj2(Eqo("olive", q), Fail())).AsString();

        WriteLine(res);

        res.Should().Be("(\"olive\")");
    }

    [Fact]
    public void Test_1_57()
    {
        var res = RunAll((Key q) => 
            Disj2(Fail(), Eqo("oil", q))).AsString();

        WriteLine(res);

        res.Should().Be("(\"oil\")");
    }

    [Fact]
    public void Test_1_58()
    {
        var res = RunAll((Key q) => 
            Disj2(Eqo("olive", q), Eqo("oil", q))).AsString();

        WriteLine(res);

        res.Should().Be("(\"olive\", \"oil\")");
    }

    [Fact]
    public void Test_1_59()
    {
        var res = RunAll((Key q) =>
            Fresh((Key x, Key y) =>
                Disj2(Eqo(l(x, y), q), Eqo(l(y,x), q)))).AsString();

        WriteLine(res);

        res.Should().Be("((_1, _2), (_2, _1))");
    }
}
