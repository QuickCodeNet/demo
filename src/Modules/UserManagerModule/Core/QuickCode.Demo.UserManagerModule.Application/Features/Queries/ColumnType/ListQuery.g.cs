using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos.ColumnType;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.ColumnType
{
    public class ListColumnTypeQuery : IRequest<Response<List<ColumnTypeDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public ListColumnTypeQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class ListColumnTypeHandler : IRequestHandler<ListColumnTypeQuery, Response<List<ColumnTypeDto>>>
        {
            private readonly ILogger<ListColumnTypeHandler> _logger;
            private readonly IColumnTypeRepository _repository;
            public ListColumnTypeHandler(ILogger<ListColumnTypeHandler> logger, IColumnTypeRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ColumnTypeDto>>> Handle(ListColumnTypeQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}