using System.Linq;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class ColumnTypesListQuery : IRequest<Response<List<ColumnTypesDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public ColumnTypesListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class ColumnTypesListHandler : IRequestHandler<ColumnTypesListQuery, Response<List<ColumnTypesDto>>>
        {
            private readonly ILogger<ColumnTypesListHandler> _logger;
            private readonly IColumnTypesRepository _repository;
            public ColumnTypesListHandler(ILogger<ColumnTypesListHandler> logger, IColumnTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ColumnTypesDto>>> Handle(ColumnTypesListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}