using Business.Commons.Exceptions;
using MediatR;
using Persistence.UnitOfWork;
using Serilog;

namespace Business.Features.Employments.Commands;

public class DeleteEmploymentsCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class DeleteEmploymentsCommandHandler : IRequestHandler<DeleteEmploymentsCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    public DeleteEmploymentsCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<bool> Handle(DeleteEmploymentsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var employmentToDelete = await _unitOfWork.EmploymentsRepository.GetByIdAsync(request.Id) 
                ?? throw new NotFoundException($"Employment Id '{request.Id}' not found.");
            _unitOfWork.EmploymentsRepository.Delete(employmentToDelete);

            return await _unitOfWork.CommitAsync() > 0;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackAsync();
            _logger.Error(e.Message, e);

            throw;
        }
    }
}
