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
    public class IdentityUserByNormalizedEmailQuery : IRequest<Response<List<IdentityUserByNormalizedEmailResponseDto>>>
    {
        public string? AspNetUserNormalizedEmail { get; set; }

        public IdentityUserByNormalizedEmailQuery(string? aspNetUserNormalizedEmail)
        {
            this.AspNetUserNormalizedEmail = aspNetUserNormalizedEmail;
        }

        public class IdentityUserByNormalizedEmailHandler : IRequestHandler<IdentityUserByNormalizedEmailQuery, Response<List<IdentityUserByNormalizedEmailResponseDto>>>
        {
            private readonly ILogger<IdentityUserByNormalizedEmailHandler> _logger;
            private readonly IAspNetUserRepository _repository;
            public IdentityUserByNormalizedEmailHandler(ILogger<IdentityUserByNormalizedEmailHandler> logger, IAspNetUserRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<IdentityUserByNormalizedEmailResponseDto>>> Handle(IdentityUserByNormalizedEmailQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.IdentityUserByNormalizedEmailAsync(request.AspNetUserNormalizedEmail);
                return returnValue.ToResponse();
            }
        }
    }
}