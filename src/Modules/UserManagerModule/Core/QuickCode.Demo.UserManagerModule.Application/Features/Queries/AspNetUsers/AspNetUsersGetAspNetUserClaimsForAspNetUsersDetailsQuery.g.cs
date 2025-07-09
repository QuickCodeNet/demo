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

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class AspNetUsersGetAspNetUserClaimsForAspNetUsersDetailsQuery : IRequest<Response<AspNetUsersGetAspNetUserClaimsForAspNetUsersResponseDto>>
    {
        public string AspNetUsersId { get; set; }
        public int AspNetUserClaimsId { get; set; }

        public AspNetUsersGetAspNetUserClaimsForAspNetUsersDetailsQuery(string aspNetUsersId, int aspNetUserClaimsId)
        {
            this.AspNetUsersId = aspNetUsersId;
            this.AspNetUserClaimsId = aspNetUserClaimsId;
        }

        public class AspNetUsersGetAspNetUserClaimsForAspNetUsersDetailsHandler : IRequestHandler<AspNetUsersGetAspNetUserClaimsForAspNetUsersDetailsQuery, Response<AspNetUsersGetAspNetUserClaimsForAspNetUsersResponseDto>>
        {
            private readonly ILogger<AspNetUsersGetAspNetUserClaimsForAspNetUsersDetailsHandler> _logger;
            private readonly IAspNetUsersRepository _repository;
            public AspNetUsersGetAspNetUserClaimsForAspNetUsersDetailsHandler(ILogger<AspNetUsersGetAspNetUserClaimsForAspNetUsersDetailsHandler> logger, IAspNetUsersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetUsersGetAspNetUserClaimsForAspNetUsersResponseDto>> Handle(AspNetUsersGetAspNetUserClaimsForAspNetUsersDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.AspNetUsersGetAspNetUserClaimsForAspNetUsersDetailsAsync(request.AspNetUsersId, request.AspNetUserClaimsId);
                return returnValue.ToResponse();
            }
        }
    }
}