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
    public class ColumnTypesUpdateCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public ColumnTypesDto request { get; set; }

        public ColumnTypesUpdateCommand(int id, ColumnTypesDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class ColumnTypesUpdateHandler : IRequestHandler<ColumnTypesUpdateCommand, Response<bool>>
        {
            private readonly ILogger<ColumnTypesUpdateHandler> _logger;
            private readonly IColumnTypesRepository _repository;
            public ColumnTypesUpdateHandler(ILogger<ColumnTypesUpdateHandler> logger, IColumnTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(ColumnTypesUpdateCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.Id);
                if (updateItem.Code == 404)
                {
                    return new Response<bool>()
                    {
                        Code = 404,
                        Value = false
                    };
                }

                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}