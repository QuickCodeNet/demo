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
    public class FeeTypesGetItemQuery : IRequest<Response<FeeTypesDto>>
    {
        public int Id { get; set; }

        public FeeTypesGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class FeeTypesGetItemHandler : IRequestHandler<FeeTypesGetItemQuery, Response<FeeTypesDto>>
        {
            private readonly ILogger<FeeTypesGetItemHandler> _logger;
            private readonly IFeeTypesRepository _repository;
            public FeeTypesGetItemHandler(ILogger<FeeTypesGetItemHandler> logger, IFeeTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<FeeTypesDto>> Handle(FeeTypesGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}