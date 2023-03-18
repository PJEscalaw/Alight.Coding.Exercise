using Business.Dtos.Outputs;
using Mapster;
using MapsterMapper;
using MediatR;
using Persistence.UnitOfWork;

namespace Business.Features.Users.Queries;

public class GetUsersByIdQuery : IRequest<UsersOutputDto>
{
    public int Id { get; set; }
}

public class GetUsersByIdQueryHander : IRequestHandler<GetUsersByIdQuery, UsersOutputDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUsersByIdQueryHander(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<UsersOutputDto> Handle(GetUsersByIdQuery request, CancellationToken cancellationToken)
    {
        //NOTE: Queried each entity for flexibility purposes.

        var userEntity = await _unitOfWork.UsersRepository.GetByIdAsync(request.Id);
        var addressEntity = await _unitOfWork.AddressesRepository.GetAddressesByUserIdAsync(userEntity.Id);
        var employmentsEntities = await _unitOfWork.EmploymentsRepository.GetEmploymentsByUserId(userEntity.Id);

        var userOutputDto = userEntity.Adapt<UsersOutputDto>();
        userOutputDto.Address = addressEntity.Adapt<AddressesOutputDto>();
        userOutputDto.Employments = employmentsEntities.Adapt<IEnumerable<EmploymentsOutputDto>>();

        return userOutputDto;
    }
}
