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
    public class PaymentMonthsInsertCommand : IRequest<Response<PaymentMonthsDto>>
    {
        public PaymentMonthsDto request { get; set; }

        public PaymentMonthsInsertCommand(PaymentMonthsDto request)
        {
            this.request = request;
        }

        public class PaymentMonthsInsertHandler : IRequestHandler<PaymentMonthsInsertCommand, Response<PaymentMonthsDto>>
        {
            private readonly ILogger<PaymentMonthsInsertHandler> _logger;
            private readonly IPaymentMonthsRepository _repository;
            public PaymentMonthsInsertHandler(ILogger<PaymentMonthsInsertHandler> logger, IPaymentMonthsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PaymentMonthsDto>> Handle(PaymentMonthsInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}