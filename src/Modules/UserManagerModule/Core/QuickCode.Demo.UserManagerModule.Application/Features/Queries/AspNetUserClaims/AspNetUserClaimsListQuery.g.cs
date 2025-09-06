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
    public class AspNetUserClaimsListQuery : IRequest<Response<List<AspNetUserClaimsDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public AspNetUserClaimsListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class AspNetUserClaimsListHandler : IRequestHandler<AspNetUserClaimsListQuery, Response<List<AspNetUserClaimsDto>>>
        {
            private readonly ILogger<AspNetUserClaimsListHandler> _logger;
            private readonly IAspNetUserClaimsRepository _repository;
            public AspNetUserClaimsListHandler(ILogger<AspNetUserClaimsListHandler> logger, IAspNetUserClaimsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<AspNetUserClaimsDto>>> Handle(AspNetUserClaimsListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}