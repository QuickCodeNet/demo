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
    public class AspNetUsersAspNetUsersAspNetUserTokens_RESTQuery : IRequest<Response<List<AspNetUsersAspNetUserTokens_RESTResponseDto>>>
    {
        public string AspNetUsersId { get; set; }

        public AspNetUsersAspNetUsersAspNetUserTokens_RESTQuery(string aspNetUsersId)
        {
            this.AspNetUsersId = aspNetUsersId;
        }

        public class AspNetUsersAspNetUsersAspNetUserTokens_RESTHandler : IRequestHandler<AspNetUsersAspNetUsersAspNetUserTokens_RESTQuery, Response<List<AspNetUsersAspNetUserTokens_RESTResponseDto>>>
        {
            private readonly ILogger<AspNetUsersAspNetUsersAspNetUserTokens_RESTHandler> _logger;
            private readonly IAspNetUsersRepository _repository;
            public AspNetUsersAspNetUsersAspNetUserTokens_RESTHandler(ILogger<AspNetUsersAspNetUsersAspNetUserTokens_RESTHandler> logger, IAspNetUsersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<AspNetUsersAspNetUserTokens_RESTResponseDto>>> Handle(AspNetUsersAspNetUsersAspNetUserTokens_RESTQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.AspNetUsersAspNetUserTokens_RESTAsync(request.AspNetUsersId);
                return returnValue.ToResponse();
            }
        }
    }
}