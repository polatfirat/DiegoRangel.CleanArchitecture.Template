using DiegoRangel.CleanArchitecture.Domain.Example;
using DiegoRangel.DotNet.Framework.CQRS.API.Mapper;

namespace DiegoRangel.CleanArchitecture.Api.ViewModels.Examples
{
    public class ExampleViewModel : IViewModel<Example>
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}