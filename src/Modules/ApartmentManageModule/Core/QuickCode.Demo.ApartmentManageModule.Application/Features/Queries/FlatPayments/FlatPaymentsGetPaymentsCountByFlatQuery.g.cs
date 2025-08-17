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
    public class FlatPaymentsGetPaymentsCountByFlatQuery : IRequest<Response<long>>
    {
        public int FlatPaymentsFlatId { get; set; }

        public FlatPaymentsGetPaymentsCountByFlatQuery(int flatPaymentsFlatId)
        {
            this.FlatPaymentsFlatId = flatPaymentsFlatId;
        }

        public class FlatPaymentsGetPaymentsCountByFlatHandler : IRequestHandler<FlatPaymentsGetPaymentsCountByFlatQuery, Response<long>>
        {
            private readonly ILogger<FlatPaymentsGetPaymentsCountByFlatHandler> _logger;
            private readonly IFlatPaymentsRepository _repository;
            public FlatPaymentsGetPaymentsCountByFlatHandler(ILogger<FlatPaymentsGetPaymentsCountByFlatHandler> logger, IFlatPaymentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<long>> Handle(FlatPaymentsGetPaymentsCountByFlatQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatPaymentsGetPaymentsCountByFlatAsync(request.FlatPaymentsFlatId);
                return returnValue.ToResponse();
            }
        }
    }
}