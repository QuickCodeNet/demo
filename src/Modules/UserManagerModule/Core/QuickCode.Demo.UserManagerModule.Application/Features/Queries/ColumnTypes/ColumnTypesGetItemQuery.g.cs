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
    public class ColumnTypesGetItemQuery : IRequest<Response<ColumnTypesDto>>
    {
        public int Id { get; set; }

        public ColumnTypesGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class ColumnTypesGetItemHandler : IRequestHandler<ColumnTypesGetItemQuery, Response<ColumnTypesDto>>
        {
            private readonly ILogger<ColumnTypesGetItemHandler> _logger;
            private readonly IColumnTypesRepository _repository;
            public ColumnTypesGetItemHandler(ILogger<ColumnTypesGetItemHandler> logger, IColumnTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ColumnTypesDto>> Handle(ColumnTypesGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}