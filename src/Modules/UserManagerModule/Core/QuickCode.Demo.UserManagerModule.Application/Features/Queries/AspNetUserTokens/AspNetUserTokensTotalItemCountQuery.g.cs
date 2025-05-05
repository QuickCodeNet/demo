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
    public class AspNetUserTokensTotalItemCountQuery : IRequest<Response<int>>
    {
        public AspNetUserTokensTotalItemCountQuery()
        {
        }

        public class AspNetUserTokensTotalItemCountHandler : IRequestHandler<AspNetUserTokensTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<AspNetUserTokensTotalItemCountHandler> _logger;
            private readonly IAspNetUserTokensRepository _repository;
            public AspNetUserTokensTotalItemCountHandler(ILogger<AspNetUserTokensTotalItemCountHandler> logger, IAspNetUserTokensRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(AspNetUserTokensTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}