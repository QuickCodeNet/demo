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
    public class AspNetUserRolesTotalItemCountQuery : IRequest<Response<int>>
    {
        public AspNetUserRolesTotalItemCountQuery()
        {
        }

        public class AspNetUserRolesTotalItemCountHandler : IRequestHandler<AspNetUserRolesTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<AspNetUserRolesTotalItemCountHandler> _logger;
            private readonly IAspNetUserRolesRepository _repository;
            public AspNetUserRolesTotalItemCountHandler(ILogger<AspNetUserRolesTotalItemCountHandler> logger, IAspNetUserRolesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(AspNetUserRolesTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}