using System;
using System.Linq;
using MediatR;
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
    public class AspNetUsersAspNetUsersRefreshTokens_RESTQuery : IRequest<Response<List<AspNetUsersRefreshTokens_RESTResponseDto>>>
    {
        public string AspNetUsersId { get; set; }

        public AspNetUsersAspNetUsersRefreshTokens_RESTQuery(string aspNetUsersId)
        {
            this.AspNetUsersId = aspNetUsersId;
        }

        public class AspNetUsersAspNetUsersRefreshTokens_RESTHandler : IRequestHandler<AspNetUsersAspNetUsersRefreshTokens_RESTQuery, Response<List<AspNetUsersRefreshTokens_RESTResponseDto>>>
        {
            private readonly ILogger<AspNetUsersAspNetUsersRefreshTokens_RESTHandler> _logger;
            private readonly IAspNetUsersRepository _repository;
            public AspNetUsersAspNetUsersRefreshTokens_RESTHandler(ILogger<AspNetUsersAspNetUsersRefreshTokens_RESTHandler> logger, IAspNetUsersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<AspNetUsersRefreshTokens_RESTResponseDto>>> Handle(AspNetUsersAspNetUsersRefreshTokens_RESTQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.AspNetUsersRefreshTokens_RESTAsync(request.AspNetUsersId);
                return returnValue.ToResponse();
            }
        }
    }
}