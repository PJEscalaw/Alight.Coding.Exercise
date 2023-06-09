﻿using Business.Commons.Helpers;
using Business.Features.Users.Commands;
using FluentAssertions;
using Moq;

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
        //arrange
        _mockValidationHelper.Setup(x => x
            .EmailNotExistsAsync(It.IsAny<string>(), new CancellationToken()))
            .ReturnsAsync(true);

        //actual
        var result = await _sut.ValidateAsync(TestData
            .CreateUsersCommand(), new CancellationToken());

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
            .CreateUsersCommand(), new CancellationToken());

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
            .CreateUsersCommandInvalid(), new CancellationToken());

        result.IsValid
            .Should()
            .BeFalse();
    }
}
