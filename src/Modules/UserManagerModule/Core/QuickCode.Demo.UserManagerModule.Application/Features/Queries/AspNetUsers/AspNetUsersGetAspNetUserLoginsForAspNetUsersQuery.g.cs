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
    public class AspNetUsersGetAspNetUserLoginsForAspNetUsersQuery : IRequest<Response<List<AspNetUsersGetAspNetUserLoginsForAspNetUsersResponseDto>>>
    {
        public string AspNetUsersId { get; set; }

        public AspNetUsersGetAspNetUserLoginsForAspNetUsersQuery(string aspNetUsersId)
        {
            this.AspNetUsersId = aspNetUsersId;
        }

        public class AspNetUsersGetAspNetUserLoginsForAspNetUsersHandler : IRequestHandler<AspNetUsersGetAspNetUserLoginsForAspNetUsersQuery, Response<List<AspNetUsersGetAspNetUserLoginsForAspNetUsersResponseDto>>>
        {
            private readonly ILogger<AspNetUsersGetAspNetUserLoginsForAspNetUsersHandler> _logger;
            private readonly IAspNetUsersRepository _repository;
            public AspNetUsersGetAspNetUserLoginsForAspNetUsersHandler(ILogger<AspNetUsersGetAspNetUserLoginsForAspNetUsersHandler> logger, IAspNetUsersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<AspNetUsersGetAspNetUserLoginsForAspNetUsersResponseDto>>> Handle(AspNetUsersGetAspNetUserLoginsForAspNetUsersQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.AspNetUsersGetAspNetUserLoginsForAspNetUsersAsync(request.AspNetUsersId);
                return returnValue.ToResponse();
            }
        }
    }
}