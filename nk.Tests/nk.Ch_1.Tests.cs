
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace nk.Tests;

using static nk.Runners;
using static nk.Goals;
using static nk.ListConstructor;

public class Playthings
{
    // PROTECTED

    protected readonly ITestOutputHelper _output;

    protected void WriteLine(string line)
    {
        _output.WriteLine(line);
    }

    // PUBLIC

    public Playthings(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Test_1_07() // p 3
    {
        RunAll((q) => 
            Fail()
        ).ShouldBe("()");
    }

    [Fact]
    public void Test_1_10() // p 3
    {
        RunAll((q) => 
            Eqo("pea", "pod")
        ).ShouldBe("()");
    }

    [Fact]
    public void Test_1_11() // p 4
    {
        RunAll((q) =>
            Eqo(q, "pea")
        ).ShouldBe("(\"pea\")");
    }

    [Fact]
    public void Test_1_12() // p 4
    {
        RunAll((q) =>
            Eqo("pea", q)
        ).ShouldBe("(\"pea\")");
    }

    [Fact]
    public void Test_1_17() // p 6
    {
        RunAll((q) =>
            Succ()
        ).ShouldBe("(_0)");
    }

    [Fact]
    public void Test_1_19() // p 6
    {
        RunAll((q) =>
            Eqo("pea", "pea")
        ).ShouldBe("(_0)");
    }
    
    [Fact]
    public void Test_1_20() // p 6
    {
        RunAll((q) =>
            Eqo(q, q)
        ).ShouldBe("(_0)");
    }

    [Fact]
    public void Test_1_24()
    {
        RunAll((q, x) =>
            Eqo("pea", x)
        ).ShouldBe("(_0)");
    }

    [Fact]
    public void Test_1_31()
    {
        RunAll((q, x) =>
            Eqo(q, x)
        ).ShouldBe("(_1)");
    }

    [Fact]
    public void Test_1_32()
    {
        RunAll((q) =>
            Eqo(l(l(l("pea")), "pod"), l(l(l("pea")), "pod"))
        ).ShouldBe("(_0)");
    }

    [Fact]
    public void Test_1_33()
    {
        RunAll((q) =>
            Eqo(l(l(l("pea")), "pod"), l(l(l("pea")), q))
        ).ShouldBe("(\"pod\")");
    }

    [Fact]
    public void Test_1_34()
    {
        RunAll((q) =>
            Eqo(l(l(l(q)), "pod"), l(l(l("pea")), "pod"))
        ).ShouldBe("(\"pea\")");
    }

    [Fact]
    public void Test_1_35()
    {
        RunAll((q, x) => 
            Eqo(l(l(l(q)), "pod"), l(l(l(x)), "pod"))
        ).ShouldBe("(_1)");
    }

    [Fact]
    public void Test_1_36()
    {
        RunAll((q, x) =>
            Eqo(l(l(l(q)), x), l(l(l(x)), "pod"))
        ).ShouldBe("(\"pod\")");
    }

    [Fact]
    public void Test_1_37()
    {
        RunAll((q, x) =>
            Eqo(l(x, x), q)
        ).ShouldBe("((_1, _1))");
    }

    [Fact]
    public void Test_1_38()
    {
        RunAll((q, x, y) =>
            Eqo(l(q, y), l(l(x, y), x))
        ).ShouldBe("((_1, _1))");
    }

    [Fact]
    public void Test_1_41()
    {
        RunAll((q, x, y) => 
            Eqo(l(x, y), q)
        ).ShouldBe("((_1, _2))");
    }

    [Fact]
    public void Test_1_42()
    {
        RunAll((s, t, u) => 
            Eqo(l(t, u), s)
        ).ShouldBe("((_1, _2))");
    }

    [Fact]
    public void Test_1_43()
    {
        RunAll((q, x, y) => 
            Eqo(l(x, y, x), q)
        ).ShouldBe("((_1, _2, _1))");
    }

    [Fact]
    public void Test_1_50()
    {
        RunAll((q) => 
            Conj2(Succ(), Succ())
        ).ShouldBe("(_0)");
    }

    [Fact]
    public void Test_1_51()
    {
        RunAll((q) => 
            Conj2(Succ(), Eqo("corn", q))
        ).ShouldBe("(\"corn\")");
    }

    [Fact]
    public void Test_1_52()
    {
        RunAll((q) => 
            Conj2(Fail(), Eqo("corn", q))
        ).ShouldBe("()");
    }

    [Fact]
    public void Test_1_53()
    {
        RunAll((q) => 
            Conj2(Eqo("corn", q), Eqo("meal", q))
        ).ShouldBe("()");
    }

    [Fact]
    public void Test_1_54()
    {
        RunAll((q) => 
            Conj2(Eqo("corn", q), Eqo("corn", q))
        ).ShouldBe("(\"corn\")");
    }

    [Fact]
    public void Test_1_55()
    {
        RunAll((q) => 
            Conj2(Fail(), Fail())
        ).ShouldBe("()");
    }

    [Fact]
    public void Test_1_56()
    {
        RunAll((q) => 
            Disj2(Eqo("olive", q), Fail())
        ).ShouldBe("(\"olive\")");
    }

    [Fact]
    public void Test_1_57()
    {
        RunAll((q) => 
            Disj2(Fail(), Eqo("oil", q))
        ).ShouldBe("(\"oil\")");
    }

    [Fact]
    public void Test_1_58()
    {
        RunAll((q) => 
            Disj2(Eqo("olive", q), Eqo("oil", q))
        ).ShouldBe("(\"olive\", \"oil\")");
    }

    [Fact]
    public void Test_1_59()
    {
        RunAll((q, x, y) =>
            Disj2(Eqo(l(x, y), q), Eqo(l(y,x), q))
        ).ShouldBe("((_1, _2), (_2, _1))");
    }

    [Fact]
    public void Test_1_61a()
    {
        RunAll((x) =>
            Disj2(Eqo("olive", x), Eqo("oil" ,x))
        ).ShouldBe("(\"olive\", \"oil\")");
    }

    [Fact]
    public void Test_1_61b()
    {
        RunAll((x) =>
            Disj2(Eqo("oil", x), Eqo("olive" ,x))
        ).ShouldBe("(\"oil\", \"olive\")");
    }

    [Fact]
    public void Test_1_62()
    {
        RunAll((x) =>
            Disj2(
                Conj2(Eqo("olive", x), Fail()),
                Eqo("oil" ,x)
            )
        ).ShouldBe("(\"oil\")");
    }

    [Fact]
    public void Test_1_63()
    {
        RunAll((x) =>
            Disj2(
                Conj2(Eqo("olive", x), Succ()),
                Eqo("oil" ,x)
            )
        ).ShouldBe("(\"olive\", \"oil\")");
    }

    [Fact]
    public void Test_1_64()
    {
        RunAll((x) =>
            Disj2(
                Eqo("oil" ,x),
                Conj2(Eqo("olive", x), Succ())
            )
        ).ShouldBe("(\"oil\", \"olive\")");
    }

    [Fact]
    public void Test_1_65()
    {
        RunAll((x) =>
            Disj2(
                Conj2(Eqo("virgin", x), Fail()),
                Disj2(
                    Eqo("olive" ,x),
                    Disj2(Succ(), Eqo("oil" ,x))
                )
            )
        ).ShouldBe("(\"olive\", _0, \"oil\")");
    }

    [Fact]
    public void Test_1_66()
    {
        RunAll((r, x, y) =>
            Conj2(
                Eqo("split", x),
                Conj2(
                    Eqo("pea", y),
                    Eqo(l(x, y), r)
                )
            )
        ).ShouldBe("((\"split\", \"pea\"))");
    }
}
