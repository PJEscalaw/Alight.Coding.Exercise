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
            var userToCreate = _mapper.Map<UsersEntity>(request.UsersInputDto);
            await _unitOfWork.UsersRepository.AddAsync(userToCreate);

            var addressToCreate = _mapper.Map<AddressesEntity>(request.UsersInputDto.Address);
            addressToCreate.UserId = userToCreate.Id;
            await _unitOfWork.AddressesRepository.AddAsync(addressToCreate);

            var employmentsToCreate = _mapper.Map<List<EmploymentsEntity>>(request.UsersInputDto.Employments);
            employmentsToCreate.ForEach(employment => employment.UserId = userToCreate.Id);
            await _unitOfWork.EmploymentsRepository.AddRangeAsync(employmentsToCreate);

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
}
