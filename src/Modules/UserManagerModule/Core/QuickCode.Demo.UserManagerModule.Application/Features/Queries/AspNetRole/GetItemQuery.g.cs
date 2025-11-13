using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos.AspNetRole;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.AspNetRole
{
    public class GetItemAspNetRoleQuery : IRequest<Response<AspNetRoleDto>>
    {
        public string Id { get; set; }

        public GetItemAspNetRoleQuery(string id)
        {
            this.Id = id;
        }

        public class GetItemAspNetRoleHandler : IRequestHandler<GetItemAspNetRoleQuery, Response<AspNetRoleDto>>
        {
            private readonly ILogger<GetItemAspNetRoleHandler> _logger;
            private readonly IAspNetRoleRepository _repository;
            public GetItemAspNetRoleHandler(ILogger<GetItemAspNetRoleHandler> logger, IAspNetRoleRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetRoleDto>> Handle(GetItemAspNetRoleQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}