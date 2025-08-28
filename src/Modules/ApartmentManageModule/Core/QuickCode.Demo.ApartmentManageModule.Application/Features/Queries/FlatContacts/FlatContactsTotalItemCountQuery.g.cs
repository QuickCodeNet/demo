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
    public class FlatContactsTotalItemCountQuery : IRequest<Response<int>>
    {
        public FlatContactsTotalItemCountQuery()
        {
        }

        public class FlatContactsTotalItemCountHandler : IRequestHandler<FlatContactsTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<FlatContactsTotalItemCountHandler> _logger;
            private readonly IFlatContactsRepository _repository;
            public FlatContactsTotalItemCountHandler(ILogger<FlatContactsTotalItemCountHandler> logger, IFlatContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(FlatContactsTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}