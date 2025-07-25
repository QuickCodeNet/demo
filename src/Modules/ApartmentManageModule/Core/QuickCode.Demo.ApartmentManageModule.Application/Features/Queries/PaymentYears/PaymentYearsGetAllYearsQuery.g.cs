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
    public class PaymentYearsGetAllYearsQuery : IRequest<Response<List<PaymentYearsGetAllYearsResponseDto>>>
    {
        public PaymentYearsGetAllYearsQuery()
        {
        }

        public class PaymentYearsGetAllYearsHandler : IRequestHandler<PaymentYearsGetAllYearsQuery, Response<List<PaymentYearsGetAllYearsResponseDto>>>
        {
            private readonly ILogger<PaymentYearsGetAllYearsHandler> _logger;
            private readonly IPaymentYearsRepository _repository;
            public PaymentYearsGetAllYearsHandler(ILogger<PaymentYearsGetAllYearsHandler> logger, IPaymentYearsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PaymentYearsGetAllYearsResponseDto>>> Handle(PaymentYearsGetAllYearsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PaymentYearsGetAllYearsAsync();
                return returnValue.ToResponse();
            }
        }
    }
}