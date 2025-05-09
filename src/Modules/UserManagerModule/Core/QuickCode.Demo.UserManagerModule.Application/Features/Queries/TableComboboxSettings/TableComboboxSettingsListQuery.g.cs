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
    public class TableComboboxSettingsListQuery : IRequest<Response<List<TableComboboxSettingsDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public TableComboboxSettingsListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class TableComboboxSettingsListHandler : IRequestHandler<TableComboboxSettingsListQuery, Response<List<TableComboboxSettingsDto>>>
        {
            private readonly ILogger<TableComboboxSettingsListHandler> _logger;
            private readonly ITableComboboxSettingsRepository _repository;
            public TableComboboxSettingsListHandler(ILogger<TableComboboxSettingsListHandler> logger, ITableComboboxSettingsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<TableComboboxSettingsDto>>> Handle(TableComboboxSettingsListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}