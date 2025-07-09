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
    public class TableComboboxSettingsUpdateCommand : IRequest<Response<bool>>
    {
        public string TableName { get; set; }
        public TableComboboxSettingsDto request { get; set; }

        public TableComboboxSettingsUpdateCommand(string tableName, TableComboboxSettingsDto request)
        {
            this.request = request;
            this.TableName = tableName;
        }

        public class TableComboboxSettingsUpdateHandler : IRequestHandler<TableComboboxSettingsUpdateCommand, Response<bool>>
        {
            private readonly ILogger<TableComboboxSettingsUpdateHandler> _logger;
            private readonly ITableComboboxSettingsRepository _repository;
            public TableComboboxSettingsUpdateHandler(ILogger<TableComboboxSettingsUpdateHandler> logger, ITableComboboxSettingsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(TableComboboxSettingsUpdateCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.TableName);
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