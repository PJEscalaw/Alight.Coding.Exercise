using Business.Dtos.Outputs;
using Business.Features.Users.Queries;
using Domain.Entities;
using FluentAssertions;
using MapsterMapper;
using Moq;
using Persistence.UnitOfWork;

namespace Unit.Features.Users.Queries;

public class GetUsersByIdQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetUsersByIdQueryHandler _sut;
    public GetUsersByIdQueryHandlerTests()
    {
        _mockUnitOfWork = new();
        _mockMapper = new();
        _sut = new(_mockUnitOfWork.Object, 
                   _mockMapper.Object);
    }

    [Test]
    public async Task ShouldGetUserById()
    {
        //arrange
        _mockUnitOfWork.Setup(x => x.UsersRepository
            .GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(TestData.UsersEntity);

        _mockUnitOfWork.Setup(x => x.AddressesRepository
            .GetAddressesByUserIdAsync(It.IsAny<int>()))
            .ReturnsAsync(TestData.AddressesEntity);

        _mockUnitOfWork.Setup(x => x.EmploymentsRepository
            .GetEmploymentsByUserIdAsync(It.IsAny<int>()))
            .ReturnsAsync(TestData.EmploymentsEntities);

        _mockMapper.Setup(x => x
            .Map<UsersOutputDto>(It.IsAny<UsersEntity>()))
            .Returns(TestData.UsersOutputDto);

        _mockMapper.Setup(x => x
            .Map<AddressesOutputDto>(It.IsAny<AddressesEntity>()))
            .Returns(TestData.AddressesOutputDto);

        _mockMapper.Setup(x => x
            .Map<List<EmploymentsOutputDto>>(It.IsAny<List<EmploymentsEntity>>()))
            .Returns(TestData.EmploymentsOutputDto);

        //actual
        var result = await _sut.Handle(new GetUsersByIdQuery { Id = It.IsAny<int>() }, new CancellationToken());

        //assert
        result.Should()
            .NotBeNull();
        result.Should()
            .BeOfType<UsersOutputDto>();
    }
}
