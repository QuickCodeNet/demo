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
    public class PaymentYearsInsertCommand : IRequest<Response<PaymentYearsDto>>
    {
        public PaymentYearsDto request { get; set; }

        public PaymentYearsInsertCommand(PaymentYearsDto request)
        {
            this.request = request;
        }

        public class PaymentYearsInsertHandler : IRequestHandler<PaymentYearsInsertCommand, Response<PaymentYearsDto>>
        {
            private readonly ILogger<PaymentYearsInsertHandler> _logger;
            private readonly IPaymentYearsRepository _repository;
            public PaymentYearsInsertHandler(ILogger<PaymentYearsInsertHandler> logger, IPaymentYearsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PaymentYearsDto>> Handle(PaymentYearsInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}