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
    public class FlatPaymentsGetItemQuery : IRequest<Response<FlatPaymentsDto>>
    {
        public int Id { get; set; }

        public FlatPaymentsGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class FlatPaymentsGetItemHandler : IRequestHandler<FlatPaymentsGetItemQuery, Response<FlatPaymentsDto>>
        {
            private readonly ILogger<FlatPaymentsGetItemHandler> _logger;
            private readonly IFlatPaymentsRepository _repository;
            public FlatPaymentsGetItemHandler(ILogger<FlatPaymentsGetItemHandler> logger, IFlatPaymentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<FlatPaymentsDto>> Handle(FlatPaymentsGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}