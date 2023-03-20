using Business.Features.Users.Commands;
using FluentAssertions;
using Moq;
using Persistence.UnitOfWork;
using Serilog;

namespace Unit.Features.Users.Commands;

public class UpdateUsersCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<ILogger> _mockLogger;
    private readonly UpdateUsersCommandHandler _sut;
    public UpdateUsersCommandHandlerTests()
    {
        _mockUnitOfWork = new();
        _mockLogger = new();
        _sut = new(_mockUnitOfWork.Object,
                    _mockLogger.Object);
    }

    [Test]
    public async Task ShouldUpdateUsers()
    {
        //arrange
        _mockUnitOfWork.Setup(x => x.UsersRepository
            .GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(TestData.UsersEntity);

        _mockUnitOfWork.Setup(x => x.AddressesRepository
            .GetAddressesByUserIdAsync(It.IsAny<int>()))
            .ReturnsAsync(TestData.AddressesEntity);

        _mockUnitOfWork.Setup(x => x.EmploymentsRepository
            .GetEmploymentsByIdAndUserIdAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(TestData.EmploymentsEntities().FirstOrDefault());

        //actual
        var result = await _sut.Handle(TestData
            .UpdateUsersCommand(), new CancellationToken());

        //assert
        result.Should()
            .NotBe(0);
    }
}
