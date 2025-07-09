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
    public class AspNetRolesTotalItemCountQuery : IRequest<Response<int>>
    {
        public AspNetRolesTotalItemCountQuery()
        {
        }

        public class AspNetRolesTotalItemCountHandler : IRequestHandler<AspNetRolesTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<AspNetRolesTotalItemCountHandler> _logger;
            private readonly IAspNetRolesRepository _repository;
            public AspNetRolesTotalItemCountHandler(ILogger<AspNetRolesTotalItemCountHandler> logger, IAspNetRolesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(AspNetRolesTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}