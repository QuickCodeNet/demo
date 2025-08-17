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
    public class AspNetUsersGetUserQuery : IRequest<Response<AspNetUsersGetUserResponseDto>>
    {
        public string? AspNetUsersEmail { get; set; }

        public AspNetUsersGetUserQuery(string? aspNetUsersEmail)
        {
            this.AspNetUsersEmail = aspNetUsersEmail;
        }

        public class AspNetUsersGetUserHandler : IRequestHandler<AspNetUsersGetUserQuery, Response<AspNetUsersGetUserResponseDto>>
        {
            private readonly ILogger<AspNetUsersGetUserHandler> _logger;
            private readonly IAspNetUsersRepository _repository;
            public AspNetUsersGetUserHandler(ILogger<AspNetUsersGetUserHandler> logger, IAspNetUsersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetUsersGetUserResponseDto>> Handle(AspNetUsersGetUserQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.AspNetUsersGetUserAsync(request.AspNetUsersEmail);
                return returnValue.ToResponse();
            }
        }
    }
}