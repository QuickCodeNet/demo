using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.IdentityModule.Domain.Entities;
using QuickCode.Demo.IdentityModule.Application.Interfaces.Repositories;
using QuickCode.Demo.IdentityModule.Application.Dtos.PortalPageAccessGrant;
using QuickCode.Demo.IdentityModule.Domain.Enums;

namespace QuickCode.Demo.IdentityModule.Application.Features.PortalPageAccessGrant
{
    public class ListPortalPageAccessGrantQuery : IRequest<Response<List<PortalPageAccessGrantDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public ListPortalPageAccessGrantQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class ListPortalPageAccessGrantHandler : IRequestHandler<ListPortalPageAccessGrantQuery, Response<List<PortalPageAccessGrantDto>>>
        {
            private readonly ILogger<ListPortalPageAccessGrantHandler> _logger;
            private readonly IPortalPageAccessGrantRepository _repository;
            public ListPortalPageAccessGrantHandler(ILogger<ListPortalPageAccessGrantHandler> logger, IPortalPageAccessGrantRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PortalPageAccessGrantDto>>> Handle(ListPortalPageAccessGrantQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}