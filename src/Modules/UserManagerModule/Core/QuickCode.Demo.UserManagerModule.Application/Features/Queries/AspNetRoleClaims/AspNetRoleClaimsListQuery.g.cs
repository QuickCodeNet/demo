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
    public class AspNetRoleClaimsListQuery : IRequest<Response<List<AspNetRoleClaimsDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public AspNetRoleClaimsListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class AspNetRoleClaimsListHandler : IRequestHandler<AspNetRoleClaimsListQuery, Response<List<AspNetRoleClaimsDto>>>
        {
            private readonly ILogger<AspNetRoleClaimsListHandler> _logger;
            private readonly IAspNetRoleClaimsRepository _repository;
            public AspNetRoleClaimsListHandler(ILogger<AspNetRoleClaimsListHandler> logger, IAspNetRoleClaimsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<AspNetRoleClaimsDto>>> Handle(AspNetRoleClaimsListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}