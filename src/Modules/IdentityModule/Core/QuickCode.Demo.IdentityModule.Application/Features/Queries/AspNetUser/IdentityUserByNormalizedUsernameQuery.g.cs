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
using QuickCode.Demo.IdentityModule.Application.Dtos.AspNetUser;
using QuickCode.Demo.IdentityModule.Domain.Enums;

namespace QuickCode.Demo.IdentityModule.Application.Features.AspNetUser
{
    public class IdentityUserByNormalizedUsernameQuery : IRequest<Response<List<IdentityUserByNormalizedUsernameResponseDto>>>
    {
        public string? AspNetUserNormalizedUserName { get; set; }

        public IdentityUserByNormalizedUsernameQuery(string? aspNetUserNormalizedUserName)
        {
            this.AspNetUserNormalizedUserName = aspNetUserNormalizedUserName;
        }

        public class IdentityUserByNormalizedUsernameHandler : IRequestHandler<IdentityUserByNormalizedUsernameQuery, Response<List<IdentityUserByNormalizedUsernameResponseDto>>>
        {
            private readonly ILogger<IdentityUserByNormalizedUsernameHandler> _logger;
            private readonly IAspNetUserRepository _repository;
            public IdentityUserByNormalizedUsernameHandler(ILogger<IdentityUserByNormalizedUsernameHandler> logger, IAspNetUserRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<IdentityUserByNormalizedUsernameResponseDto>>> Handle(IdentityUserByNormalizedUsernameQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.IdentityUserByNormalizedUsernameAsync(request.AspNetUserNormalizedUserName);
                return returnValue.ToResponse();
            }
        }
    }
}