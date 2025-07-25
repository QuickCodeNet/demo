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
    public class PaymentMonthsDeleteCommand : IRequest<Response<bool>>
    {
        public PaymentMonthsDto request { get; set; }

        public PaymentMonthsDeleteCommand(PaymentMonthsDto request)
        {
            this.request = request;
        }

        public class PaymentMonthsDeleteHandler : IRequestHandler<PaymentMonthsDeleteCommand, Response<bool>>
        {
            private readonly ILogger<PaymentMonthsDeleteHandler> _logger;
            private readonly IPaymentMonthsRepository _repository;
            public PaymentMonthsDeleteHandler(ILogger<PaymentMonthsDeleteHandler> logger, IPaymentMonthsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(PaymentMonthsDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}