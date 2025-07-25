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
    public class FlatsTotalItemCountQuery : IRequest<Response<int>>
    {
        public FlatsTotalItemCountQuery()
        {
        }

        public class FlatsTotalItemCountHandler : IRequestHandler<FlatsTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<FlatsTotalItemCountHandler> _logger;
            private readonly IFlatsRepository _repository;
            public FlatsTotalItemCountHandler(ILogger<FlatsTotalItemCountHandler> logger, IFlatsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(FlatsTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}