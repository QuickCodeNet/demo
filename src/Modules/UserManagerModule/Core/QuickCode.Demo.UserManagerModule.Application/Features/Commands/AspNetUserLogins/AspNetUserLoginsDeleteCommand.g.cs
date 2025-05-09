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
    public class AspNetUserLoginsDeleteCommand : IRequest<Response<bool>>
    {
        public AspNetUserLoginsDto request { get; set; }

        public AspNetUserLoginsDeleteCommand(AspNetUserLoginsDto request)
        {
            this.request = request;
        }

        public class AspNetUserLoginsDeleteHandler : IRequestHandler<AspNetUserLoginsDeleteCommand, Response<bool>>
        {
            private readonly ILogger<AspNetUserLoginsDeleteHandler> _logger;
            private readonly IAspNetUserLoginsRepository _repository;
            public AspNetUserLoginsDeleteHandler(ILogger<AspNetUserLoginsDeleteHandler> logger, IAspNetUserLoginsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(AspNetUserLoginsDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}