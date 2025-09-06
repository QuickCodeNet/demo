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
    public class FlatContactsGetFlatContactsCountQuery : IRequest<Response<long>>
    {
        public int FlatContactsFlatId { get; set; }
        public bool FlatContactsIsActive { get; set; }

        public FlatContactsGetFlatContactsCountQuery(int flatContactsFlatId, bool flatContactsIsActive)
        {
            this.FlatContactsFlatId = flatContactsFlatId;
            this.FlatContactsIsActive = flatContactsIsActive;
        }

        public class FlatContactsGetFlatContactsCountHandler : IRequestHandler<FlatContactsGetFlatContactsCountQuery, Response<long>>
        {
            private readonly ILogger<FlatContactsGetFlatContactsCountHandler> _logger;
            private readonly IFlatContactsRepository _repository;
            public FlatContactsGetFlatContactsCountHandler(ILogger<FlatContactsGetFlatContactsCountHandler> logger, IFlatContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<long>> Handle(FlatContactsGetFlatContactsCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatContactsGetFlatContactsCountAsync(request.FlatContactsFlatId, request.FlatContactsIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}