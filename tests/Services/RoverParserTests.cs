using AutoFixture;
using Deltatre.MarsRovers.Enums;
using Deltatre.MarsRovers.Services;
using FluentAssertions;
using Moq.AutoMock;

namespace Deltatre.MarsRovers.Tests.Services;

public class RoverParserTests
{
    private readonly AutoMocker _mocker = new();
    private readonly Fixture _fixture = new();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void RoverParser_Parse_WhenPassingNullOrEmptyRoverPosition_ShouldThrowException(string roverPosition)
    {
        // Given
        var subject = _mocker.CreateInstance<RoverParser>();

        // When
        var exception = Assert.Throws<FormatException>(() => subject.Parse(roverPosition));

        // Then
        exception.Message.Should().Be("The rover position string cannot be null or empty.");
    }

    [Fact]
    public void RoverParser_Parse_WhenPassingInvalidRoverPositionLength_ShouldThrowException()
    {
        // Given
        var subject = _mocker.CreateInstance<RoverParser>();

        // When
        var exception = Assert.Throws<FormatException>(() => subject.Parse($"{_fixture.Create<int>()} {_fixture.Create<int>()} "));

        // Then
        exception.Message.Should().Be("Parsing failed: Invalid rover position string provided.");
    }

    [Fact]
    public void RoverParser_Parse_WhenPassingInvalidXRoverPosition_ShouldThrowException()
    {
        // Given
        var subject = _mocker.CreateInstance<RoverParser>();

        // When
        var exception = Assert.Throws<FormatException>(() => subject.Parse($"x {_fixture.Create<int>()} {_fixture.Create<Heading>()}"));

        // Then
        exception.Message.Should().Be("Parsing failed: The x value provided in the rover position string is not a valid int.");
    }

    [Fact]
    public void RoverParser_Parse_WhenPassingInvalidYRoverPosition_ShouldThrowException()
    {
        // Given
        var subject = _mocker.CreateInstance<RoverParser>();

        // When
        var exception = Assert.Throws<FormatException>(() => subject.Parse($"{_fixture.Create<int>()} x {_fixture.Create<Heading>()}"));

        // Then
        exception.Message.Should().Be("Parsing failed: The y value provided in the rover position string is not a valid int.");
    }

    [Fact]
    public void RoverParser_Parse_WhenPassingInvalidHeadingRoverPosition_ShouldThrowException()
    {
        // Given
        var subject = _mocker.CreateInstance<RoverParser>();

        // When
        var exception = Assert.Throws<FormatException>(() => subject.Parse($"{_fixture.Create<int>()} {_fixture.Create<int>()} Z"));

        // Then
        exception.Message.Should().Be("Parsing failed: The heading value provided in the rover position string is not a valid enum value.");
    }

    [Fact]
    public void RoverParser_Parse_WhenPassingValidRoverPosition_ShouldReturnTheRoverParsed()
    {
        // Given
        var subject = _mocker.CreateInstance<RoverParser>();
        var x = _fixture.Create<int>();
        var y = _fixture.Create<int>();
        var heading = _fixture.Create<Heading>();

        // When
        var result = subject.Parse($"{x} {y} {heading}");

        // Then
        result.X.Should().Be(x);
        result.Y.Should().Be(y);
        result.Heading.Should().Be(heading);
    }
}