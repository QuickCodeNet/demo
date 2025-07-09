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
    public class AspNetUserTokensGetItemQuery : IRequest<Response<AspNetUserTokensDto>>
    {
        public string UserId { get; set; }

        public AspNetUserTokensGetItemQuery(string userId)
        {
            this.UserId = userId;
        }

        public class AspNetUserTokensGetItemHandler : IRequestHandler<AspNetUserTokensGetItemQuery, Response<AspNetUserTokensDto>>
        {
            private readonly ILogger<AspNetUserTokensGetItemHandler> _logger;
            private readonly IAspNetUserTokensRepository _repository;
            public AspNetUserTokensGetItemHandler(ILogger<AspNetUserTokensGetItemHandler> logger, IAspNetUserTokensRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetUserTokensDto>> Handle(AspNetUserTokensGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.UserId);
                return returnValue.ToResponse();
            }
        }
    }
}