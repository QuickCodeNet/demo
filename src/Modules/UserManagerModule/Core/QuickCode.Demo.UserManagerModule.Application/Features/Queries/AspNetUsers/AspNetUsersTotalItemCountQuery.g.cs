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
    public class AspNetUsersTotalItemCountQuery : IRequest<Response<int>>
    {
        public AspNetUsersTotalItemCountQuery()
        {
        }

        public class AspNetUsersTotalItemCountHandler : IRequestHandler<AspNetUsersTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<AspNetUsersTotalItemCountHandler> _logger;
            private readonly IAspNetUsersRepository _repository;
            public AspNetUsersTotalItemCountHandler(ILogger<AspNetUsersTotalItemCountHandler> logger, IAspNetUsersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(AspNetUsersTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}