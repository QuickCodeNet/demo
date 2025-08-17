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
    public class PaymentTypesInsertCommand : IRequest<Response<PaymentTypesDto>>
    {
        public PaymentTypesDto request { get; set; }

        public PaymentTypesInsertCommand(PaymentTypesDto request)
        {
            this.request = request;
        }

        public class PaymentTypesInsertHandler : IRequestHandler<PaymentTypesInsertCommand, Response<PaymentTypesDto>>
        {
            private readonly ILogger<PaymentTypesInsertHandler> _logger;
            private readonly IPaymentTypesRepository _repository;
            public PaymentTypesInsertHandler(ILogger<PaymentTypesInsertHandler> logger, IPaymentTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PaymentTypesDto>> Handle(PaymentTypesInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}