using FluentAssertions;
using static nk.RunnerModule;

namespace TowerOfHanoi.Tests;
using static TowerOfHanoi.RunnerModule;
using static TowerOfHanoi.RenderModule;


[Trait("Tower Solver", "")]
[Collection("RenderedTowerOfHanoi")]
public class Given_Height_1
{
    [Fact]
    public void ItResolvesAll()
    {
        int[][][] res = RunTowerOfHanoi(1).ToArray();
        
        res.Length.Should().Be(1);

        ("\n" + res[0].AsString(resetRenderCount: true)).Should().Be( "\n" +
            "  ║   ║   ║ \n" +
            "  ║   ║  ▇▇▇\n" +
            "══╩═══╩═══╩══ 1\n"
        );
    }
}

[Trait("Tower Solver", "")]
[Collection("RenderedTowerOfHanoi")]
public class Given_Height_2
{
    [Fact]
    public void ItResolvesAll()
    {
        int[][][] res = RunTowerOfHanoi(2).ToArray();
        
        res.Length.Should().Be(1);

        ("\n" + res[0].AsString(resetRenderCount: true)).Should().Be( "\n" +
            "   ║     ║     ║ \n" +
            "   ║     ║    ▇▇▇\n" +
            "   ║     ║   ▇▇▇▇▇\n" +
            "═══╩═════╩═════╩═══ 1\n"
        );
    }
}

[Trait("Tower Solver", "")]
[Collection("RenderedTowerOfHanoi")]
public class Given_Height_3
{
    [Fact]
    public void ItResolvesAll()
    {
        int[][][] res = RunTowerOfHanoi(3).ToArray();
        
        res.Length.Should().Be(1);

        ("\n" + res[0].AsString(resetRenderCount: true)).Should().Be( "\n" +
            "    ║       ║       ║ \n" +
            "    ║       ║      ▇▇▇\n" +
            "    ║       ║     ▇▇▇▇▇\n" +
            "    ║       ║    ▇▇▇▇▇▇▇\n" +
            "════╩═══════╩═══════╩═══ 1\n"
        );
    }
}

[Trait("Tower Solver", "")]
[Collection("RenderedTowerOfHanoi")]
public class Given_Height_4
{
    [Fact]
    public void ItResolvesAll()
    {
        int[][][] res = RunTowerOfHanoi(4).ToArray();
        
        res.Length.Should().Be(1);

        ("\n" + res[0].AsString(resetRenderCount: true)).Should().Be( "\n" +
            "     ║         ║         ║ \n" +
            "     ║         ║        ▇▇▇\n" +
            "     ║         ║       ▇▇▇▇▇\n" +
            "     ║         ║      ▇▇▇▇▇▇▇\n" +
            "     ║         ║     ▇▇▇▇▇▇▇▇▇\n" +
            "═════╩═════════╩═════════╩═════ 1\n"
        );
    }
}

[Trait("Tower Solver", "")]
[Collection("RenderedTowerOfHanoi")]
public class Given_Height_5
{
    [Fact]
    public void ItResolvesAll()
    {
        int[][][] res = RunTowerOfHanoi(5).ToArray();
        
        res.Length.Should().Be(1);

        ("\n" + res[0].AsString(resetRenderCount: true)).Should().Be( "\n" +
            "      ║           ║           ║ \n" +
            "      ║           ║          ▇▇▇\n" +
            "      ║           ║         ▇▇▇▇▇\n" +
            "      ║           ║        ▇▇▇▇▇▇▇\n" +
            "      ║           ║       ▇▇▇▇▇▇▇▇▇\n" +
            "      ║           ║      ▇▇▇▇▇▇▇▇▇▇▇\n" +
            "══════╩═══════════╩═══════════╩══════ 1\n"
        );
    }
}

[Trait("Tower Solver", "")]
[Collection("RenderedTowerOfHanoi")]
public class Given_Height_6
{
    [Fact]
    public void ItResolvesAll()
    {
        int[][][] res = RunTowerOfHanoi(6).ToArray();
        
        res.Length.Should().Be(1);

        ("\n" + res[0].AsString(resetRenderCount: true)).Should().Be( "\n" +
            "       ║             ║             ║ \n" +
            "       ║             ║            ▇▇▇\n" +
            "       ║             ║           ▇▇▇▇▇\n" +
            "       ║             ║          ▇▇▇▇▇▇▇\n" +
            "       ║             ║         ▇▇▇▇▇▇▇▇▇\n" +
            "       ║             ║        ▇▇▇▇▇▇▇▇▇▇▇\n" +
            "       ║             ║       ▇▇▇▇▇▇▇▇▇▇▇▇▇\n" +
            "═══════╩═════════════╩═════════════╩═══════ 1\n"
        );
    }
}

[Trait("Tower Solver", "")]
[Collection("RenderedTowerOfHanoi")]
public class Given_Height_7
{
    [Fact]
    public void ItResolvesAll()
    {
        int[][][] res = RunTowerOfHanoi(7).ToArray();
        
        res.Length.Should().Be(1);

        ("\n" + res[0].AsString(resetRenderCount: true)).Should().Be( "\n" +
            "        ║               ║               ║ \n" +
            "        ║               ║              ▇▇▇\n" +
            "        ║               ║             ▇▇▇▇▇\n" +
            "        ║               ║            ▇▇▇▇▇▇▇\n" +
            "        ║               ║           ▇▇▇▇▇▇▇▇▇\n" +
            "        ║               ║          ▇▇▇▇▇▇▇▇▇▇▇\n" +
            "        ║               ║         ▇▇▇▇▇▇▇▇▇▇▇▇▇\n" +
            "        ║               ║        ▇▇▇▇▇▇▇▇▇▇▇▇▇▇▇\n" +
            "════════╩═══════════════╩═══════════════╩════════ 1\n"
        );
    }
}

[Trait("Tower Solver", "")]
[Collection("RenderedTowerOfHanoi")]
public class Given_Height_8
{
    [Fact]
    public void ItResolvesAll()
    {
        int[][][] res = RunTowerOfHanoi(8).ToArray();
        
        res.Length.Should().Be(1);

        ("\n" + res[0].AsString(resetRenderCount: true)).Should().Be( "\n" +
            "         ║                 ║                 ║ \n" +
            "         ║                 ║                ▇▇▇\n" +
            "         ║                 ║               ▇▇▇▇▇\n" +
            "         ║                 ║              ▇▇▇▇▇▇▇\n" +
            "         ║                 ║             ▇▇▇▇▇▇▇▇▇\n" +
            "         ║                 ║            ▇▇▇▇▇▇▇▇▇▇▇\n" +
            "         ║                 ║           ▇▇▇▇▇▇▇▇▇▇▇▇▇\n" +
            "         ║                 ║          ▇▇▇▇▇▇▇▇▇▇▇▇▇▇▇\n" +
            "         ║                 ║         ▇▇▇▇▇▇▇▇▇▇▇▇▇▇▇▇▇\n" +
            "═════════╩═════════════════╩═════════════════╩═════════ 1\n"
        );
    }
}
