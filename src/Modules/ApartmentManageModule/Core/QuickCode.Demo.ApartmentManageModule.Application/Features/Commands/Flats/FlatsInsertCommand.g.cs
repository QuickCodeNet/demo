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
    public class FlatsInsertCommand : IRequest<Response<FlatsDto>>
    {
        public FlatsDto request { get; set; }

        public FlatsInsertCommand(FlatsDto request)
        {
            this.request = request;
        }

        public class FlatsInsertHandler : IRequestHandler<FlatsInsertCommand, Response<FlatsDto>>
        {
            private readonly ILogger<FlatsInsertHandler> _logger;
            private readonly IFlatsRepository _repository;
            public FlatsInsertHandler(ILogger<FlatsInsertHandler> logger, IFlatsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<FlatsDto>> Handle(FlatsInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}