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
    public class FlatContactsListQuery : IRequest<Response<List<FlatContactsDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public FlatContactsListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class FlatContactsListHandler : IRequestHandler<FlatContactsListQuery, Response<List<FlatContactsDto>>>
        {
            private readonly ILogger<FlatContactsListHandler> _logger;
            private readonly IFlatContactsRepository _repository;
            public FlatContactsListHandler(ILogger<FlatContactsListHandler> logger, IFlatContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatContactsDto>>> Handle(FlatContactsListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}