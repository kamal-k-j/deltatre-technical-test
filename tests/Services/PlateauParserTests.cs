using AutoFixture;
using Deltatre.MarsRovers.Services;
using FluentAssertions;
using Moq.AutoMock;

namespace Deltatre.MarsRovers.Tests.Services;

public class PlateauParserTests
{
    private readonly AutoMocker _mocker = new();
    private readonly Fixture _fixture = new();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void PlateauParser_Parse_WhenPassingNullOrEmptyPlateauDimensions_ShouldThrowException(string plateauDimensions)
    {
        // Given
        var subject = _mocker.CreateInstance<PlateauParser>();

        // When
        var exception = Assert.Throws<FormatException>(() => subject.Parse(plateauDimensions));

        // Then
        exception.Message.Should().Be("The plateau dimensions string cannot be null or empty.");
    }

    [Fact]
    public void PlateauParser_Parse_WhenPassingInvalidPlateauDimensionsLength_ShouldThrowException()
    {
        // Given
        var subject = _mocker.CreateInstance<PlateauParser>();

        // When
        var exception = Assert.Throws<FormatException>(() => subject.Parse($"{_fixture.Create<int>()} "));

        // Then
        exception.Message.Should().Be("Parsing failed: Invalid plateau dimensions string provided.");
    }

    [Fact]
    public void PlateauParser_Parse_WhenPassingInvalidXPlateauDimension_ShouldThrowException()
    {
        // Given
        var subject = _mocker.CreateInstance<PlateauParser>();

        // When
        var exception = Assert.Throws<FormatException>(() => subject.Parse($"x {_fixture.Create<int>()}"));

        // Then
        exception.Message.Should().Be("Parsing failed: The x value provided in the plateau dimensions string is not a valid int.");
    }

    [Fact]
    public void PlateauParser_Parse_WhenPassingInvalidYPlateauDimension_ShouldThrowException()
    {
        // Given
        var subject = _mocker.CreateInstance<PlateauParser>();

        // When
        var exception = Assert.Throws<FormatException>(() => subject.Parse($"{_fixture.Create<int>()} x"));

        // Then
        exception.Message.Should().Be("Parsing failed: The y value provided in the plateau dimensions string is not a valid int.");
    }

    [Fact]
    public void PlateauParser_Parse_WhenPassingValidPlateauDimensions_ShouldReturnThePlateauParsed()
    {
        // Given
        var subject = _mocker.CreateInstance<PlateauParser>();
        var x = _fixture.Create<int>();
        var y = _fixture.Create<int>();

        // When
        var result = subject.Parse($"{x} {y}");

        // Then
        result.X.Should().Be(x);
        result.Y.Should().Be(y);
    }
}