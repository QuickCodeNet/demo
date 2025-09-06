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
    public class ApartmentsTotalItemCountQuery : IRequest<Response<int>>
    {
        public ApartmentsTotalItemCountQuery()
        {
        }

        public class ApartmentsTotalItemCountHandler : IRequestHandler<ApartmentsTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<ApartmentsTotalItemCountHandler> _logger;
            private readonly IApartmentsRepository _repository;
            public ApartmentsTotalItemCountHandler(ILogger<ApartmentsTotalItemCountHandler> logger, IApartmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(ApartmentsTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}