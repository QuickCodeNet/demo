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
    public class ApartmentFeePlansListQuery : IRequest<Response<List<ApartmentFeePlansDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public ApartmentFeePlansListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class ApartmentFeePlansListHandler : IRequestHandler<ApartmentFeePlansListQuery, Response<List<ApartmentFeePlansDto>>>
        {
            private readonly ILogger<ApartmentFeePlansListHandler> _logger;
            private readonly IApartmentFeePlansRepository _repository;
            public ApartmentFeePlansListHandler(ILogger<ApartmentFeePlansListHandler> logger, IApartmentFeePlansRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ApartmentFeePlansDto>>> Handle(ApartmentFeePlansListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}