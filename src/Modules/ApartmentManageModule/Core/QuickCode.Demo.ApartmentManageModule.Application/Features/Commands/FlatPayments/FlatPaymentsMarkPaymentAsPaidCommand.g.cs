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
    public class FlatPaymentsMarkPaymentAsPaidCommand : IRequest<Response<int>>
    {
        public int FlatPaymentsId { get; set; }
        public FlatPaymentsMarkPaymentAsPaidRequestDto UpdateRequest { get; set; }

        public FlatPaymentsMarkPaymentAsPaidCommand(int flatPaymentsId, FlatPaymentsMarkPaymentAsPaidRequestDto updateRequest)
        {
            this.FlatPaymentsId = flatPaymentsId;
            this.UpdateRequest = updateRequest;
        }

        public class FlatPaymentsMarkPaymentAsPaidHandler : IRequestHandler<FlatPaymentsMarkPaymentAsPaidCommand, Response<int>>
        {
            private readonly ILogger<FlatPaymentsMarkPaymentAsPaidHandler> _logger;
            private readonly IFlatPaymentsRepository _repository;
            public FlatPaymentsMarkPaymentAsPaidHandler(ILogger<FlatPaymentsMarkPaymentAsPaidHandler> logger, IFlatPaymentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(FlatPaymentsMarkPaymentAsPaidCommand request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatPaymentsMarkPaymentAsPaidAsync(request.FlatPaymentsId, request.UpdateRequest);
                return returnValue.ToResponse();
            }
        }
    }
}