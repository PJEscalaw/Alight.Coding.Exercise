using Business.Dtos.Inputs;
using Business.Dtos.Inputs.Base;
using Domain.Entities;
using Mapster;

namespace Business.Features.Users.Commands;

internal class CreateUsersCommandMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UsersInputBaseDto, UsersEntity>();
        config.NewConfig<AddressesInputDto, AddressesEntity>();
        config.NewConfig<CreateEmploymentsInputDto, EmploymentsEntity>();
    }
}
