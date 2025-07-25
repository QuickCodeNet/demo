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
    public class PaymentYearsDeleteCommand : IRequest<Response<bool>>
    {
        public PaymentYearsDto request { get; set; }

        public PaymentYearsDeleteCommand(PaymentYearsDto request)
        {
            this.request = request;
        }

        public class PaymentYearsDeleteHandler : IRequestHandler<PaymentYearsDeleteCommand, Response<bool>>
        {
            private readonly ILogger<PaymentYearsDeleteHandler> _logger;
            private readonly IPaymentYearsRepository _repository;
            public PaymentYearsDeleteHandler(ILogger<PaymentYearsDeleteHandler> logger, IPaymentYearsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(PaymentYearsDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}