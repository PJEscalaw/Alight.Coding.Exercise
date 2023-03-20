using Business.Commons.Helpers;
using Business.Features.Users.Queries;
using FluentAssertions;
using Moq;

namespace Unit.Features.Users.Queries;

public class GetUsersByIdQueryValidatorTests
{
    private readonly Mock<IValidationHelper> _mockValidationHelper;
    private readonly GetUsersByIdQueryValidator _sut;
    public GetUsersByIdQueryValidatorTests()
    {
        _mockValidationHelper = new();
        _sut = new(_mockValidationHelper.Object);
    }

    [Test]
    public async Task ShouldPassValidation()
    {
        //arrange
        _mockValidationHelper.Setup(x => x
            .UserExistsAsync(It.IsAny<int>(), new CancellationToken()))
            .ReturnsAsync(true);

        //actual
        var result = await _sut.ValidateAsync(TestData
            .GetUsersByIdQuery(), new CancellationToken());

        //assert
        result.IsValid
            .Should()
            .BeTrue();
    }

    [Test]
    public async Task ShouldNotPassValidationWhenUserNotExists()
    {
        //arrange
        _mockValidationHelper.Setup(x => x
            .UserExistsAsync(It.IsAny<int>(), new CancellationToken()))
            .ReturnsAsync(false);

        //actual
        var result = await _sut.ValidateAsync(TestData
            .GetUsersByIdQuery(), new CancellationToken());

        //assert
        result.IsValid
            .Should()
            .BeFalse();
    }
}
