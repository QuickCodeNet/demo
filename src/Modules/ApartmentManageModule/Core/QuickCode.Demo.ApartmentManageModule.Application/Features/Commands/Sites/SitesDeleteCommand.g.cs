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
    public class SitesDeleteCommand : IRequest<Response<bool>>
    {
        public SitesDto request { get; set; }

        public SitesDeleteCommand(SitesDto request)
        {
            this.request = request;
        }

        public class SitesDeleteHandler : IRequestHandler<SitesDeleteCommand, Response<bool>>
        {
            private readonly ILogger<SitesDeleteHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesDeleteHandler(ILogger<SitesDeleteHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(SitesDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}