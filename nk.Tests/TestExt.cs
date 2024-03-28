
using FluentAssertions;

namespace nk.Tests;

internal static class TestExt
{
    public static void ShouldBe(this IEnumerator<object> stream, string expected)
    {
        stream.AsString().Should().Be(expected);
    }
}

