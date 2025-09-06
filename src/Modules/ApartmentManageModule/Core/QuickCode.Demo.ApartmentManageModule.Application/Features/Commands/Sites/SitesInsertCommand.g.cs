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
    public class SitesInsertCommand : IRequest<Response<SitesDto>>
    {
        public SitesDto request { get; set; }

        public SitesInsertCommand(SitesDto request)
        {
            this.request = request;
        }

        public class SitesInsertHandler : IRequestHandler<SitesInsertCommand, Response<SitesDto>>
        {
            private readonly ILogger<SitesInsertHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesInsertHandler(ILogger<SitesInsertHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<SitesDto>> Handle(SitesInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}