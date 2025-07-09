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
    public class PortalMenusListQuery : IRequest<Response<List<PortalMenusDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public PortalMenusListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class PortalMenusListHandler : IRequestHandler<PortalMenusListQuery, Response<List<PortalMenusDto>>>
        {
            private readonly ILogger<PortalMenusListHandler> _logger;
            private readonly IPortalMenusRepository _repository;
            public PortalMenusListHandler(ILogger<PortalMenusListHandler> logger, IPortalMenusRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PortalMenusDto>>> Handle(PortalMenusListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}