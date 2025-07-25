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
    public class FeeTypesInsertCommand : IRequest<Response<FeeTypesDto>>
    {
        public FeeTypesDto request { get; set; }

        public FeeTypesInsertCommand(FeeTypesDto request)
        {
            this.request = request;
        }

        public class FeeTypesInsertHandler : IRequestHandler<FeeTypesInsertCommand, Response<FeeTypesDto>>
        {
            private readonly ILogger<FeeTypesInsertHandler> _logger;
            private readonly IFeeTypesRepository _repository;
            public FeeTypesInsertHandler(ILogger<FeeTypesInsertHandler> logger, IFeeTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<FeeTypesDto>> Handle(FeeTypesInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}