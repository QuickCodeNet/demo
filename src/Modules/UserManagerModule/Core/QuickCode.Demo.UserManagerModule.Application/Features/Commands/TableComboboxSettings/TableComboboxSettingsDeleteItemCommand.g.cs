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
    public class TableComboboxSettingsDeleteItemCommand : IRequest<Response<bool>>
    {
        public string TableName { get; set; }

        public TableComboboxSettingsDeleteItemCommand(string tableName)
        {
            this.TableName = tableName;
        }

        public class TableComboboxSettingsDeleteItemHandler : IRequestHandler<TableComboboxSettingsDeleteItemCommand, Response<bool>>
        {
            private readonly ILogger<TableComboboxSettingsDeleteItemHandler> _logger;
            private readonly ITableComboboxSettingsRepository _repository;
            public TableComboboxSettingsDeleteItemHandler(ILogger<TableComboboxSettingsDeleteItemHandler> logger, ITableComboboxSettingsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(TableComboboxSettingsDeleteItemCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.TableName);
                if (deleteItem.Code == 404)
                {
                    return new Response<bool>()
                    {
                        Code = 404,
                        Value = false
                    };
                }

                var returnValue = await _repository.DeleteAsync(deleteItem.Value);
                return returnValue.ToResponse();
            }
        }
    }
}