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
using QuickCode.Demo.UserManagerModule.Application.Dtos.AspNetUserRole;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.AspNetUserRole
{
    public class DeleteAspNetUserRoleCommand : IRequest<Response<bool>>
    {
        public AspNetUserRoleDto request { get; set; }

        public DeleteAspNetUserRoleCommand(AspNetUserRoleDto request)
        {
            this.request = request;
        }

        public class DeleteAspNetUserRoleHandler : IRequestHandler<DeleteAspNetUserRoleCommand, Response<bool>>
        {
            private readonly ILogger<DeleteAspNetUserRoleHandler> _logger;
            private readonly IAspNetUserRoleRepository _repository;
            public DeleteAspNetUserRoleHandler(ILogger<DeleteAspNetUserRoleHandler> logger, IAspNetUserRoleRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteAspNetUserRoleCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}