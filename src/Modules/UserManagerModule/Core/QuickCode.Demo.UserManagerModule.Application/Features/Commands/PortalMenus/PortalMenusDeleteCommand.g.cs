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
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class PortalMenusDeleteCommand : IRequest<Response<bool>>
    {
        public PortalMenusDto request { get; set; }

        public PortalMenusDeleteCommand(PortalMenusDto request)
        {
            this.request = request;
        }

        public class PortalMenusDeleteHandler : IRequestHandler<PortalMenusDeleteCommand, Response<bool>>
        {
            private readonly ILogger<PortalMenusDeleteHandler> _logger;
            private readonly IPortalMenusRepository _repository;
            public PortalMenusDeleteHandler(ILogger<PortalMenusDeleteHandler> logger, IPortalMenusRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(PortalMenusDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}