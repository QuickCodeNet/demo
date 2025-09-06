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
    public class ApartmentFeePlansDeleteCommand : IRequest<Response<bool>>
    {
        public ApartmentFeePlansDto request { get; set; }

        public ApartmentFeePlansDeleteCommand(ApartmentFeePlansDto request)
        {
            this.request = request;
        }

        public class ApartmentFeePlansDeleteHandler : IRequestHandler<ApartmentFeePlansDeleteCommand, Response<bool>>
        {
            private readonly ILogger<ApartmentFeePlansDeleteHandler> _logger;
            private readonly IApartmentFeePlansRepository _repository;
            public ApartmentFeePlansDeleteHandler(ILogger<ApartmentFeePlansDeleteHandler> logger, IApartmentFeePlansRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(ApartmentFeePlansDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}