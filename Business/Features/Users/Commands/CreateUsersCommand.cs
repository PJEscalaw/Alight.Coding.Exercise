using Business.Dtos.Inputs;
using Domain.Entities;
using MapsterMapper;
using MediatR;
using Persistence.UnitOfWork;
using Serilog;

namespace Business.Features.Users.Commands;

public class CreateUsersCommand : IRequest<int>
{
    public UsersInputDto UsersInputDto { get; set; }
}

public class CreateUsersCommandHandler : IRequestHandler<CreateUsersCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public CreateUsersCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<int> Handle(CreateUsersCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userToCreate = await PeristUserAsync(request.UsersInputDto);
            await PersistAddressAsync(request.UsersInputDto.Address, userToCreate.Id);
            await PersistEmploymentsAsync(request.UsersInputDto.Employments, userToCreate.Id);

            _ = await _unitOfWork.CommitAsync();

            return userToCreate.Id;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackAsync();
            _logger.Error(e.Message, e);

            throw;
        }
    }

    private async Task PersistEmploymentsAsync(IEnumerable<EmploymentsInputDto> employmentsInputDtos, int userId)
    {
        var employmentsToCreate = _mapper.Map<List<EmploymentsEntity>>(employmentsInputDtos);
        employmentsToCreate.ForEach(employment => employment.UserId = userId);
        await _unitOfWork.EmploymentsRepository.AddRangeAsync(employmentsToCreate);
    }

    private async Task PersistAddressAsync(AddressesInputDto addressesInputDto, int userId)
    {
        var addressToCreate = _mapper.Map<AddressesEntity>(addressesInputDto);
        addressToCreate.UserId = userId;
        await _unitOfWork.AddressesRepository.AddAsync(addressToCreate);
    }

    private async Task<UsersEntity> PeristUserAsync(UsersInputDto usersInputDto)
    {
        var userToCreate = _mapper.Map<UsersEntity>(usersInputDto);
        await _unitOfWork.UsersRepository.AddAsync(userToCreate);

        _ = await _unitOfWork.CommitAsync();
        return userToCreate;
    }
}
