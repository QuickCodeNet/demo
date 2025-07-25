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
    public class FlatsGetFlatContactsForFlatsDetailsQuery : IRequest<Response<FlatsGetFlatContactsForFlatsResponseDto>>
    {
        public int FlatsId { get; set; }
        public int FlatContactsId { get; set; }

        public FlatsGetFlatContactsForFlatsDetailsQuery(int flatsId, int flatContactsId)
        {
            this.FlatsId = flatsId;
            this.FlatContactsId = flatContactsId;
        }

        public class FlatsGetFlatContactsForFlatsDetailsHandler : IRequestHandler<FlatsGetFlatContactsForFlatsDetailsQuery, Response<FlatsGetFlatContactsForFlatsResponseDto>>
        {
            private readonly ILogger<FlatsGetFlatContactsForFlatsDetailsHandler> _logger;
            private readonly IFlatsRepository _repository;
            public FlatsGetFlatContactsForFlatsDetailsHandler(ILogger<FlatsGetFlatContactsForFlatsDetailsHandler> logger, IFlatsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<FlatsGetFlatContactsForFlatsResponseDto>> Handle(FlatsGetFlatContactsForFlatsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatsGetFlatContactsForFlatsDetailsAsync(request.FlatsId, request.FlatContactsId);
                return returnValue.ToResponse();
            }
        }
    }
}