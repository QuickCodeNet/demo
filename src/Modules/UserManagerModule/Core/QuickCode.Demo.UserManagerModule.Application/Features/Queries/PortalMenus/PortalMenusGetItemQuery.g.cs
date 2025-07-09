using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class PortalMenusGetItemQuery : IRequest<Response<PortalMenusDto>>
    {
        public int Id { get; set; }

        public PortalMenusGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class PortalMenusGetItemHandler : IRequestHandler<PortalMenusGetItemQuery, Response<PortalMenusDto>>
        {
            private readonly ILogger<PortalMenusGetItemHandler> _logger;
            private readonly IPortalMenusRepository _repository;
            public PortalMenusGetItemHandler(ILogger<PortalMenusGetItemHandler> logger, IPortalMenusRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PortalMenusDto>> Handle(PortalMenusGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}