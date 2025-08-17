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
    public class FlatExpenseInstallmentsMarkFlatInstallmentAsPaidCommand : IRequest<Response<int>>
    {
        public int FlatExpenseInstallmentsId { get; set; }
        public FlatExpenseInstallmentsMarkFlatInstallmentAsPaidRequestDto UpdateRequest { get; set; }

        public FlatExpenseInstallmentsMarkFlatInstallmentAsPaidCommand(int flatExpenseInstallmentsId, FlatExpenseInstallmentsMarkFlatInstallmentAsPaidRequestDto updateRequest)
        {
            this.FlatExpenseInstallmentsId = flatExpenseInstallmentsId;
            this.UpdateRequest = updateRequest;
        }

        public class FlatExpenseInstallmentsMarkFlatInstallmentAsPaidHandler : IRequestHandler<FlatExpenseInstallmentsMarkFlatInstallmentAsPaidCommand, Response<int>>
        {
            private readonly ILogger<FlatExpenseInstallmentsMarkFlatInstallmentAsPaidHandler> _logger;
            private readonly IFlatExpenseInstallmentsRepository _repository;
            public FlatExpenseInstallmentsMarkFlatInstallmentAsPaidHandler(ILogger<FlatExpenseInstallmentsMarkFlatInstallmentAsPaidHandler> logger, IFlatExpenseInstallmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(FlatExpenseInstallmentsMarkFlatInstallmentAsPaidCommand request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatExpenseInstallmentsMarkFlatInstallmentAsPaidAsync(request.FlatExpenseInstallmentsId, request.UpdateRequest);
                return returnValue.ToResponse();
            }
        }
    }
}