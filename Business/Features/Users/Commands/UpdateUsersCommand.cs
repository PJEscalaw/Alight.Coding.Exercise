using Business.Commons.Exceptions;
using Business.Dtos.Inputs;
using Domain.Entities;
using Mapster;
using MediatR;
using Persistence.UnitOfWork;
using Serilog;

namespace Business.Features.Users.Commands;

public class UpdateUsersCommand : IRequest<int>
{
    public UpdateUsersInputDto UsersInputDto { get; set; }
}

public class UpdateUsersCommandHandler : IRequestHandler<UpdateUsersCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    public UpdateUsersCommandHandler(
        IUnitOfWork unitOfWork, 
        ILogger logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<int> Handle(UpdateUsersCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userEntity = await UpdateUserAsync(request);
            await UpdateAddressAsync(request.UsersInputDto.Address, userEntity.Id);
            await UpdateEmploymentsAsync(request.UsersInputDto.Employments, userEntity.Id);

            await _unitOfWork.CommitAsync();

            return userEntity.Id;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackAsync();
            _logger.Error(e.Message, e);

            throw;
        }
    }

    private async Task UpdateEmploymentsAsync(IEnumerable<UpdateEmploymentsInputDto> employments, int userId)
    {
        var employmentsToUpdate = new List<EmploymentsEntity>();
        foreach (var employment in employments)
        {
            var employmentEntity = await _unitOfWork.EmploymentsRepository.GetEmploymentsByIdAndUserIdAsync(employment.Id, userId) 
                ?? throw new NotFoundException($"No employment found using employment id '{employment.Id}' and userid '{userId}'.");
            employment.Adapt(employmentEntity);
            employmentsToUpdate.Add(employmentEntity);
        }
    }

    private async Task UpdateAddressAsync(AddressesInputDto address, int userId)
    {
        var addressEntity = await _unitOfWork.AddressesRepository.GetAddressesByUserIdAsync(userId);
        address.Adapt(addressEntity);
    }

    private async Task<UsersEntity> UpdateUserAsync(UpdateUsersCommand request)
    {
        var userEntity = await _unitOfWork.UsersRepository.GetByIdAsync(request.UsersInputDto.UserId);
        request.UsersInputDto.Adapt(userEntity);

        return userEntity;
    }
}
