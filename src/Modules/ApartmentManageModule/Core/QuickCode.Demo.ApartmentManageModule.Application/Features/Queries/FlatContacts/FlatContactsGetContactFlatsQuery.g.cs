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
    public class FlatContactsGetContactFlatsQuery : IRequest<Response<List<FlatContactsGetContactFlatsResponseDto>>>
    {
        public int FlatContactsContactId { get; set; }
        public bool FlatContactsIsActive { get; set; }

        public FlatContactsGetContactFlatsQuery(int flatContactsContactId, bool flatContactsIsActive)
        {
            this.FlatContactsContactId = flatContactsContactId;
            this.FlatContactsIsActive = flatContactsIsActive;
        }

        public class FlatContactsGetContactFlatsHandler : IRequestHandler<FlatContactsGetContactFlatsQuery, Response<List<FlatContactsGetContactFlatsResponseDto>>>
        {
            private readonly ILogger<FlatContactsGetContactFlatsHandler> _logger;
            private readonly IFlatContactsRepository _repository;
            public FlatContactsGetContactFlatsHandler(ILogger<FlatContactsGetContactFlatsHandler> logger, IFlatContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatContactsGetContactFlatsResponseDto>>> Handle(FlatContactsGetContactFlatsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatContactsGetContactFlatsAsync(request.FlatContactsContactId, request.FlatContactsIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}