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
    public class ApartmentFeePlansInsertCommand : IRequest<Response<ApartmentFeePlansDto>>
    {
        public ApartmentFeePlansDto request { get; set; }

        public ApartmentFeePlansInsertCommand(ApartmentFeePlansDto request)
        {
            this.request = request;
        }

        public class ApartmentFeePlansInsertHandler : IRequestHandler<ApartmentFeePlansInsertCommand, Response<ApartmentFeePlansDto>>
        {
            private readonly ILogger<ApartmentFeePlansInsertHandler> _logger;
            private readonly IApartmentFeePlansRepository _repository;
            public ApartmentFeePlansInsertHandler(ILogger<ApartmentFeePlansInsertHandler> logger, IApartmentFeePlansRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ApartmentFeePlansDto>> Handle(ApartmentFeePlansInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}