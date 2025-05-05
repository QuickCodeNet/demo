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
    public class AspNetUserLoginsInsertCommand : IRequest<Response<AspNetUserLoginsDto>>
    {
        public AspNetUserLoginsDto request { get; set; }

        public AspNetUserLoginsInsertCommand(AspNetUserLoginsDto request)
        {
            this.request = request;
        }

        public class AspNetUserLoginsInsertHandler : IRequestHandler<AspNetUserLoginsInsertCommand, Response<AspNetUserLoginsDto>>
        {
            private readonly ILogger<AspNetUserLoginsInsertHandler> _logger;
            private readonly IAspNetUserLoginsRepository _repository;
            public AspNetUserLoginsInsertHandler(ILogger<AspNetUserLoginsInsertHandler> logger, IAspNetUserLoginsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetUserLoginsDto>> Handle(AspNetUserLoginsInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}