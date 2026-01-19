using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos.TableComboboxSetting;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.TableComboboxSetting
{
    public class InsertTableComboboxSettingCommand : IRequest<Response<TableComboboxSettingDto>>
    {
        public TableComboboxSettingDto request { get; set; }

        public InsertTableComboboxSettingCommand(TableComboboxSettingDto request)
        {
            this.request = request;
        }

        public class InsertTableComboboxSettingHandler : IRequestHandler<InsertTableComboboxSettingCommand, Response<TableComboboxSettingDto>>
        {
            private readonly ILogger<InsertTableComboboxSettingHandler> _logger;
            private readonly ITableComboboxSettingRepository _repository;
            public InsertTableComboboxSettingHandler(ILogger<InsertTableComboboxSettingHandler> logger, ITableComboboxSettingRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<TableComboboxSettingDto>> Handle(InsertTableComboboxSettingCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}