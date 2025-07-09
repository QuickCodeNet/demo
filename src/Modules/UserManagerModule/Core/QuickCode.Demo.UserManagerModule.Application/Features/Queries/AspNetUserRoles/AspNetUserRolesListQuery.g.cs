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
    public class AspNetUserRolesListQuery : IRequest<Response<List<AspNetUserRolesDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public AspNetUserRolesListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class AspNetUserRolesListHandler : IRequestHandler<AspNetUserRolesListQuery, Response<List<AspNetUserRolesDto>>>
        {
            private readonly ILogger<AspNetUserRolesListHandler> _logger;
            private readonly IAspNetUserRolesRepository _repository;
            public AspNetUserRolesListHandler(ILogger<AspNetUserRolesListHandler> logger, IAspNetUserRolesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<AspNetUserRolesDto>>> Handle(AspNetUserRolesListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}