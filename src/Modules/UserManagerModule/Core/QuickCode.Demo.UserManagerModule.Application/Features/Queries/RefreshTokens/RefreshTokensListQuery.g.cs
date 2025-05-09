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
    public class RefreshTokensListQuery : IRequest<Response<List<RefreshTokensDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public RefreshTokensListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class RefreshTokensListHandler : IRequestHandler<RefreshTokensListQuery, Response<List<RefreshTokensDto>>>
        {
            private readonly ILogger<RefreshTokensListHandler> _logger;
            private readonly IRefreshTokensRepository _repository;
            public RefreshTokensListHandler(ILogger<RefreshTokensListHandler> logger, IRefreshTokensRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<RefreshTokensDto>>> Handle(RefreshTokensListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}