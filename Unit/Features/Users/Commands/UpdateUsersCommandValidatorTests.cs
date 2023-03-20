using Business.Commons.Helpers;
using Business.Features.Users.Commands;
using FluentAssertions;
using FluentValidation;
using Moq;

namespace Unit.Features.Users.Commands;

public class UpdateUsersCommandValidatorTests
{
    private readonly Mock<IValidationHelper> _mockValidationHelper;
    private readonly UpdateUsersCommandValidator _sut;
    public UpdateUsersCommandValidatorTests()
    {
        _mockValidationHelper = new();
        _sut = new(_mockValidationHelper.Object);
    }

    [Test]
    public async Task ShouldPassValidation()
    {
        //arrange
        _mockValidationHelper.Setup(x => x
            .EmailNotExistsAsync(It.IsAny<string>(), new CancellationToken()))
            .ReturnsAsync(true);

        _mockValidationHelper.Setup(x => x
            .UserExistsAsync(It.IsAny<int>(), new CancellationToken()))
            .ReturnsAsync(true);

        //actual
        var result = await _sut.ValidateAsync(TestData
            .UpdateUsersCommand(), new CancellationToken());

        //assert
        result.IsValid
            .Should()
            .BeTrue();
    }

    [Test]
    public async Task ShouldNotPassValidationWhenEmailExists()
    {
        _mockValidationHelper.Setup(x => x
            .EmailNotExistsAsync(It.IsAny<string>(), new CancellationToken()))
            .ReturnsAsync(false);

        var result = await _sut.ValidateAsync(TestData
            .UpdateUsersCommand(), new CancellationToken());

        result.IsValid
            .Should()
            .BeFalse();
    }

    [Test]
    public async Task ShouldNotPassValidationWhenRequiredFieldsAreNotMet()
    {
        _mockValidationHelper.Setup(x => x
            .EmailNotExistsAsync(It.IsAny<string>(), new CancellationToken()))
            .ReturnsAsync(false);

        var result = await _sut.ValidateAsync(TestData
            .UpdateUsersCommandInvalid(), new CancellationToken());

        result.IsValid
            .Should()
            .BeFalse();
    }
}
