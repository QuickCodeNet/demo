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
using QuickCode.Demo.UserManagerModule.Application.Dtos;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class AspNetUsersGetAspNetUserClaimsForAspNetUsersQuery : IRequest<Response<List<AspNetUsersGetAspNetUserClaimsForAspNetUsersResponseDto>>>
    {
        public string AspNetUsersId { get; set; }

        public AspNetUsersGetAspNetUserClaimsForAspNetUsersQuery(string aspNetUsersId)
        {
            this.AspNetUsersId = aspNetUsersId;
        }

        public class AspNetUsersGetAspNetUserClaimsForAspNetUsersHandler : IRequestHandler<AspNetUsersGetAspNetUserClaimsForAspNetUsersQuery, Response<List<AspNetUsersGetAspNetUserClaimsForAspNetUsersResponseDto>>>
        {
            private readonly ILogger<AspNetUsersGetAspNetUserClaimsForAspNetUsersHandler> _logger;
            private readonly IAspNetUsersRepository _repository;
            public AspNetUsersGetAspNetUserClaimsForAspNetUsersHandler(ILogger<AspNetUsersGetAspNetUserClaimsForAspNetUsersHandler> logger, IAspNetUsersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<AspNetUsersGetAspNetUserClaimsForAspNetUsersResponseDto>>> Handle(AspNetUsersGetAspNetUserClaimsForAspNetUsersQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.AspNetUsersGetAspNetUserClaimsForAspNetUsersAsync(request.AspNetUsersId);
                return returnValue.ToResponse();
            }
        }
    }
}