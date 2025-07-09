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
    public class AspNetUsersGetAspNetUserTokensForAspNetUsersQuery : IRequest<Response<List<AspNetUsersGetAspNetUserTokensForAspNetUsersResponseDto>>>
    {
        public string AspNetUsersId { get; set; }

        public AspNetUsersGetAspNetUserTokensForAspNetUsersQuery(string aspNetUsersId)
        {
            this.AspNetUsersId = aspNetUsersId;
        }

        public class AspNetUsersGetAspNetUserTokensForAspNetUsersHandler : IRequestHandler<AspNetUsersGetAspNetUserTokensForAspNetUsersQuery, Response<List<AspNetUsersGetAspNetUserTokensForAspNetUsersResponseDto>>>
        {
            private readonly ILogger<AspNetUsersGetAspNetUserTokensForAspNetUsersHandler> _logger;
            private readonly IAspNetUsersRepository _repository;
            public AspNetUsersGetAspNetUserTokensForAspNetUsersHandler(ILogger<AspNetUsersGetAspNetUserTokensForAspNetUsersHandler> logger, IAspNetUsersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<AspNetUsersGetAspNetUserTokensForAspNetUsersResponseDto>>> Handle(AspNetUsersGetAspNetUserTokensForAspNetUsersQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.AspNetUsersGetAspNetUserTokensForAspNetUsersAsync(request.AspNetUsersId);
                return returnValue.ToResponse();
            }
        }
    }
}