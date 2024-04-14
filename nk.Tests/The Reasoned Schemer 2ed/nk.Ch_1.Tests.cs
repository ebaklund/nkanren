using Xunit;
using Xunit.Abstractions;
using FluentAssertions;

namespace nk.Tests;

using static nk.RunnerModule;
using static nk.GoalsModule;
using static nk.ListConstructor;

[Trait("The Reasoned Schemer 2nd ed", "Playthings")]
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
        ).AsString().Should().Be("()");
    }

    [Fact]
    public void Test_1_10() // p 3
    {
        RunAll((q) => 
            Equal("pea", "pod")
        ).AsString().Should().Be("()");
    }

    [Fact]
    public void Test_1_11() // p 4
    {
        RunAll((q) =>
            Equal(q, "pea")
        ).AsString().Should().Be("(\"pea\")");
    }

    [Fact]
    public void Test_1_12() // p 4
    {
        RunAll((q) =>
            Equal("pea", q)
        ).AsString().Should().Be("(\"pea\")");
    }

    [Fact]
    public void Test_1_17() // p 6
    {
        RunAll((q) =>
            Succ()
        ).AsString().Should().Be("(_0)");
    }

    [Fact]
    public void Test_1_19() // p 6
    {
        RunAll((q) =>
            Equal("pea", "pea")
        ).AsString().Should().Be("(_0)");
    }
    
    [Fact]
    public void Test_1_20() // p 6
    {
        RunAll((q) =>
            Equal(q, q)
        ).AsString().Should().Be("(_0)");
    }

    [Fact]
    public void Test_1_24a()
    {
        RunAll((q) =>
            Fresh((x) =>
                Equal("pea", x)
            )
        ).AsString().Should().Be("(_0)");
    }

    [Fact]
    public void Test_1_24b()
    {
        RunAll((q) =>
            Fresh(1, (x) =>
                Equal("pea", x[0])
            )
        ).AsString().Should().Be("(_0)");
    }

    [Fact]
    public void Test_1_31a()
    {
        RunAll((q) =>
            Fresh((x) =>
                Equal(q, x)
        )).AsString().Should().Be("(_1)");
    }

    [Fact]
    public void Test_1_31b()
    {
        RunAll((q) =>
            Fresh(1, (x) =>
                Equal(q, x[0])
        )).AsString().Should().Be("(_1)");
    }

    [Fact]
    public void Test_1_32()
    {
        RunAll((q) =>
            Equal(l(l(l("pea")), "pod"), l(l(l("pea")), "pod"))
        ).AsString().Should().Be("(_0)");
    }

    [Fact]
    public void Test_1_33()
    {
        RunAll((q) =>
            Equal(l(l(l("pea")), "pod"), l(l(l("pea")), q))
        ).AsString().Should().Be("(\"pod\")");
    }

    [Fact]
    public void Test_1_34()
    {
        RunAll((q) =>
            Equal(l(l(l(q)), "pod"), l(l(l("pea")), "pod"))
        ).AsString().Should().Be("(\"pea\")");
    }

    [Fact]
    public void Test_1_35a()
    {
        RunAll((q) => 
            Fresh((x) =>
                Equal(l(l(l(q)), "pod"), l(l(l(x)), "pod"))
        )).AsString().Should().Be("(_1)");
    }
    
    [Fact]
    public void Test_1_35b()
    {
        RunAll((q) => 
            Fresh(1, (x) =>
                Equal(l(l(l(q)), "pod"), l(l(l(x[0])), "pod"))
        )).AsString().Should().Be("(_1)");
    }

    [Fact]
    public void Test_1_36a()
    {
        RunAll((q) =>
            Fresh((x) =>
                Equal(l(l(l(q)), x), l(l(l(x)), "pod"))
        )).AsString().Should().Be("(\"pod\")");
    }

    [Fact]
    public void Test_1_36b()
    {
        RunAll((q) =>
            Fresh(1, (x) =>
                Equal(l(l(l(q)), x[0]), l(l(l(x[0])), "pod"))
        )).AsString().Should().Be("(\"pod\")");
    }

    [Fact]
    public void Test_1_37a()
    {
        RunAll((q) =>
            Fresh((x) =>
                Equal(l(x, x), q)
        )).AsString().Should().Be("((_1, _1))");
    }

    [Fact]
    public void Test_1_37b()
    {
        RunAll((q) =>
            Fresh(1, (x) =>
                Equal(l(x[0], x[0]), q)
        )).AsString().Should().Be("((_1, _1))");
    }
    
    [Fact]
    public void Test_1_38a()
    {
        RunAll((q) =>
            Fresh((x) =>
                Fresh((y) =>
                    Equal(l(q, y), l(l(x, y), x))
        ))).AsString().Should().Be("((_1, _1))");
    }

    [Fact]
    public void Test_1_38b()
    {
        RunAll((q) =>
            Fresh(2, (x) =>
                Equal(l(q, x[1]), l(l(x[0], x[1]), x[0]))
        )).AsString().Should().Be("((_1, _1))");
    }

    [Fact]
    public void Test_1_41a()
    {
        RunAll((q) => 
            Fresh((x) =>
                Fresh((y) =>
                    Equal(l(x, y), q)
        ))).AsString().Should().Be("((_1, _2))");
    }
    
    [Fact]
    public void Test_1_41b()
    {
        RunAll((q) => 
            Fresh(2, (x) =>
                Equal(l(x[0], x[1]), q)
        )).AsString().Should().Be("((_1, _2))");
    }

    [Fact]
    public void Test_1_42a()
    {
        RunAll((s) => 
            Fresh((t) =>
                Fresh((u) =>
                    Equal(l(t, u), s)
        ))).AsString().Should().Be("((_1, _2))");
    }

    [Fact]
    public void Test_1_42b()
    {
        RunAll((s) => 
            Fresh(2, (t) =>
                Equal(l(t[0], t[1]), s)
        )).AsString().Should().Be("((_1, _2))");
    }

    [Fact]
    public void Test_1_43a()
    {
        RunAll((q) => 
            Fresh((x) =>
                Fresh((y) =>
                    Equal(l(x, y, x), q)
        ))).AsString().Should().Be("((_1, _2, _1))");
    }

    [Fact]
    public void Test_1_43b()
    {
        RunAll((q) => 
            Fresh(2, (x) =>
                Equal(l(x[0], x[1], x[0]), q)
        )).AsString().Should().Be("((_1, _2, _1))");
    }

    [Fact]
    public void Test_1_50()
    {
        RunAll((q) => 
            Conj(Succ(), Succ())
        ).AsString().Should().Be("(_0)");
    }

    [Fact]
    public void Test_1_51()
    {
        RunAll((q) => 
            Conj(Succ(), Equal("corn", q))
        ).AsString().Should().Be("(\"corn\")");
    }

    [Fact]
    public void Test_1_52()
    {
        RunAll((q) => 
            Conj(Fail(), Equal("corn", q))
        ).AsString().Should().Be("()");
    }

    [Fact]
    public void Test_1_53()
    {
        RunAll((q) => 
            Conj(Equal("corn", q), Equal("meal", q))
        ).AsString().Should().Be("()");
    }

    [Fact]
    public void Test_1_54()
    {
        RunAll((q) => 
            Conj(Equal("corn", q), Equal("corn", q))
        ).AsString().Should().Be("(\"corn\")");
    }

    [Fact]
    public void Test_1_55()
    {
        RunAll((q) => 
            Conj(Fail(), Fail())
        ).AsString().Should().Be("()");
    }

    [Fact]
    public void Test_1_56()
    {
        RunAll((q) => 
            Disj(Equal("olive", q), Fail())
        ).AsString().Should().Be("(\"olive\")");
    }

    [Fact]
    public void Test_1_57()
    {
        RunAll((q) => 
            Disj(Fail(), Equal("oil", q))
        ).AsString().Should().Be("(\"oil\")");
    }

    [Fact]
    public void Test_1_58()
    {
        RunAll((q) => 
            Disj(Equal("olive", q), Equal("oil", q))
        ).AsString().Should().Be("(\"olive\", \"oil\")");
    }

    [Fact]
    public void Test_1_59a()
    {
        RunAll((q) =>
            Fresh((x) =>
                Fresh((y) =>
                    Disj(Equal(l(x, y), q), Equal(l(y, x), q))
        ))).AsString().Should().Be("((_1, _2), (_2, _1))");
    }

    [Fact]
    public void Test_1_59b()
    {
        RunAll((q) =>
            Fresh(2, (x) =>
                Disj(Equal(l(x[0], x[1]), q), Equal(l(x[1], x[0]), q))
        )).AsString().Should().Be("((_1, _2), (_2, _1))");
    }

    [Fact]
    public void Test_1_61a()
    {
        RunAll((x) =>
            Disj(Equal("olive", x), Equal("oil" ,x))
        ).AsString().Should().Be("(\"olive\", \"oil\")");
    }

    [Fact]
    public void Test_1_61b()
    {
        RunAll((x) =>
            Disj(Equal("oil", x), Equal("olive" ,x))
        ).AsString().Should().Be("(\"oil\", \"olive\")");
    }

    [Fact]
    public void Test_1_62()
    {
        RunAll((x) =>
            Disj(
                Conj(Equal("olive", x), Fail()),
                Equal("oil" ,x)
        )).AsString().Should().Be("(\"oil\")");
    }

    [Fact]
    public void Test_1_63()
    {
        RunAll((x) =>
            Disj(
                Conj(Equal("olive", x), Succ()),
                Equal("oil" ,x)
        )).AsString().Should().Be("(\"olive\", \"oil\")");
    }

    [Fact]
    public void Test_1_64()
    {
        RunAll((x) =>
            Disj(
                Equal("oil" ,x),
                Conj(Equal("olive", x), Succ())
            )
        ).AsString().Should().Be("(\"oil\", \"olive\")");
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
        ).AsString().Should().Be("(\"olive\", _0, \"oil\")");
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
        ).AsString().Should().Be("((\"split\", \"pea\"))");
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
        ).AsString().Should().Be("((\"split\", \"pea\"))");
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
        ).AsString().Should().Be("((\"split\", \"pea\"))");
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
        ).AsString().Should().Be("((\"split\", \"pea\"))");
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
        ).AsString().Should().Be("(((\"split\", \"pea\"), \"split\", \"pea\"))");
    }

    [Fact]
    public void Test_1_75()
    {
        RunAll((x, y) =>
            Conj(
                Equal("split", x),
                Equal("pea", y)
            )
        ).AsString().Should().Be("((\"split\", \"pea\"))");
    }

    [Fact]
    public void Test_1_76()
    {
        RunAll((x, y) => Conj(
            Disj(
                Conj(Equal("split", x), Equal("pea", y)),
                Conj(Equal("red", x), Equal("bean", y))
            )
        )).AsString().Should().Be("((\"split\", \"pea\"), (\"red\", \"bean\"))");
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
        ).AsString().Should().Be("((\"split\", \"pea\", \"soup\"), (\"red\", \"bean\", \"soup\"))");
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
        ).AsString().Should().Be("((\"split\", \"pea\", \"soup\"), (\"red\", \"bean\", \"soup\"))");
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
        ).AsString().Should().Be("((\"split\", \"pea\", \"soup\"), (\"red\", \"bean\", \"soup\"))");
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
        ).AsString().Should().Be("((\"split\", \"pea\", \"soup\"), (\"red\", \"bean\", \"soup\"))");
    }

    [Fact]
    public void Test_1_80()
    {
        RunAll((x, y, z) => Conj( // NOTE: Implicit Run conjunction presented in 1_80, is not possible in C#.
            Disj(
                Conj(Equal("split", x), Equal("pea", y)),
                Conj(Equal("red", x), Equal("bean", y))
            ),
            Equal("soup", z)
        )).AsString().Should().Be("((\"split\", \"pea\", \"soup\"), (\"red\", \"bean\", \"soup\"))");
    }

    [Fact]
    public void Test_1_81()
    {
        RunAll((x, y) => Conj( // NOTE: Implicit Run conjunction presented in 1_80, is not possible in C#.
            Equal("split", x),
            Equal("pea", y)
        )).AsString().Should().Be("((\"split\", \"pea\"))");
    }
}
