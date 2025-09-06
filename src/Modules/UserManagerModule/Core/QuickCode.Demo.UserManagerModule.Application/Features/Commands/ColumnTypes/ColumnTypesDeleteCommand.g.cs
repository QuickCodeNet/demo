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
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class ColumnTypesDeleteCommand : IRequest<Response<bool>>
    {
        public ColumnTypesDto request { get; set; }

        public ColumnTypesDeleteCommand(ColumnTypesDto request)
        {
            this.request = request;
        }

        public class ColumnTypesDeleteHandler : IRequestHandler<ColumnTypesDeleteCommand, Response<bool>>
        {
            private readonly ILogger<ColumnTypesDeleteHandler> _logger;
            private readonly IColumnTypesRepository _repository;
            public ColumnTypesDeleteHandler(ILogger<ColumnTypesDeleteHandler> logger, IColumnTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(ColumnTypesDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}