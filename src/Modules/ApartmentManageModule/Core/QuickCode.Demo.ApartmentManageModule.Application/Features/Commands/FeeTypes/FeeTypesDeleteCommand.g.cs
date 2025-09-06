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
    public class FeeTypesDeleteCommand : IRequest<Response<bool>>
    {
        public FeeTypesDto request { get; set; }

        public FeeTypesDeleteCommand(FeeTypesDto request)
        {
            this.request = request;
        }

        public class FeeTypesDeleteHandler : IRequestHandler<FeeTypesDeleteCommand, Response<bool>>
        {
            private readonly ILogger<FeeTypesDeleteHandler> _logger;
            private readonly IFeeTypesRepository _repository;
            public FeeTypesDeleteHandler(ILogger<FeeTypesDeleteHandler> logger, IFeeTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(FeeTypesDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}