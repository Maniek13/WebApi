using Abstractions.DbContexts;
using MediatR;

namespace Application.Commands.Api;

[Shared.Atributes.SaveDbContextAttribute(typeof(AbstractAppDbContext))]
public class SetUserAddressCommand : IRequest
{
    public string UserId { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? ZipCode { get; set; }
}
