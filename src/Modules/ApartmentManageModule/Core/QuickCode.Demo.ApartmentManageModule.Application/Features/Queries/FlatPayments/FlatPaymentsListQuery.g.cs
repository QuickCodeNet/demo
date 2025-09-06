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
    public class FlatPaymentsListQuery : IRequest<Response<List<FlatPaymentsDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public FlatPaymentsListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class FlatPaymentsListHandler : IRequestHandler<FlatPaymentsListQuery, Response<List<FlatPaymentsDto>>>
        {
            private readonly ILogger<FlatPaymentsListHandler> _logger;
            private readonly IFlatPaymentsRepository _repository;
            public FlatPaymentsListHandler(ILogger<FlatPaymentsListHandler> logger, IFlatPaymentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatPaymentsDto>>> Handle(FlatPaymentsListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}