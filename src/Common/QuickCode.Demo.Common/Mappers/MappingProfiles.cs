using QuickCode.Demo.Common.Extensions;
using QuickCode.Demo.Common.Models;
using AutoMapper;

namespace QuickCode.Demo.Common.Mappers;

public class MappingProfiles : Profile
{
    public MappingProfiles(AppDomain currentDomain)
    {
        CreateMap(typeof(RepoResponse<>), typeof(Response<>));
        AddDtoMappers(currentDomain);
    }

    private void AddDtoMappers(AppDomain currentDomain)
    {
        var dtoTypes = currentDomain.GetAssemblyTypes("Application", "Dtos");

        var entityTypes = currentDomain.GetAssemblyTypes("Domain", "Entities");

        foreach (var dtoType in dtoTypes)
        {
            var entityType = entityTypes.FirstOrDefault(i => i.Name == $"{dtoType.Name.Replace("Dto", string.Empty)}");
            if (entityType == null) continue;
            CreateMap(dtoType, entityType);
            CreateMap(entityType, dtoType);
        }
    }
}
