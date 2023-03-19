using Business.Commons.Helpers;
using Business.Features.Users.Commands;
using FluentAssertions;
using Moq;
using System.Security.Cryptography.X509Certificates;

namespace Unit.Features.Users.Commands;

/// <summary>
/// _sut = System Unit Test.
/// </summary>
public class CreateUsersCommandValidatorTests
{
    private readonly Mock<IValidationHelper> _mockValidationHelper;
    private readonly CreateUsersCommandValidator _sut;

    public CreateUsersCommandValidatorTests()
    {
        _mockValidationHelper = new();
        _sut = new(_mockValidationHelper.Object);
    }

    [Test]
    public async Task ShouldPassValidation()
    {
        _mockValidationHelper.Setup(x => x
            .EmailNotExistsAsync(It.IsAny<string>(), new CancellationToken()))
            .ReturnsAsync(true);

        var result = await _sut.ValidateAsync(TestData.CreateUsersCommand(), new CancellationToken());

        result.IsValid.Should().BeTrue();
    }
}
