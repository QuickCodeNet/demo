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
using QuickCode.Demo.UserManagerModule.Application.Dtos.AspNetUserClaim;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.AspNetUserClaim
{
    public class GetItemAspNetUserClaimQuery : IRequest<Response<AspNetUserClaimDto>>
    {
        public int Id { get; set; }

        public GetItemAspNetUserClaimQuery(int id)
        {
            this.Id = id;
        }

        public class GetItemAspNetUserClaimHandler : IRequestHandler<GetItemAspNetUserClaimQuery, Response<AspNetUserClaimDto>>
        {
            private readonly ILogger<GetItemAspNetUserClaimHandler> _logger;
            private readonly IAspNetUserClaimRepository _repository;
            public GetItemAspNetUserClaimHandler(ILogger<GetItemAspNetUserClaimHandler> logger, IAspNetUserClaimRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetUserClaimDto>> Handle(GetItemAspNetUserClaimQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}