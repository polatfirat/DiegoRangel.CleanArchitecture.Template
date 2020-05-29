using System;
using System.Threading;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Handlers;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR;

namespace DiegoRangel.CleanArchitecture.Domain.Example.Commands
{
    public class GenerateKeyCommand : ICommand<Guid>
    {
        
    }

    public class GenerateKeyCommandHandler : ICommandHandler<GenerateKeyCommand, Guid>
    {
        public Task<Guid> Handle(GenerateKeyCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Guid.NewGuid());
        }
    }
}