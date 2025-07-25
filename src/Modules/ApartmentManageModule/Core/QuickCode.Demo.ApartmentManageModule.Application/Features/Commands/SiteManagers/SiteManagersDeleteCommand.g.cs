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
    public class SiteManagersDeleteCommand : IRequest<Response<bool>>
    {
        public SiteManagersDto request { get; set; }

        public SiteManagersDeleteCommand(SiteManagersDto request)
        {
            this.request = request;
        }

        public class SiteManagersDeleteHandler : IRequestHandler<SiteManagersDeleteCommand, Response<bool>>
        {
            private readonly ILogger<SiteManagersDeleteHandler> _logger;
            private readonly ISiteManagersRepository _repository;
            public SiteManagersDeleteHandler(ILogger<SiteManagersDeleteHandler> logger, ISiteManagersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(SiteManagersDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}