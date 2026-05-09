using Deltatre.MarsRovers.Services;
using FluentAssertions;
using Moq.AutoMock;

namespace Deltatre.MarsRovers.Tests.Services;

public class CommandParserTests
{
    private readonly AutoMocker _mocker = new();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void CommandParser_Parse_WhenPassingNullOrEmptyCommand_ShouldThrowException(string command)
    {
        // Given
        var subject = _mocker.CreateInstance<CommandParser>();

        // When
        var exception = Assert.Throws<FormatException>(() => subject.Parse(command));

        // Then
        exception.Message.Should().Be("The command string cannot be null or empty.");
    }

    [Fact]
    public void CommandParser_Parse_WhenPassingInvalidCommand_ShouldThrowException()
    {
        // Given
        var subject = _mocker.CreateInstance<CommandParser>();

        // When
        var exception = Assert.Throws<FormatException>(() => subject.Parse("LRMX"));

        // Then
        exception.Message.Should().Be("Parsing failed: The command can only assume the values L, R and M.");
    }

    [Fact]
    public void CommandParser_Parse_WhenPassingValidCommand_ShouldReturnTheCommandParsed()
    {
        // Given
        var subject = _mocker.CreateInstance<CommandParser>();

        // When
        var result = subject.Parse("lMlMLmLMMrrR");

        // Then
        result.Should().Be("LMLMLMLMMRRR");
    }
}