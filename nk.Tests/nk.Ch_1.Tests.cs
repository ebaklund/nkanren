
using Xunit;
using Xunit.Abstractions;

namespace nk.Tests;

using static nk.Runners;
using static nk.GoalsModule;
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
            Equal("pea", "pod")
        ).ShouldBe("()");
    }

    [Fact]
    public void Test_1_11() // p 4
    {
        RunAll((q) =>
            Equal(q, "pea")
        ).ShouldBe("(\"pea\")");
    }

    [Fact]
    public void Test_1_12() // p 4
    {
        RunAll((q) =>
            Equal("pea", q)
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
            Equal("pea", "pea")
        ).ShouldBe("(_0)");
    }
    
    [Fact]
    public void Test_1_20() // p 6
    {
        RunAll((q) =>
            Equal(q, q)
        ).ShouldBe("(_0)");
    }

    [Fact]
    public void Test_1_24a()
    {
        RunAll((q) =>
            Fresh((x) =>
                Equal("pea", x)
            )
        ).ShouldBe("(_0)");
    }

    [Fact]
    public void Test_1_24b()
    {
        RunAll((q) =>
            Fresh(1, (x) =>
                Equal("pea", x[0])
            )
        ).ShouldBe("(_0)");
    }

    [Fact]
    public void Test_1_31a()
    {
        RunAll((q) =>
            Fresh((x) =>
                Equal(q, x)
        )).ShouldBe("(_1)");
    }

    [Fact]
    public void Test_1_31b()
    {
        RunAll((q) =>
            Fresh(1, (x) =>
                Equal(q, x[0])
        )).ShouldBe("(_1)");
    }

    [Fact]
    public void Test_1_32()
    {
        RunAll((q) =>
            Equal(l(l(l("pea")), "pod"), l(l(l("pea")), "pod"))
        ).ShouldBe("(_0)");
    }

    [Fact]
    public void Test_1_33()
    {
        RunAll((q) =>
            Equal(l(l(l("pea")), "pod"), l(l(l("pea")), q))
        ).ShouldBe("(\"pod\")");
    }

    [Fact]
    public void Test_1_34()
    {
        RunAll((q) =>
            Equal(l(l(l(q)), "pod"), l(l(l("pea")), "pod"))
        ).ShouldBe("(\"pea\")");
    }

    [Fact]
    public void Test_1_35a()
    {
        RunAll((q) => 
            Fresh((x) =>
                Equal(l(l(l(q)), "pod"), l(l(l(x)), "pod"))
        )).ShouldBe("(_1)");
    }
    
    [Fact]
    public void Test_1_35b()
    {
        RunAll((q) => 
            Fresh(1, (x) =>
                Equal(l(l(l(q)), "pod"), l(l(l(x[0])), "pod"))
        )).ShouldBe("(_1)");
    }

    [Fact]
    public void Test_1_36a()
    {
        RunAll((q) =>
            Fresh((x) =>
                Equal(l(l(l(q)), x), l(l(l(x)), "pod"))
        )).ShouldBe("(\"pod\")");
    }

    [Fact]
    public void Test_1_36b()
    {
        RunAll((q) =>
            Fresh(1, (x) =>
                Equal(l(l(l(q)), x[0]), l(l(l(x[0])), "pod"))
        )).ShouldBe("(\"pod\")");
    }

    [Fact]
    public void Test_1_37a()
    {
        RunAll((q) =>
            Fresh((x) =>
                Equal(l(x, x), q)
        )).ShouldBe("((_1, _1))");
    }

    [Fact]
    public void Test_1_37b()
    {
        RunAll((q) =>
            Fresh(1, (x) =>
                Equal(l(x[0], x[0]), q)
        )).ShouldBe("((_1, _1))");
    }
    
    [Fact]
    public void Test_1_38a()
    {
        RunAll((q) =>
            Fresh((x) =>
                Fresh((y) =>
                    Equal(l(q, y), l(l(x, y), x))
        ))).ShouldBe("((_1, _1))");
    }

    [Fact]
    public void Test_1_38b()
    {
        RunAll((q) =>
            Fresh(2, (x) =>
                Equal(l(q, x[1]), l(l(x[0], x[1]), x[0]))
        )).ShouldBe("((_1, _1))");
    }

    [Fact]
    public void Test_1_41a()
    {
        RunAll((q) => 
            Fresh((x) =>
                Fresh((y) =>
                    Equal(l(x, y), q)
        ))).ShouldBe("((_1, _2))");
    }
    
    [Fact]
    public void Test_1_41b()
    {
        RunAll((q) => 
            Fresh(2, (x) =>
                Equal(l(x[0], x[1]), q)
        )).ShouldBe("((_1, _2))");
    }

    [Fact]
    public void Test_1_42a()
    {
        RunAll((s) => 
            Fresh((t) =>
                Fresh((u) =>
                    Equal(l(t, u), s)
        ))).ShouldBe("((_1, _2))");
    }

    [Fact]
    public void Test_1_42b()
    {
        RunAll((s) => 
            Fresh(2, (t) =>
                Equal(l(t[0], t[1]), s)
        )).ShouldBe("((_1, _2))");
    }

    [Fact]
    public void Test_1_43a()
    {
        RunAll((q) => 
            Fresh((x) =>
                Fresh((y) =>
                    Equal(l(x, y, x), q)
        ))).ShouldBe("((_1, _2, _1))");
    }

    [Fact]
    public void Test_1_43b()
    {
        RunAll((q) => 
            Fresh(2, (x) =>
                Equal(l(x[0], x[1], x[0]), q)
        )).ShouldBe("((_1, _2, _1))");
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
            Conj(Succ(), Equal("corn", q))
        ).ShouldBe("(\"corn\")");
    }

    [Fact]
    public void Test_1_52()
    {
        RunAll((q) => 
            Conj(Fail(), Equal("corn", q))
        ).ShouldBe("()");
    }

    [Fact]
    public void Test_1_53()
    {
        RunAll((q) => 
            Conj(Equal("corn", q), Equal("meal", q))
        ).ShouldBe("()");
    }

    [Fact]
    public void Test_1_54()
    {
        RunAll((q) => 
            Conj(Equal("corn", q), Equal("corn", q))
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
            Disj(Equal("olive", q), Fail())
        ).ShouldBe("(\"olive\")");
    }

    [Fact]
    public void Test_1_57()
    {
        RunAll((q) => 
            Disj(Fail(), Equal("oil", q))
        ).ShouldBe("(\"oil\")");
    }

    [Fact]
    public void Test_1_58()
    {
        RunAll((q) => 
            Disj(Equal("olive", q), Equal("oil", q))
        ).ShouldBe("(\"olive\", \"oil\")");
    }

    [Fact]
    public void Test_1_59a()
    {
        RunAll((q) =>
            Fresh((x) =>
                Fresh((y) =>
                    Disj(Equal(l(x, y), q), Equal(l(y, x), q))
        ))).ShouldBe("((_1, _2), (_2, _1))");
    }

    [Fact]
    public void Test_1_59b()
    {
        RunAll((q) =>
            Fresh(2, (x) =>
                Disj(Equal(l(x[0], x[1]), q), Equal(l(x[1], x[0]), q))
        )).ShouldBe("((_1, _2), (_2, _1))");
    }

    [Fact]
    public void Test_1_61a()
    {
        RunAll((x) =>
            Disj(Equal("olive", x), Equal("oil" ,x))
        ).ShouldBe("(\"olive\", \"oil\")");
    }

    [Fact]
    public void Test_1_61b()
    {
        RunAll((x) =>
            Disj(Equal("oil", x), Equal("olive" ,x))
        ).ShouldBe("(\"oil\", \"olive\")");
    }

    [Fact]
    public void Test_1_62()
    {
        RunAll((x) =>
            Disj(
                Conj(Equal("olive", x), Fail()),
                Equal("oil" ,x)
        )).ShouldBe("(\"oil\")");
    }

    [Fact]
    public void Test_1_63()
    {
        RunAll((x) =>
            Disj(
                Conj(Equal("olive", x), Succ()),
                Equal("oil" ,x)
        )).ShouldBe("(\"olive\", \"oil\")");
    }

    [Fact]
    public void Test_1_64()
    {
        RunAll((x) =>
            Disj(
                Equal("oil" ,x),
                Conj(Equal("olive", x), Succ())
            )
        ).ShouldBe("(\"oil\", \"olive\")");
    }

    [Fact]
    public void Test_1_65()
    {
        RunAll((x) =>
            Disj(
                Conj(Equal("virgin", x), Fail()),
                Disj(
                    Equal("olive" ,x),
                    Disj(Succ(), Equal("oil" ,x))
            ))
        ).ShouldBe("(\"olive\", _0, \"oil\")");
    }

    [Fact]
    public void Test_1_67a()
    {
        RunAll((r) =>
            Fresh((x) =>
                Fresh((y) =>
                    Conj(
                        Equal("split", x),
                        Conj(
                            Equal("pea", y),
                            Equal(l(x, y), r)
            ))))
        ).ShouldBe("((\"split\", \"pea\"))");
    }

    [Fact]
    public void Test_1_67b()
    {
        RunAll((r) =>
            Fresh(2, (x) =>
                Conj(
                    Equal("split", x[0]),
                    Conj(
                        Equal("pea", x[1]),
                        Equal(l(x[0], x[1]), r)
            )))
        ).ShouldBe("((\"split\", \"pea\"))");
    }

    [Fact]
    public void Test_1_68a()
    {
        RunAll((r) =>
           Fresh((x) =>
                Fresh((y) =>
                    Conj(
                        Conj(
                            Equal("pea", y),
                            Equal("split", x)
                        ),
                    Equal(l(x, y), r)
           )))
        ).ShouldBe("((\"split\", \"pea\"))");
    }

    [Fact]
    public void Test_1_68b()
    {
        RunAll((r) =>
           Fresh(2, (x) =>
                Conj(
                    Conj(
                        Equal("pea", x[1]),
                        Equal("split", x[0])
                    ),
                Equal(l(x[0], x[1]), r)
           ))
        ).ShouldBe("((\"split\", \"pea\"))");
    }

    [Fact]
    public void Test_1_72_nk()
    {
        RunAll(2, (r, x) =>
            Conj(
                Conj(
                    Equal("pea", x[1]),
                    Equal("split", x[0])
                ),
                Equal(l(x, x[0], x[1]), r)
            )
        ).ShouldBe("(((\"split\", \"pea\"), \"split\", \"pea\"))");
    }

    [Fact]
    public void Test_1_75_nk()
    {
        RunAll(2, (q, x) =>
            Conj(
                Equal("pea", x[1]),
                Equal("split", x[0]),
                Equal(x, q)
            )
        ).ShouldBe("((\"split\", \"pea\"))");
    }

    [Fact]
    public void Test_1_76_nk()
    {
        RunAll(2, (q, x) => Conj(
            Disj(
                Conj(Equal("split", x[0]), Equal("pea", x[1])),
                Conj(Equal("red", x[0]), Equal("bean", x[1]))
            ),
            Equal(q, x)
        )).ShouldBe("((\"split\", \"pea\"), (\"red\", \"bean\"))");
    }
    
    [Fact]
    public void Test_1_77a()
    {
        RunAll((r) =>
            Fresh((x, y) =>
                Conj(
                    Disj(
                        Conj(Equal("split", x), Equal("pea", y)),
                        Conj(Equal("red", x), Equal("bean", y))
                    ),
                    Equal(l(x, y, "soup"), r)
            ))
        ).ShouldBe("((\"split\", \"pea\", \"soup\"), (\"red\", \"bean\", \"soup\"))");
    }
    
    [Fact]
    public void Test_1_77b()
    {
        RunAll((r) =>
            Fresh(2, (x) =>
                Conj(
                    Disj(
                        Conj(Equal("split", x[0]), Equal("pea", x[1])),
                        Conj(Equal("red", x[0]), Equal("bean", x[1]))
                    ),
                    Equal(l(x[0], x[1], "soup"), r)
            ))
        ).ShouldBe("((\"split\", \"pea\", \"soup\"), (\"red\", \"bean\", \"soup\"))");
    }

    [Fact]
    public void Test_1_78a()
    {
        RunAll((r) =>
            Fresh((x, y) => Conj( // NOTE: Implicit Fresh conjunction presented in 1_78, is not possible in C#.
                Disj(
                    Conj(Equal("split", x), Equal("pea", y)),
                    Conj(Equal("red", x), Equal("bean", y))
                ),
                Equal(l(x, y, "soup"), r)
            ))
        ).ShouldBe("((\"split\", \"pea\", \"soup\"), (\"red\", \"bean\", \"soup\"))");
    }

    [Fact]
    public void Test_1_78b()
    {
        RunAll((r) =>
            Fresh(2, (x) => Conj( // NOTE: Implicit Fresh conjunction presented in 1_78, is not possible in C#.
                Disj(
                    Conj(Equal("split", x[0]), Equal("pea", x[1])),
                    Conj(Equal("red", x[0]), Equal("bean", x[1]))
                ),
                Equal(l(x[0], x[1], "soup"), r)
            ))
        ).ShouldBe("((\"split\", \"pea\", \"soup\"), (\"red\", \"bean\", \"soup\"))");
    }

    [Fact]
    public void Test_1_80_mk()
    {
        RunAll(3, (q, x) => Conj( // NOTE: Implicit Run conjunction presented in 1_80, is not possible in C#.
            Disj(
                Conj(Equal("split", x[0]), Equal("pea", x[1])),
                Conj(Equal("red", x[0]), Equal("bean", x[1]))
            ),
            Equal("soup", x[2]),
            Equal(q, x)
        )).ShouldBe("((\"split\", \"pea\", \"soup\"), (\"red\", \"bean\", \"soup\"))");
    }
}
