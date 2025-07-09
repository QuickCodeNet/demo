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
    public class AspNetUserRolesUpdateCommand : IRequest<Response<bool>>
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public AspNetUserRolesDto request { get; set; }

        public AspNetUserRolesUpdateCommand(string userId, string roleId, AspNetUserRolesDto request)
        {
            this.request = request;
            this.UserId = userId;
            this.RoleId = roleId;
        }

        public class AspNetUserRolesUpdateHandler : IRequestHandler<AspNetUserRolesUpdateCommand, Response<bool>>
        {
            private readonly ILogger<AspNetUserRolesUpdateHandler> _logger;
            private readonly IAspNetUserRolesRepository _repository;
            public AspNetUserRolesUpdateHandler(ILogger<AspNetUserRolesUpdateHandler> logger, IAspNetUserRolesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(AspNetUserRolesUpdateCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.UserId, request.RoleId);
                if (updateItem.Code == 404)
                {
                    return new Response<bool>()
                    {
                        Code = 404,
                        Value = false
                    };
                }

                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}