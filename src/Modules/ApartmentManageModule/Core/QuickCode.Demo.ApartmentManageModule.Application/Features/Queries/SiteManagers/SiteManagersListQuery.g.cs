using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Features
{
    public class SiteManagersListQuery : IRequest<Response<List<SiteManagersDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public SiteManagersListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class SiteManagersListHandler : IRequestHandler<SiteManagersListQuery, Response<List<SiteManagersDto>>>
        {
            private readonly ILogger<SiteManagersListHandler> _logger;
            private readonly ISiteManagersRepository _repository;
            public SiteManagersListHandler(ILogger<SiteManagersListHandler> logger, ISiteManagersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<SiteManagersDto>>> Handle(SiteManagersListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}