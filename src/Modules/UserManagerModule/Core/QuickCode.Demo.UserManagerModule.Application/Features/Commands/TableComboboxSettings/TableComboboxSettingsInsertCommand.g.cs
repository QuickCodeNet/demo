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
    public class TableComboboxSettingsInsertCommand : IRequest<Response<TableComboboxSettingsDto>>
    {
        public TableComboboxSettingsDto request { get; set; }

        public TableComboboxSettingsInsertCommand(TableComboboxSettingsDto request)
        {
            this.request = request;
        }

        public class TableComboboxSettingsInsertHandler : IRequestHandler<TableComboboxSettingsInsertCommand, Response<TableComboboxSettingsDto>>
        {
            private readonly ILogger<TableComboboxSettingsInsertHandler> _logger;
            private readonly ITableComboboxSettingsRepository _repository;
            public TableComboboxSettingsInsertHandler(ILogger<TableComboboxSettingsInsertHandler> logger, ITableComboboxSettingsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<TableComboboxSettingsDto>> Handle(TableComboboxSettingsInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}