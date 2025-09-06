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
    public class ApartmentsGetItemQuery : IRequest<Response<ApartmentsDto>>
    {
        public int Id { get; set; }

        public ApartmentsGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class ApartmentsGetItemHandler : IRequestHandler<ApartmentsGetItemQuery, Response<ApartmentsDto>>
        {
            private readonly ILogger<ApartmentsGetItemHandler> _logger;
            private readonly IApartmentsRepository _repository;
            public ApartmentsGetItemHandler(ILogger<ApartmentsGetItemHandler> logger, IApartmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ApartmentsDto>> Handle(ApartmentsGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}