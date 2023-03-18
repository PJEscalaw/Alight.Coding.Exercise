using Business.Dtos.Outputs;
using Domain.Entities;
using Mapster;

namespace Business.Features.Users.Queries;

public class GetUsersByIdQueryMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        _ = config.NewConfig<UsersEntity, UsersOutputDto>();
        _ = config.NewConfig<AddressesEntity, AddressesOutputDto>();
        _ = config.NewConfig<EmploymentsEntity, EmploymentsOutputDto>();
    }
}
