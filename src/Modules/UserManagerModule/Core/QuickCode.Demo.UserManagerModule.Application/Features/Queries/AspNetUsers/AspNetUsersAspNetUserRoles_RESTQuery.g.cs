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
    public class AspNetUsersAspNetUsersAspNetUserRoles_RESTQuery : IRequest<Response<List<AspNetUsersAspNetUserRoles_RESTResponseDto>>>
    {
        public string AspNetUsersId { get; set; }

        public AspNetUsersAspNetUsersAspNetUserRoles_RESTQuery(string aspNetUsersId)
        {
            this.AspNetUsersId = aspNetUsersId;
        }

        public class AspNetUsersAspNetUsersAspNetUserRoles_RESTHandler : IRequestHandler<AspNetUsersAspNetUsersAspNetUserRoles_RESTQuery, Response<List<AspNetUsersAspNetUserRoles_RESTResponseDto>>>
        {
            private readonly ILogger<AspNetUsersAspNetUsersAspNetUserRoles_RESTHandler> _logger;
            private readonly IAspNetUsersRepository _repository;
            public AspNetUsersAspNetUsersAspNetUserRoles_RESTHandler(ILogger<AspNetUsersAspNetUsersAspNetUserRoles_RESTHandler> logger, IAspNetUsersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<AspNetUsersAspNetUserRoles_RESTResponseDto>>> Handle(AspNetUsersAspNetUsersAspNetUserRoles_RESTQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.AspNetUsersAspNetUserRoles_RESTAsync(request.AspNetUsersId);
                return returnValue.ToResponse();
            }
        }
    }
}