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
    public class DeleteItemAspNetUserRoleCommand : IRequest<Response<bool>>
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public DeleteItemAspNetUserRoleCommand(string userId, string roleId)
        {
            this.UserId = userId;
            this.RoleId = roleId;
        }

        public class DeleteItemAspNetUserRoleHandler : IRequestHandler<DeleteItemAspNetUserRoleCommand, Response<bool>>
        {
            private readonly ILogger<DeleteItemAspNetUserRoleHandler> _logger;
            private readonly IAspNetUserRoleRepository _repository;
            public DeleteItemAspNetUserRoleHandler(ILogger<DeleteItemAspNetUserRoleHandler> logger, IAspNetUserRoleRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteItemAspNetUserRoleCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.UserId, request.RoleId);
                if (deleteItem.Code == 404)
                    return Response<bool>.NotFound();
                var returnValue = await _repository.DeleteAsync(deleteItem.Value);
                return returnValue.ToResponse();
            }
        }
    }
}