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
    public class SitesListQuery : IRequest<Response<List<SitesDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public SitesListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class SitesListHandler : IRequestHandler<SitesListQuery, Response<List<SitesDto>>>
        {
            private readonly ILogger<SitesListHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesListHandler(ILogger<SitesListHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<SitesDto>>> Handle(SitesListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}