using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.IdentityModule.Domain.Entities;
using QuickCode.Demo.IdentityModule.Application.Interfaces.Repositories;
using QuickCode.Demo.IdentityModule.Application.Dtos.AspNetRole;
using QuickCode.Demo.IdentityModule.Domain.Enums;

namespace QuickCode.Demo.IdentityModule.Application.Features.AspNetRole
{
    public class InsertAspNetRoleCommand : IRequest<Response<AspNetRoleDto>>
    {
        public AspNetRoleDto request { get; set; }

        public InsertAspNetRoleCommand(AspNetRoleDto request)
        {
            this.request = request;
        }

        public class InsertAspNetRoleHandler : IRequestHandler<InsertAspNetRoleCommand, Response<AspNetRoleDto>>
        {
            private readonly ILogger<InsertAspNetRoleHandler> _logger;
            private readonly IAspNetRoleRepository _repository;
            public InsertAspNetRoleHandler(ILogger<InsertAspNetRoleHandler> logger, IAspNetRoleRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetRoleDto>> Handle(InsertAspNetRoleCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}