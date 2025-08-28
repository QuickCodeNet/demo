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
    public class FlatPaymentsInsertCommand : IRequest<Response<FlatPaymentsDto>>
    {
        public FlatPaymentsDto request { get; set; }

        public FlatPaymentsInsertCommand(FlatPaymentsDto request)
        {
            this.request = request;
        }

        public class FlatPaymentsInsertHandler : IRequestHandler<FlatPaymentsInsertCommand, Response<FlatPaymentsDto>>
        {
            private readonly ILogger<FlatPaymentsInsertHandler> _logger;
            private readonly IFlatPaymentsRepository _repository;
            public FlatPaymentsInsertHandler(ILogger<FlatPaymentsInsertHandler> logger, IFlatPaymentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<FlatPaymentsDto>> Handle(FlatPaymentsInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}