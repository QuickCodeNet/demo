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
    public class FlatExpenseInstallmentsDeleteCommand : IRequest<Response<bool>>
    {
        public FlatExpenseInstallmentsDto request { get; set; }

        public FlatExpenseInstallmentsDeleteCommand(FlatExpenseInstallmentsDto request)
        {
            this.request = request;
        }

        public class FlatExpenseInstallmentsDeleteHandler : IRequestHandler<FlatExpenseInstallmentsDeleteCommand, Response<bool>>
        {
            private readonly ILogger<FlatExpenseInstallmentsDeleteHandler> _logger;
            private readonly IFlatExpenseInstallmentsRepository _repository;
            public FlatExpenseInstallmentsDeleteHandler(ILogger<FlatExpenseInstallmentsDeleteHandler> logger, IFlatExpenseInstallmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(FlatExpenseInstallmentsDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}