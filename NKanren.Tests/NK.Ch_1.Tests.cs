
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;

namespace nkanren.Tests;
using static nkanren.Runners;
using static nkanren.Goals;

public class Paythings
{
    protected readonly ITestOutputHelper _output;

    public Paythings(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Test_1_7() // p 3
    {
        var st = Run((Key q) => Fail());
        _output.WriteLine($"result: {st.ToString()}");

        st.Count.Should().Be(0);
    }
}
