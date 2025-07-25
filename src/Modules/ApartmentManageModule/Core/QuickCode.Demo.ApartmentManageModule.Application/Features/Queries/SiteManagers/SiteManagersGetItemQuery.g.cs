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
    public class SiteManagersGetItemQuery : IRequest<Response<SiteManagersDto>>
    {
        public int Id { get; set; }

        public SiteManagersGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class SiteManagersGetItemHandler : IRequestHandler<SiteManagersGetItemQuery, Response<SiteManagersDto>>
        {
            private readonly ILogger<SiteManagersGetItemHandler> _logger;
            private readonly ISiteManagersRepository _repository;
            public SiteManagersGetItemHandler(ILogger<SiteManagersGetItemHandler> logger, ISiteManagersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<SiteManagersDto>> Handle(SiteManagersGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}