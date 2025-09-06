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
    public class ApartmentsDeleteCommand : IRequest<Response<bool>>
    {
        public ApartmentsDto request { get; set; }

        public ApartmentsDeleteCommand(ApartmentsDto request)
        {
            this.request = request;
        }

        public class ApartmentsDeleteHandler : IRequestHandler<ApartmentsDeleteCommand, Response<bool>>
        {
            private readonly ILogger<ApartmentsDeleteHandler> _logger;
            private readonly IApartmentsRepository _repository;
            public ApartmentsDeleteHandler(ILogger<ApartmentsDeleteHandler> logger, IApartmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(ApartmentsDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}