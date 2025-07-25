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
    public class SiteManagersInsertCommand : IRequest<Response<SiteManagersDto>>
    {
        public SiteManagersDto request { get; set; }

        public SiteManagersInsertCommand(SiteManagersDto request)
        {
            this.request = request;
        }

        public class SiteManagersInsertHandler : IRequestHandler<SiteManagersInsertCommand, Response<SiteManagersDto>>
        {
            private readonly ILogger<SiteManagersInsertHandler> _logger;
            private readonly ISiteManagersRepository _repository;
            public SiteManagersInsertHandler(ILogger<SiteManagersInsertHandler> logger, ISiteManagersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<SiteManagersDto>> Handle(SiteManagersInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}