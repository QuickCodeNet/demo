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
    public class FeeTypesTotalItemCountQuery : IRequest<Response<int>>
    {
        public FeeTypesTotalItemCountQuery()
        {
        }

        public class FeeTypesTotalItemCountHandler : IRequestHandler<FeeTypesTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<FeeTypesTotalItemCountHandler> _logger;
            private readonly IFeeTypesRepository _repository;
            public FeeTypesTotalItemCountHandler(ILogger<FeeTypesTotalItemCountHandler> logger, IFeeTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(FeeTypesTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}