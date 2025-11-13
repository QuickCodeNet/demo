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
using QuickCode.Demo.UserManagerModule.Application.Dtos.AspNetUserLogin;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.AspNetUserLogin
{
    public class DeleteAspNetUserLoginCommand : IRequest<Response<bool>>
    {
        public AspNetUserLoginDto request { get; set; }

        public DeleteAspNetUserLoginCommand(AspNetUserLoginDto request)
        {
            this.request = request;
        }

        public class DeleteAspNetUserLoginHandler : IRequestHandler<DeleteAspNetUserLoginCommand, Response<bool>>
        {
            private readonly ILogger<DeleteAspNetUserLoginHandler> _logger;
            private readonly IAspNetUserLoginRepository _repository;
            public DeleteAspNetUserLoginHandler(ILogger<DeleteAspNetUserLoginHandler> logger, IAspNetUserLoginRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteAspNetUserLoginCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}