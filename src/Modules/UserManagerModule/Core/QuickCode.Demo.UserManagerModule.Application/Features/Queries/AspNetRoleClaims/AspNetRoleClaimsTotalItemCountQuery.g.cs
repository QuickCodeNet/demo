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
    public class AspNetRoleClaimsTotalItemCountQuery : IRequest<Response<int>>
    {
        public AspNetRoleClaimsTotalItemCountQuery()
        {
        }

        public class AspNetRoleClaimsTotalItemCountHandler : IRequestHandler<AspNetRoleClaimsTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<AspNetRoleClaimsTotalItemCountHandler> _logger;
            private readonly IAspNetRoleClaimsRepository _repository;
            public AspNetRoleClaimsTotalItemCountHandler(ILogger<AspNetRoleClaimsTotalItemCountHandler> logger, IAspNetRoleClaimsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(AspNetRoleClaimsTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}