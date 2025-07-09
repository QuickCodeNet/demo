using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class PortalMenusInsertCommand : IRequest<Response<PortalMenusDto>>
    {
        public PortalMenusDto request { get; set; }

        public PortalMenusInsertCommand(PortalMenusDto request)
        {
            this.request = request;
        }

        public class PortalMenusInsertHandler : IRequestHandler<PortalMenusInsertCommand, Response<PortalMenusDto>>
        {
            private readonly ILogger<PortalMenusInsertHandler> _logger;
            private readonly IPortalMenusRepository _repository;
            public PortalMenusInsertHandler(ILogger<PortalMenusInsertHandler> logger, IPortalMenusRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PortalMenusDto>> Handle(PortalMenusInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}