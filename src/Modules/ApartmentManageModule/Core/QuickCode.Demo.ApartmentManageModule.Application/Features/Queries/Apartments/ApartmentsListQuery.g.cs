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
    public class ApartmentsListQuery : IRequest<Response<List<ApartmentsDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public ApartmentsListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class ApartmentsListHandler : IRequestHandler<ApartmentsListQuery, Response<List<ApartmentsDto>>>
        {
            private readonly ILogger<ApartmentsListHandler> _logger;
            private readonly IApartmentsRepository _repository;
            public ApartmentsListHandler(ILogger<ApartmentsListHandler> logger, IApartmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ApartmentsDto>>> Handle(ApartmentsListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}