using System;
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
    public class FlatsGetFlatContactsForFlatsQuery : IRequest<Response<List<FlatsGetFlatContactsForFlatsResponseDto>>>
    {
        public int FlatsId { get; set; }

        public FlatsGetFlatContactsForFlatsQuery(int flatsId)
        {
            this.FlatsId = flatsId;
        }

        public class FlatsGetFlatContactsForFlatsHandler : IRequestHandler<FlatsGetFlatContactsForFlatsQuery, Response<List<FlatsGetFlatContactsForFlatsResponseDto>>>
        {
            private readonly ILogger<FlatsGetFlatContactsForFlatsHandler> _logger;
            private readonly IFlatsRepository _repository;
            public FlatsGetFlatContactsForFlatsHandler(ILogger<FlatsGetFlatContactsForFlatsHandler> logger, IFlatsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatsGetFlatContactsForFlatsResponseDto>>> Handle(FlatsGetFlatContactsForFlatsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatsGetFlatContactsForFlatsAsync(request.FlatsId);
                return returnValue.ToResponse();
            }
        }
    }
}