using RapidBlazor.Domain.ValueObject;
using RapidBlazor.Domain.Exceptions;

namespace RapidBlazor.Domain.UnitTests.ValueObjects;

public class ColourTests
{
    [Fact]
    public void ShouldReturnCorrectColourCode()
    {
        var code = "#FFFFFF";
        var colour = Colour.From(code);
        colour.Code.Should().Be(code);
    }

    [Fact]
    public void ToStringReturnsCode()
    {
        var colour = Colour.White;
        colour.ToString().Should().Be(colour.Code);
    }

    [Fact]
    public void ShouldPerformImplicitConversionToColourCodeString()
    {
        string code = Colour.White;

        code.Should().Be("#FFFFFF");
    }

    [Fact]
    public void ShouldPerformExplicitConversionGivenSupportedColorCode()
    {
        var color = (Colour)"#FFFFFF";

        color.Should().Be(Colour.White);
    }

    [Fact]
    public void ShouldThrowUnsupportedColourExceptionGivenNotSupportedColourCode()
    {
        FluentActions.Invoking(() => Colour.From("##FF33CC"))
            .Should().Throw<UnsupportedColourException>();
    }
}
