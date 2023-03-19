using Business.Dtos.Inputs;
using Business.Dtos.Inputs.Base;
using Business.Features.Users.Commands;
using Domain.Entities;
using FluentAssertions;
using MapsterMapper;
using Moq;
using Persistence.UnitOfWork;
using Serilog;

namespace Unit.Features.Users.Commands;

/// <summary>
/// _sut = System Under Test
/// </summary>
public class CreateUsersCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger> _mockLogger;
    private readonly CreateUsersCommandHandler _sut;

    public CreateUsersCommandHandlerTests()
    {
        _mockUnitOfWork = new();
        _mockMapper = new();
        _mockLogger = new();
        _sut = new(_mockUnitOfWork.Object, 
                    _mockMapper.Object, 
                    _mockLogger.Object);
    }

    [Test]
    public async Task ShouldCreateUsers()
    {
        //arrange
        _mockMapper.Setup(x => x
            .Map<UsersEntity>(It.IsAny<UsersInputBaseDto>()))
            .Returns(TestData.UsersEntity);

        _mockUnitOfWork.Setup(x => x.UsersRepository
            .AddAsync(It.IsAny<UsersEntity>()))
            .Returns(Task.CompletedTask);

        _mockMapper.Setup(x => x
            .Map<AddressesEntity>(It.IsAny<AddressesInputDto>()))
            .Returns(TestData.AddressesEntity);

        _mockUnitOfWork.Setup(x => x.AddressesRepository
            .AddAsync(It.IsAny<AddressesEntity>()))
            .Returns(Task.CompletedTask);

        _mockMapper.Setup(x => x
           .Map<IEnumerable<EmploymentsEntity>>(It.IsAny<IEnumerable<CreateEmploymentsInputDto>>()))
           .Returns(TestData.EmploymentsEntities);

        _mockUnitOfWork.Setup(x => x.EmploymentsRepository
            .AddRangeAsync(It.IsAny<IEnumerable<EmploymentsEntity>>()))
            .Returns(Task.CompletedTask);

        _mockUnitOfWork.Setup(x => x
            .CommitAsync())
            .ReturnsAsync(It.IsAny<int>());

        //actual
        var result = await _sut.Handle(TestData.CreateUsersCommand(), new CancellationToken());

        //assert
        result.Should().NotBe(0);
    }
}
