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
    public class AspNetRolesGetItemQuery : IRequest<Response<AspNetRolesDto>>
    {
        public string Id { get; set; }

        public AspNetRolesGetItemQuery(string id)
        {
            this.Id = id;
        }

        public class AspNetRolesGetItemHandler : IRequestHandler<AspNetRolesGetItemQuery, Response<AspNetRolesDto>>
        {
            private readonly ILogger<AspNetRolesGetItemHandler> _logger;
            private readonly IAspNetRolesRepository _repository;
            public AspNetRolesGetItemHandler(ILogger<AspNetRolesGetItemHandler> logger, IAspNetRolesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetRolesDto>> Handle(AspNetRolesGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}