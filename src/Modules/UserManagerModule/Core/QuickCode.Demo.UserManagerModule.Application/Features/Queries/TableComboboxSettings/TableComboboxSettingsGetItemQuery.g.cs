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
    public class TableComboboxSettingsGetItemQuery : IRequest<Response<TableComboboxSettingsDto>>
    {
        public string TableName { get; set; }

        public TableComboboxSettingsGetItemQuery(string tableName)
        {
            this.TableName = tableName;
        }

        public class TableComboboxSettingsGetItemHandler : IRequestHandler<TableComboboxSettingsGetItemQuery, Response<TableComboboxSettingsDto>>
        {
            private readonly ILogger<TableComboboxSettingsGetItemHandler> _logger;
            private readonly ITableComboboxSettingsRepository _repository;
            public TableComboboxSettingsGetItemHandler(ILogger<TableComboboxSettingsGetItemHandler> logger, ITableComboboxSettingsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<TableComboboxSettingsDto>> Handle(TableComboboxSettingsGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.TableName);
                return returnValue.ToResponse();
            }
        }
    }
}