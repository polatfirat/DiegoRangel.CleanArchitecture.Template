using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DiegoRangel.CleanArchitecture.Domain.Common.UoW;
using DiegoRangel.CleanArchitecture.Domain.Example.Repositories;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Handlers;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Messages;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.NotificationPattern;

namespace DiegoRangel.CleanArchitecture.Domain.Example.Commands
{
    public class CreateExampleCommand : ICommandMapped<Example, int, Example>
    {
        public string Title { get; set; }
    }

    public class CreateExampleCommandHandler : 
        CommandHandlerBase<IApplicationUnitOfWork>, //Optional generic abstraction most used on writing operations
        ICommandHandler<CreateExampleCommand, Example>
    {
        private readonly IMapper _mapper;
        private readonly IExampleRepository _exampleRepository;

        public CreateExampleCommandHandler(
            NotificationContext notificationContext, 
            CommonMessages commonMessages,
            IMapper mapper,
            IApplicationUnitOfWork uow, 
            IExampleRepository exampleRepository) : base(notificationContext, commonMessages, uow)
        {
            _mapper = mapper;
            _exampleRepository = exampleRepository;
        }

        public async Task<Example> Handle(CreateExampleCommand request, CancellationToken cancellationToken)
        {
            var newExample = _mapper.Map<Example>(request);

            if (!IsValid(newExample)) return null;
            await _exampleRepository.AddAsync(newExample);
            await Commit();

            return newExample;
        }
    }
}