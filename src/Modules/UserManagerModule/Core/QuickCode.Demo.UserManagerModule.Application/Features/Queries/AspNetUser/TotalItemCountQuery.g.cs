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
using QuickCode.Demo.UserManagerModule.Application.Dtos.AspNetUser;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.AspNetUser
{
    public class TotalCountAspNetUserQuery : IRequest<Response<int>>
    {
        public TotalCountAspNetUserQuery()
        {
        }

        public class TotalCountAspNetUserHandler : IRequestHandler<TotalCountAspNetUserQuery, Response<int>>
        {
            private readonly ILogger<TotalCountAspNetUserHandler> _logger;
            private readonly IAspNetUserRepository _repository;
            public TotalCountAspNetUserHandler(ILogger<TotalCountAspNetUserHandler> logger, IAspNetUserRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(TotalCountAspNetUserQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}