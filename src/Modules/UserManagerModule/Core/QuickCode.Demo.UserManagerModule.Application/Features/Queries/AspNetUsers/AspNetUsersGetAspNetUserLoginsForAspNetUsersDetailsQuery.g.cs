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
    public class AspNetUsersGetAspNetUserLoginsForAspNetUsersDetailsQuery : IRequest<Response<AspNetUsersGetAspNetUserLoginsForAspNetUsersResponseDto>>
    {
        public string AspNetUsersId { get; set; }
        public string AspNetUserLoginsLoginProvider { get; set; }

        public AspNetUsersGetAspNetUserLoginsForAspNetUsersDetailsQuery(string aspNetUsersId, string aspNetUserLoginsLoginProvider)
        {
            this.AspNetUsersId = aspNetUsersId;
            this.AspNetUserLoginsLoginProvider = aspNetUserLoginsLoginProvider;
        }

        public class AspNetUsersGetAspNetUserLoginsForAspNetUsersDetailsHandler : IRequestHandler<AspNetUsersGetAspNetUserLoginsForAspNetUsersDetailsQuery, Response<AspNetUsersGetAspNetUserLoginsForAspNetUsersResponseDto>>
        {
            private readonly ILogger<AspNetUsersGetAspNetUserLoginsForAspNetUsersDetailsHandler> _logger;
            private readonly IAspNetUsersRepository _repository;
            public AspNetUsersGetAspNetUserLoginsForAspNetUsersDetailsHandler(ILogger<AspNetUsersGetAspNetUserLoginsForAspNetUsersDetailsHandler> logger, IAspNetUsersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetUsersGetAspNetUserLoginsForAspNetUsersResponseDto>> Handle(AspNetUsersGetAspNetUserLoginsForAspNetUsersDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.AspNetUsersGetAspNetUserLoginsForAspNetUsersDetailsAsync(request.AspNetUsersId, request.AspNetUserLoginsLoginProvider);
                return returnValue.ToResponse();
            }
        }
    }
}