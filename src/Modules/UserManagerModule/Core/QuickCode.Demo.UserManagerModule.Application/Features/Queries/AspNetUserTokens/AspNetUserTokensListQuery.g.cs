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
    public class AspNetUserTokensListQuery : IRequest<Response<List<AspNetUserTokensDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public AspNetUserTokensListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class AspNetUserTokensListHandler : IRequestHandler<AspNetUserTokensListQuery, Response<List<AspNetUserTokensDto>>>
        {
            private readonly ILogger<AspNetUserTokensListHandler> _logger;
            private readonly IAspNetUserTokensRepository _repository;
            public AspNetUserTokensListHandler(ILogger<AspNetUserTokensListHandler> logger, IAspNetUserTokensRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<AspNetUserTokensDto>>> Handle(AspNetUserTokensListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}