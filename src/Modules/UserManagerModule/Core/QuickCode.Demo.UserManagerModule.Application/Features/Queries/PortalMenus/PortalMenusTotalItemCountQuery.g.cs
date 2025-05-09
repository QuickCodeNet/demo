using System.Linq;
using MediatR;
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
    public class PortalMenusTotalItemCountQuery : IRequest<Response<int>>
    {
        public PortalMenusTotalItemCountQuery()
        {
        }

        public class PortalMenusTotalItemCountHandler : IRequestHandler<PortalMenusTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<PortalMenusTotalItemCountHandler> _logger;
            private readonly IPortalMenusRepository _repository;
            public PortalMenusTotalItemCountHandler(ILogger<PortalMenusTotalItemCountHandler> logger, IPortalMenusRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(PortalMenusTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}