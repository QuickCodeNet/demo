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
    public class FlatsDeleteCommand : IRequest<Response<bool>>
    {
        public FlatsDto request { get; set; }

        public FlatsDeleteCommand(FlatsDto request)
        {
            this.request = request;
        }

        public class FlatsDeleteHandler : IRequestHandler<FlatsDeleteCommand, Response<bool>>
        {
            private readonly ILogger<FlatsDeleteHandler> _logger;
            private readonly IFlatsRepository _repository;
            public FlatsDeleteHandler(ILogger<FlatsDeleteHandler> logger, IFlatsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(FlatsDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}