using Business.Dtos.Inputs;
using Domain.Entities;
using Mapster;

namespace Business.Features.Users.Commands;

internal class CreateUsersCommandMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UsersInputDto, UsersEntity>();
        config.NewConfig<AddressesInputDto, AddressesEntity>();
        config.NewConfig<EmploymentsInputDto, EmploymentsEntity>();
    }
}
