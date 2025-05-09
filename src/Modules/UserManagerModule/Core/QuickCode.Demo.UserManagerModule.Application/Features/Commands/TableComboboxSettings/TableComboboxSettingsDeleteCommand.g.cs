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
    public class TableComboboxSettingsDeleteCommand : IRequest<Response<bool>>
    {
        public TableComboboxSettingsDto request { get; set; }

        public TableComboboxSettingsDeleteCommand(TableComboboxSettingsDto request)
        {
            this.request = request;
        }

        public class TableComboboxSettingsDeleteHandler : IRequestHandler<TableComboboxSettingsDeleteCommand, Response<bool>>
        {
            private readonly ILogger<TableComboboxSettingsDeleteHandler> _logger;
            private readonly ITableComboboxSettingsRepository _repository;
            public TableComboboxSettingsDeleteHandler(ILogger<TableComboboxSettingsDeleteHandler> logger, ITableComboboxSettingsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(TableComboboxSettingsDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}