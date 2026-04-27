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
using QuickCode.Demo.IdentityModule.Application.Dtos.AspNetRoleClaim;
using QuickCode.Demo.IdentityModule.Domain.Enums;

namespace QuickCode.Demo.IdentityModule.Application.Features.AspNetRoleClaim
{
    public class GetItemAspNetRoleClaimQuery : IRequest<Response<AspNetRoleClaimDto>>
    {
        public int Id { get; set; }

        public GetItemAspNetRoleClaimQuery(int id)
        {
            this.Id = id;
        }

        public class GetItemAspNetRoleClaimHandler : IRequestHandler<GetItemAspNetRoleClaimQuery, Response<AspNetRoleClaimDto>>
        {
            private readonly ILogger<GetItemAspNetRoleClaimHandler> _logger;
            private readonly IAspNetRoleClaimRepository _repository;
            public GetItemAspNetRoleClaimHandler(ILogger<GetItemAspNetRoleClaimHandler> logger, IAspNetRoleClaimRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetRoleClaimDto>> Handle(GetItemAspNetRoleClaimQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}