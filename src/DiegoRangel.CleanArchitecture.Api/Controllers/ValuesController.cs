using System.Threading.Tasks;
using DiegoRangel.CleanArchitecture.Api.ViewModels.Examples;
using DiegoRangel.CleanArchitecture.Domain.Example.Commands;
using DiegoRangel.DotNet.Framework.CQRS.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DiegoRangel.CleanArchitecture.Api.Controllers
{
    /// <summary>
    /// A simple example of a controller.
    /// Possible list of base controllers: ApiControllerBase, CrudApiController.
    /// </summary>
    [Route("api/values")]
    public class ValuesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GenerateKey()
        {
            var result = await Mediator.Send(new GenerateKeyCommand());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddExample([FromBody] CreateExampleCommand input)
        {
            var result = await Mediator.Send(input);
            return Ok(Mapper.Map<ExampleViewModel>(result));
        }
    }
}
