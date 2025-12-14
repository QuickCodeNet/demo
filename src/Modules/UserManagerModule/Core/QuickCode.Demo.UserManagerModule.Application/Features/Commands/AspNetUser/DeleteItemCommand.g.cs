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
using QuickCode.Demo.UserManagerModule.Application.Dtos.AspNetUser;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.AspNetUser
{
    public class DeleteItemAspNetUserCommand : IRequest<Response<bool>>
    {
        public string Id { get; set; }

        public DeleteItemAspNetUserCommand(string id)
        {
            this.Id = id;
        }

        public class DeleteItemAspNetUserHandler : IRequestHandler<DeleteItemAspNetUserCommand, Response<bool>>
        {
            private readonly ILogger<DeleteItemAspNetUserHandler> _logger;
            private readonly IAspNetUserRepository _repository;
            public DeleteItemAspNetUserHandler(ILogger<DeleteItemAspNetUserHandler> logger, IAspNetUserRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteItemAspNetUserCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.Id);
                if (deleteItem.Code == 404)
                    return Response<bool>.NotFound();
                var returnValue = await _repository.DeleteAsync(deleteItem.Value);
                return returnValue.ToResponse();
            }
        }
    }
}