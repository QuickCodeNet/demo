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
    public class ApartmentsInsertCommand : IRequest<Response<ApartmentsDto>>
    {
        public ApartmentsDto request { get; set; }

        public ApartmentsInsertCommand(ApartmentsDto request)
        {
            this.request = request;
        }

        public class ApartmentsInsertHandler : IRequestHandler<ApartmentsInsertCommand, Response<ApartmentsDto>>
        {
            private readonly ILogger<ApartmentsInsertHandler> _logger;
            private readonly IApartmentsRepository _repository;
            public ApartmentsInsertHandler(ILogger<ApartmentsInsertHandler> logger, IApartmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ApartmentsDto>> Handle(ApartmentsInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}