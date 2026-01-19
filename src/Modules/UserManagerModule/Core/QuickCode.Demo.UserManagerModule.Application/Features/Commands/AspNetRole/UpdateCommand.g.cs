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
    public class UpdateAspNetRoleCommand : IRequest<Response<bool>>
    {
        public string Id { get; set; }
        public AspNetRoleDto request { get; set; }

        public UpdateAspNetRoleCommand(string id, AspNetRoleDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class UpdateAspNetRoleHandler : IRequestHandler<UpdateAspNetRoleCommand, Response<bool>>
        {
            private readonly ILogger<UpdateAspNetRoleHandler> _logger;
            private readonly IAspNetRoleRepository _repository;
            public UpdateAspNetRoleHandler(ILogger<UpdateAspNetRoleHandler> logger, IAspNetRoleRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(UpdateAspNetRoleCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.Id);
                if (updateItem.Code == 404)
                    return Response<bool>.NotFound();
                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}