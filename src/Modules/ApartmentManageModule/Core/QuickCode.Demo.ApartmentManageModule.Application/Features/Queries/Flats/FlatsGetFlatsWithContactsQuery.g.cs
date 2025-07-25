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
    public class FlatsGetFlatsWithContactsQuery : IRequest<Response<List<FlatsGetFlatsWithContactsResponseDto>>>
    {
        public int FlatsId { get; set; }

        public FlatsGetFlatsWithContactsQuery(int flatsId)
        {
            this.FlatsId = flatsId;
        }

        public class FlatsGetFlatsWithContactsHandler : IRequestHandler<FlatsGetFlatsWithContactsQuery, Response<List<FlatsGetFlatsWithContactsResponseDto>>>
        {
            private readonly ILogger<FlatsGetFlatsWithContactsHandler> _logger;
            private readonly IFlatsRepository _repository;
            public FlatsGetFlatsWithContactsHandler(ILogger<FlatsGetFlatsWithContactsHandler> logger, IFlatsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatsGetFlatsWithContactsResponseDto>>> Handle(FlatsGetFlatsWithContactsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatsGetFlatsWithContactsAsync(request.FlatsId);
                return returnValue.ToResponse();
            }
        }
    }
}