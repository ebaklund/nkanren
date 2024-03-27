
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;

namespace nk.Tests;

using static nk.Runners;
using static nk.Goals;
using static nk.ListConstructor;

public class Paythings
{
    protected readonly ITestOutputHelper _output;

    public Paythings(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Test_1_07() // p 3
    {
        var res = RunAll((Key q) => Fail()).AsString();
        _output.WriteLine($"result: {res}");

        res.Should().Be("()");
    }

    [Fact]
    public void Test_1_10() // p 3
    {
        var res = RunAll((Key q) => Eqo("pea", "pod")).AsString();
        _output.WriteLine($"result: {res}");

        res.Should().Be("()");
    }

    [Fact]
    public void Test_1_11() // p 4
    {
        var res = RunAll((Key q) => Eqo(q, "pea")).AsString();
        _output.WriteLine($"result: {res}");

        res.Should().Be("(\"pea\")");
    }

    [Fact]
    public void Test_1_12() // p 4
    {
        var res = RunAll((Key q) => Eqo("pea", q)).AsString();
        _output.WriteLine($"result: {res}");

        res.Should().Be("(\"pea\")");
    }

    [Fact]
    public void Test_1_17() // p 6
    {
        var res = RunAll((Key q) => Succ()).AsString();
        _output.WriteLine($"result: {res}");

        res.Should().Be("(_0)");
    }

    [Fact]
    public void Test_1_19() // p 6
    {
        var res = RunAll((Key q) => Eqo("pea", "pea")).AsString();
        _output.WriteLine($"result: {res}");

        res.Should().Be("(_0)");
    }
    
    [Fact]
    public void Test_1_20() // p 6
    {
        var res = RunAll((Key q) => Eqo(q, q)).AsString();
        _output.WriteLine($"result: {res}");

        res.Should().Be("(_0)");
    }

    [Fact]
    public void Test_1_24()
    {
        var res = RunAll((Key q) => Fresh((Key x) => Eqo("pea", x))).AsString();
        _output.WriteLine($"result: {res}");

        res.Should().Be("(_0)");
    }

    [Fact]
    public void Test_1_31()
    {
        var res = RunAll((Key q) => Fresh((Key x) => Eqo(q, x))).AsString();
        _output.WriteLine($"result: {res}");

        res.Should().Be("(_1)");
    }

    [Fact]
    public void Test_1_32()
    {
        var res = RunAll((Key q) => Eqo(l(l(l("pea")), "pod"), l(l(l("pea")), "pod"))).AsString();
        _output.WriteLine($"result: {res}");

        res.Should().Be("(_0)");
    }

    [Fact]
    public void Test_1_33()
    {
        var res = RunAll((Key q) => Eqo(l(l(l("pea")), "pod"), l(l(l("pea")), q))).AsString();
        _output.WriteLine($"result: {res}");

        res.Should().Be("(\"pod\")");
    }

    [Fact]
    public void Test_1_34()
    {
        var res = RunAll((Key q) => Eqo(l(l(l(q)), "pod"), l(l(l("pea")), "pod"))).AsString();
        _output.WriteLine($"result: {res}");

        res.Should().Be("(\"pea\")");
    }

    [Fact]
    public void Test_1_35()
    {
        var res = RunAll((Key q) => 
            Fresh((Key x) => 
                Eqo(l(l(l(q)), "pod"), l(l(l(x)), "pod")))).AsString();

        _output.WriteLine($"result: {res}");

        res.Should().Be("(_1)");
    }

    [Fact]
    public void Test_1_36()
    {
        var res = RunAll((Key q) => 
            Fresh((Key x) => 
                Eqo(l(l(l(q)), x), l(l(l(x)), "pod")))).AsString();

        _output.WriteLine($"result: {res}");

        res.Should().Be("(\"pod\")");
    }

    [Fact]
    public void Test_1_37()
    {
        var res = RunAll((Key q) => 
            Fresh((Key x) => 
                Eqo(l(x, x), q))).AsString();

        _output.WriteLine($"result: {res}");

        res.Should().Be("((_1, _1))");
    }
}
