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
    public class RefreshTokensGetItemQuery : IRequest<Response<RefreshTokensDto>>
    {
        public int Id { get; set; }

        public RefreshTokensGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class RefreshTokensGetItemHandler : IRequestHandler<RefreshTokensGetItemQuery, Response<RefreshTokensDto>>
        {
            private readonly ILogger<RefreshTokensGetItemHandler> _logger;
            private readonly IRefreshTokensRepository _repository;
            public RefreshTokensGetItemHandler(ILogger<RefreshTokensGetItemHandler> logger, IRefreshTokensRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<RefreshTokensDto>> Handle(RefreshTokensGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}