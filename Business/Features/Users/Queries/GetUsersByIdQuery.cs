using Business.Dtos.Outputs;
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
    private readonly IMapper _mapper;

    public GetUsersByIdQueryHander(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    public async Task<UsersOutputDto> Handle(GetUsersByIdQuery request, CancellationToken cancellationToken)
    {
        //NOTE: Queried each entity for flexibility purposes.

        var userEntity = await _unitOfWork.UsersRepository.GetByIdAsync(request.Id);
        var addressEntity = await _unitOfWork.AddressesRepository.GetAddressesByUserIdAsync(userEntity.Id);
        var employmentsEntities = await _unitOfWork.EmploymentsRepository.GetEmploymentsByUserIdAsync(userEntity.Id);

        var user = _mapper.Map<UsersOutputDto>(userEntity);
        user.Address = _mapper.Map<AddressesOutputDto>(addressEntity);
        user.Employments = _mapper.Map<IEnumerable<EmploymentsOutputDto>>(employmentsEntities);

        return user;
    }
}
