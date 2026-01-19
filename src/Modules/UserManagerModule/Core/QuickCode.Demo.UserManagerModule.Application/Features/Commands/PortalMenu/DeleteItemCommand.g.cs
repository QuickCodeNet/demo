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
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalMenu;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.PortalMenu
{
    public class DeleteItemPortalMenuCommand : IRequest<Response<bool>>
    {
        public string Key { get; set; }

        public DeleteItemPortalMenuCommand(string key)
        {
            this.Key = key;
        }

        public class DeleteItemPortalMenuHandler : IRequestHandler<DeleteItemPortalMenuCommand, Response<bool>>
        {
            private readonly ILogger<DeleteItemPortalMenuHandler> _logger;
            private readonly IPortalMenuRepository _repository;
            public DeleteItemPortalMenuHandler(ILogger<DeleteItemPortalMenuHandler> logger, IPortalMenuRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteItemPortalMenuCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.Key);
                if (deleteItem.Code == 404)
                    return Response<bool>.NotFound();
                var returnValue = await _repository.DeleteAsync(deleteItem.Value);
                return returnValue.ToResponse();
            }
        }
    }
}