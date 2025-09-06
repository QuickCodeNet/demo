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
    public class AspNetUserRolesDeleteItemCommand : IRequest<Response<bool>>
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public AspNetUserRolesDeleteItemCommand(string userId, string roleId)
        {
            this.UserId = userId;
            this.RoleId = roleId;
        }

        public class AspNetUserRolesDeleteItemHandler : IRequestHandler<AspNetUserRolesDeleteItemCommand, Response<bool>>
        {
            private readonly ILogger<AspNetUserRolesDeleteItemHandler> _logger;
            private readonly IAspNetUserRolesRepository _repository;
            public AspNetUserRolesDeleteItemHandler(ILogger<AspNetUserRolesDeleteItemHandler> logger, IAspNetUserRolesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(AspNetUserRolesDeleteItemCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.UserId, request.RoleId);
                if (deleteItem.Code == 404)
                {
                    return new Response<bool>()
                    {
                        Code = 404,
                        Value = false
                    };
                }

                var returnValue = await _repository.DeleteAsync(deleteItem.Value);
                return returnValue.ToResponse();
            }
        }
    }
}