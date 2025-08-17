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
    public class FlatsGetItemQuery : IRequest<Response<FlatsDto>>
    {
        public int Id { get; set; }

        public FlatsGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class FlatsGetItemHandler : IRequestHandler<FlatsGetItemQuery, Response<FlatsDto>>
        {
            private readonly ILogger<FlatsGetItemHandler> _logger;
            private readonly IFlatsRepository _repository;
            public FlatsGetItemHandler(ILogger<FlatsGetItemHandler> logger, IFlatsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<FlatsDto>> Handle(FlatsGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}