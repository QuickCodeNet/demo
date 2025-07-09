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
    public class AspNetRoleClaimsGetItemQuery : IRequest<Response<AspNetRoleClaimsDto>>
    {
        public int Id { get; set; }

        public AspNetRoleClaimsGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class AspNetRoleClaimsGetItemHandler : IRequestHandler<AspNetRoleClaimsGetItemQuery, Response<AspNetRoleClaimsDto>>
        {
            private readonly ILogger<AspNetRoleClaimsGetItemHandler> _logger;
            private readonly IAspNetRoleClaimsRepository _repository;
            public AspNetRoleClaimsGetItemHandler(ILogger<AspNetRoleClaimsGetItemHandler> logger, IAspNetRoleClaimsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetRoleClaimsDto>> Handle(AspNetRoleClaimsGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}