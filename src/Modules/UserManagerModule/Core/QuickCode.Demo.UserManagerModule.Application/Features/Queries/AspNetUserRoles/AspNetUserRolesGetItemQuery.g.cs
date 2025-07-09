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
    public class AspNetUserRolesGetItemQuery : IRequest<Response<AspNetUserRolesDto>>
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public AspNetUserRolesGetItemQuery(string userId, string roleId)
        {
            this.UserId = userId;
            this.RoleId = roleId;
        }

        public class AspNetUserRolesGetItemHandler : IRequestHandler<AspNetUserRolesGetItemQuery, Response<AspNetUserRolesDto>>
        {
            private readonly ILogger<AspNetUserRolesGetItemHandler> _logger;
            private readonly IAspNetUserRolesRepository _repository;
            public AspNetUserRolesGetItemHandler(ILogger<AspNetUserRolesGetItemHandler> logger, IAspNetUserRolesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetUserRolesDto>> Handle(AspNetUserRolesGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.UserId, request.RoleId);
                return returnValue.ToResponse();
            }
        }
    }
}