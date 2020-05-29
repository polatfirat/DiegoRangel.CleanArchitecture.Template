using AutoMapper;
using DiegoRangel.DotNet.Framework.CQRS.API.Mapper;

namespace DiegoRangel.CleanArchitecture.Api.Mapper
{
    public class AppProfile : Profile
    {

        public AppProfile()
        {
            var _assemblies = new[]
            {
                typeof(Startup).Assembly,
                typeof(Domain.Common.ProjectIdentifier).Assembly,
            };

            this.ApplyViewModelMappings(_assemblies);
            this.ApplyAutoMappings(_assemblies);
        }
    }
}