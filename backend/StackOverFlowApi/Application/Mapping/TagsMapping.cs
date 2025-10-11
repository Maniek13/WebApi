using Contracts.Dtos;
using Domain.Entities;
using Mapster;

namespace Application.Mapping;

public class TagsMapping : IRegister
{
    public void Register(TypeAdapterConfig cfg)
    {
        cfg.NewConfig<Tag, TagDto>()
            .MapWith(el => new TagDto(
                    el.Name,
                    el.Count
                ));

    }
}
