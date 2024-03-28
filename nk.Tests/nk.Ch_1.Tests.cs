
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace nk.Tests;

using static nk.Runners;
using static nk.Goals;
using static nk.Freshes;
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
        RunAll((q) =>
            Fresh((x) =>
                Eqo("pea", x)
            )
        ).ShouldBe("(_0)");
    }

    [Fact]
    public void Test_1_31()
    {
        RunAll((q) =>
            Fresh((x) =>
                Eqo(q, x)
        )).ShouldBe("(_1)");
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
        RunAll((q) => 
            Fresh((x) =>
                Eqo(l(l(l(q)), "pod"), l(l(l(x)), "pod"))
        )).ShouldBe("(_1)");
    }

    [Fact]
    public void Test_1_36()
    {
        RunAll((q) =>
            Fresh((x) =>
                Eqo(l(l(l(q)), x), l(l(l(x)), "pod"))
        )).ShouldBe("(\"pod\")");
    }

    [Fact]
    public void Test_1_37()
    {
        RunAll((q) =>
            Fresh((x) =>
                Eqo(l(x, x), q)
        )).ShouldBe("((_1, _1))");
    }

    [Fact]
    public void Test_1_38()
    {
        RunAll((q) =>
            Fresh((x) =>
                Fresh((y) =>
                    Eqo(l(q, y), l(l(x, y), x))
        ))).ShouldBe("((_1, _1))");
    }

    [Fact]
    public void Test_1_41()
    {
        RunAll((q) => 
            Fresh((x) =>
                Fresh((y) =>
                    Eqo(l(x, y), q)
        ))).ShouldBe("((_1, _2))");
    }

    [Fact]
    public void Test_1_42()
    {
        RunAll((s) => 
            Fresh((t) =>
                Fresh((u) =>
                    Eqo(l(t, u), s)
        ))).ShouldBe("((_1, _2))");
    }

    [Fact]
    public void Test_1_43()
    {
        RunAll((q) => 
            Fresh((x) =>
                Fresh((y) =>
                    Eqo(l(x, y, x), q)
        ))).ShouldBe("((_1, _2, _1))");
    }

    [Fact]
    public void Test_1_50()
    {
        RunAll((q) => 
            Conj(Succ(), Succ())
        ).ShouldBe("(_0)");
    }

    [Fact]
    public void Test_1_51()
    {
        RunAll((q) => 
            Conj(Succ(), Eqo("corn", q))
        ).ShouldBe("(\"corn\")");
    }

    [Fact]
    public void Test_1_52()
    {
        RunAll((q) => 
            Conj(Fail(), Eqo("corn", q))
        ).ShouldBe("()");
    }

    [Fact]
    public void Test_1_53()
    {
        RunAll((q) => 
            Conj(Eqo("corn", q), Eqo("meal", q))
        ).ShouldBe("()");
    }

    [Fact]
    public void Test_1_54()
    {
        RunAll((q) => 
            Conj(Eqo("corn", q), Eqo("corn", q))
        ).ShouldBe("(\"corn\")");
    }

    [Fact]
    public void Test_1_55()
    {
        RunAll((q) => 
            Conj(Fail(), Fail())
        ).ShouldBe("()");
    }

    [Fact]
    public void Test_1_56()
    {
        RunAll((q) => 
            Disj(Eqo("olive", q), Fail())
        ).ShouldBe("(\"olive\")");
    }

    [Fact]
    public void Test_1_57()
    {
        RunAll((q) => 
            Disj(Fail(), Eqo("oil", q))
        ).ShouldBe("(\"oil\")");
    }

    [Fact]
    public void Test_1_58()
    {
        RunAll((q) => 
            Disj(Eqo("olive", q), Eqo("oil", q))
        ).ShouldBe("(\"olive\", \"oil\")");
    }

    [Fact]
    public void Test_1_59()
    {
        RunAll((q) =>
            Fresh((x) =>
                Fresh((y) =>
                    Disj(Eqo(l(x, y), q), Eqo(l(y,x), q))
        ))).ShouldBe("((_1, _2), (_2, _1))");
    }

    [Fact]
    public void Test_1_61a()
    {
        RunAll((x) =>
            Disj(Eqo("olive", x), Eqo("oil" ,x))
        ).ShouldBe("(\"olive\", \"oil\")");
    }

    [Fact]
    public void Test_1_61b()
    {
        RunAll((x) =>
            Disj(Eqo("oil", x), Eqo("olive" ,x))
        ).ShouldBe("(\"oil\", \"olive\")");
    }

    [Fact]
    public void Test_1_62()
    {
        RunAll((x) =>
            Disj(
                Conj(Eqo("olive", x), Fail()),
                Eqo("oil" ,x)
        )).ShouldBe("(\"oil\")");
    }

    [Fact]
    public void Test_1_63()
    {
        RunAll((x) =>
            Disj(
                Conj(Eqo("olive", x), Succ()),
                Eqo("oil" ,x)
        )).ShouldBe("(\"olive\", \"oil\")");
    }

    [Fact]
    public void Test_1_64()
    {
        RunAll((x) =>
            Disj(
                Eqo("oil" ,x),
                Conj(Eqo("olive", x), Succ())
            )
        ).ShouldBe("(\"oil\", \"olive\")");
    }

    [Fact]
    public void Test_1_65()
    {
        RunAll((x) =>
            Disj(
                Conj(Eqo("virgin", x), Fail()),
                Disj(
                    Eqo("olive" ,x),
                    Disj(Succ(), Eqo("oil" ,x))
            ))
        ).ShouldBe("(\"olive\", _0, \"oil\")");
    }

    [Fact]
    public void Test_1_67()
    {
        RunAll((r) =>
            Fresh((x) =>
                Fresh((y) =>
                    Conj(
                        Eqo("split", x),
                        Conj(
                            Eqo("pea", y),
                            Eqo(l(x, y), r)
            ))))
        ).ShouldBe("((\"split\", \"pea\"))");
    }

    [Fact]
    public void Test_1_68()
    {
        RunAll((r) =>
           Fresh((x) =>
                Fresh((y) =>
                    Conj(
                        Conj(
                            Eqo("pea", y),
                            Eqo("split", x)
                        ),
                    Eqo(l(x, y), r)
            )))
        ).ShouldBe("((\"split\", \"pea\"))");
    }

    [Fact]
    public void Test_1_72()
    {
        RunAll((r, x, y) =>
            Conj(
                Conj(
                    Eqo("pea", y),
                    Eqo("split", x)
                ),
                Eqo(l(x, y), r)
            )
        ).ShouldBe("(((\"split\", \"pea\"), \"split\", \"pea\"))");
    }

    [Fact]
    public void Test_1_75()
    {
        RunAll((x, y) =>
            Conj(
                Eqo("pea", y),
                Eqo("split", x)
            )
        ).ShouldBe("((\"split\", \"pea\"))");
    }

    [Fact]
    public void Test_1_76()
    {
        RunAll((x, y) =>
            Disj(
                Conj(Eqo("split", x), Eqo("pea", y)),
                Conj(Eqo("red", x), Eqo("bean", y))
            )
        ).ShouldBe("((\"split\", \"pea\"), (\"red\", \"bean\"))");
    }
    
    [Fact]
    public void Test_1_77()
    {
        RunAll((r) =>
            Fresh((x, y) =>
                Conj(
                    Disj(
                        Conj(Eqo("split", x), Eqo("pea", y)),
                        Conj(Eqo("red", x), Eqo("bean", y))
                    ),
                    Eqo(l(x, y, "soup"), r)
            ))
        ).ShouldBe("((\"split\", \"pea\", \"soup\"), (\"red\", \"bean\", \"soup\"))");
    }

    [Fact]
    public void Test_1_78()
    {
        RunAll((r) =>
            Fresh((x, y) => Conj( // NOTE: Implicit Fresh conjunction presented in 1_78, is not possible in C#.
                Disj(
                    Conj(Eqo("split", x), Eqo("pea", y)),
                    Conj(Eqo("red", x), Eqo("bean", y))
                ),
                Eqo(l(x, y, "soup"), r)
            ))
        ).ShouldBe("((\"split\", \"pea\", \"soup\"), (\"red\", \"bean\", \"soup\"))");
    }

    [Fact]
    public void Test_1_80()
    {
        RunAll((x, y, z) => Conj( // NOTE: Implicit Run conjunction presented in 1_80, is not possible in C#.
            Disj(
                Conj(Eqo("split", x), Eqo("pea", y)),
                Conj(Eqo("red", x), Eqo("bean", y))
            ),
            Eqo("soup", z)
        )).ShouldBe("((\"split\", \"pea\", \"soup\"), (\"red\", \"bean\", \"soup\"))");
    }
}
