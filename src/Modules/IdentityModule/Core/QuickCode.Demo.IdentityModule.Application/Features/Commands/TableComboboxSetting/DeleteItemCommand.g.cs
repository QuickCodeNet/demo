using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.IdentityModule.Domain.Entities;
using QuickCode.Demo.IdentityModule.Application.Interfaces.Repositories;
using QuickCode.Demo.IdentityModule.Application.Dtos.TableComboboxSetting;
using QuickCode.Demo.IdentityModule.Domain.Enums;

namespace QuickCode.Demo.IdentityModule.Application.Features.TableComboboxSetting
{
    public class DeleteItemTableComboboxSettingCommand : IRequest<Response<bool>>
    {
        public string TableName { get; set; }

        public DeleteItemTableComboboxSettingCommand(string tableName)
        {
            this.TableName = tableName;
        }

        public class DeleteItemTableComboboxSettingHandler : IRequestHandler<DeleteItemTableComboboxSettingCommand, Response<bool>>
        {
            private readonly ILogger<DeleteItemTableComboboxSettingHandler> _logger;
            private readonly ITableComboboxSettingRepository _repository;
            public DeleteItemTableComboboxSettingHandler(ILogger<DeleteItemTableComboboxSettingHandler> logger, ITableComboboxSettingRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteItemTableComboboxSettingCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.TableName);
                if (deleteItem.Code == 404)
                    return Response<bool>.NotFound();
                var returnValue = await _repository.DeleteAsync(deleteItem.Value);
                return returnValue.ToResponse();
            }
        }
    }
}