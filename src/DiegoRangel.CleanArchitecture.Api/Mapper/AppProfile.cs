using System.Reflection;
using DiegoRangel.DotNet.Framework.CQRS.API.Mapper;

namespace DiegoRangel.CleanArchitecture.Api.Mapper
{
    public class AppProfile : ProfileBase
    {
        protected override Assembly[] GetAssembliesForAutomation()
        {
            return new[]
            {
                typeof(Startup).Assembly,
                typeof(Domain.Common.ProjectIdentifier).Assembly,
            };
        }
    }
}