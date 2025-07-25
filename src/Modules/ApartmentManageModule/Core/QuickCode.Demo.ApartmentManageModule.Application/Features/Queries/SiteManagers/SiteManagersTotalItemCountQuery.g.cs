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
    public class SiteManagersTotalItemCountQuery : IRequest<Response<int>>
    {
        public SiteManagersTotalItemCountQuery()
        {
        }

        public class SiteManagersTotalItemCountHandler : IRequestHandler<SiteManagersTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<SiteManagersTotalItemCountHandler> _logger;
            private readonly ISiteManagersRepository _repository;
            public SiteManagersTotalItemCountHandler(ILogger<SiteManagersTotalItemCountHandler> logger, ISiteManagersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(SiteManagersTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}