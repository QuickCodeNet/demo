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
using QuickCode.Demo.UserManagerModule.Application.Dtos.AspNetRoleClaim;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.AspNetRoleClaim
{
    public class DeleteAspNetRoleClaimCommand : IRequest<Response<bool>>
    {
        public AspNetRoleClaimDto request { get; set; }

        public DeleteAspNetRoleClaimCommand(AspNetRoleClaimDto request)
        {
            this.request = request;
        }

        public class DeleteAspNetRoleClaimHandler : IRequestHandler<DeleteAspNetRoleClaimCommand, Response<bool>>
        {
            private readonly ILogger<DeleteAspNetRoleClaimHandler> _logger;
            private readonly IAspNetRoleClaimRepository _repository;
            public DeleteAspNetRoleClaimHandler(ILogger<DeleteAspNetRoleClaimHandler> logger, IAspNetRoleClaimRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteAspNetRoleClaimCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}