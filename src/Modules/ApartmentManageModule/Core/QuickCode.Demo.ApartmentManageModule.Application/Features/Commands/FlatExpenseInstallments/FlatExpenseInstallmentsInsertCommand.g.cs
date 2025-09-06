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
    public class FlatExpenseInstallmentsInsertCommand : IRequest<Response<FlatExpenseInstallmentsDto>>
    {
        public FlatExpenseInstallmentsDto request { get; set; }

        public FlatExpenseInstallmentsInsertCommand(FlatExpenseInstallmentsDto request)
        {
            this.request = request;
        }

        public class FlatExpenseInstallmentsInsertHandler : IRequestHandler<FlatExpenseInstallmentsInsertCommand, Response<FlatExpenseInstallmentsDto>>
        {
            private readonly ILogger<FlatExpenseInstallmentsInsertHandler> _logger;
            private readonly IFlatExpenseInstallmentsRepository _repository;
            public FlatExpenseInstallmentsInsertHandler(ILogger<FlatExpenseInstallmentsInsertHandler> logger, IFlatExpenseInstallmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<FlatExpenseInstallmentsDto>> Handle(FlatExpenseInstallmentsInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}