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
    public class AspNetUsersAspNetUsersAspNetUserLogins_RESTQuery : IRequest<Response<List<AspNetUsersAspNetUserLogins_RESTResponseDto>>>
    {
        public string AspNetUsersId { get; set; }

        public AspNetUsersAspNetUsersAspNetUserLogins_RESTQuery(string aspNetUsersId)
        {
            this.AspNetUsersId = aspNetUsersId;
        }

        public class AspNetUsersAspNetUsersAspNetUserLogins_RESTHandler : IRequestHandler<AspNetUsersAspNetUsersAspNetUserLogins_RESTQuery, Response<List<AspNetUsersAspNetUserLogins_RESTResponseDto>>>
        {
            private readonly ILogger<AspNetUsersAspNetUsersAspNetUserLogins_RESTHandler> _logger;
            private readonly IAspNetUsersRepository _repository;
            public AspNetUsersAspNetUsersAspNetUserLogins_RESTHandler(ILogger<AspNetUsersAspNetUsersAspNetUserLogins_RESTHandler> logger, IAspNetUsersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<AspNetUsersAspNetUserLogins_RESTResponseDto>>> Handle(AspNetUsersAspNetUsersAspNetUserLogins_RESTQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.AspNetUsersAspNetUserLogins_RESTAsync(request.AspNetUsersId);
                return returnValue.ToResponse();
            }
        }
    }
}