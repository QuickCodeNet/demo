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
    public class AspNetUsersAspNetUsersAspNetUserClaims_RESTQuery : IRequest<Response<List<AspNetUsersAspNetUserClaims_RESTResponseDto>>>
    {
        public string AspNetUsersId { get; set; }

        public AspNetUsersAspNetUsersAspNetUserClaims_RESTQuery(string aspNetUsersId)
        {
            this.AspNetUsersId = aspNetUsersId;
        }

        public class AspNetUsersAspNetUsersAspNetUserClaims_RESTHandler : IRequestHandler<AspNetUsersAspNetUsersAspNetUserClaims_RESTQuery, Response<List<AspNetUsersAspNetUserClaims_RESTResponseDto>>>
        {
            private readonly ILogger<AspNetUsersAspNetUsersAspNetUserClaims_RESTHandler> _logger;
            private readonly IAspNetUsersRepository _repository;
            public AspNetUsersAspNetUsersAspNetUserClaims_RESTHandler(ILogger<AspNetUsersAspNetUsersAspNetUserClaims_RESTHandler> logger, IAspNetUsersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<AspNetUsersAspNetUserClaims_RESTResponseDto>>> Handle(AspNetUsersAspNetUsersAspNetUserClaims_RESTQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.AspNetUsersAspNetUserClaims_RESTAsync(request.AspNetUsersId);
                return returnValue.ToResponse();
            }
        }
    }
}