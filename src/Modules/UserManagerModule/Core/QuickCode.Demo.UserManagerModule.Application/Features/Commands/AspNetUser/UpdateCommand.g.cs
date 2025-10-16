﻿using System.Linq;
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
    public class UpdateAspNetUserCommand : IRequest<Response<bool>>
    {
        public string Id { get; set; }
        public AspNetUserDto request { get; set; }

        public UpdateAspNetUserCommand(string id, AspNetUserDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class UpdateAspNetUserHandler : IRequestHandler<UpdateAspNetUserCommand, Response<bool>>
        {
            private readonly ILogger<UpdateAspNetUserHandler> _logger;
            private readonly IAspNetUserRepository _repository;
            public UpdateAspNetUserHandler(ILogger<UpdateAspNetUserHandler> logger, IAspNetUserRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(UpdateAspNetUserCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.Id);
                if (updateItem.Code == 404)
                    return Response<bool>.NotFound();
                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}