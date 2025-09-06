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
    public class SitesGetItemQuery : IRequest<Response<SitesDto>>
    {
        public int Id { get; set; }

        public SitesGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class SitesGetItemHandler : IRequestHandler<SitesGetItemQuery, Response<SitesDto>>
        {
            private readonly ILogger<SitesGetItemHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesGetItemHandler(ILogger<SitesGetItemHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<SitesDto>> Handle(SitesGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}