using System.Linq;
using QuickCode.Demo.Common.Mediator;
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
    public class ColumnTypesInsertCommand : IRequest<Response<ColumnTypesDto>>
    {
        public ColumnTypesDto request { get; set; }

        public ColumnTypesInsertCommand(ColumnTypesDto request)
        {
            this.request = request;
        }

        public class ColumnTypesInsertHandler : IRequestHandler<ColumnTypesInsertCommand, Response<ColumnTypesDto>>
        {
            private readonly ILogger<ColumnTypesInsertHandler> _logger;
            private readonly IColumnTypesRepository _repository;
            public ColumnTypesInsertHandler(ILogger<ColumnTypesInsertHandler> logger, IColumnTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ColumnTypesDto>> Handle(ColumnTypesInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}