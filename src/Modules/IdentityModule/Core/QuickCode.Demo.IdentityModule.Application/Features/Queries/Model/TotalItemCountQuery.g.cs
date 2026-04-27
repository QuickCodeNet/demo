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
using QuickCode.Demo.IdentityModule.Application.Dtos.Model;
using QuickCode.Demo.IdentityModule.Domain.Enums;

namespace QuickCode.Demo.IdentityModule.Application.Features.Model
{
    public class TotalCountModelQuery : IRequest<Response<int>>
    {
        public TotalCountModelQuery()
        {
        }

        public class TotalCountModelHandler : IRequestHandler<TotalCountModelQuery, Response<int>>
        {
            private readonly ILogger<TotalCountModelHandler> _logger;
            private readonly IModelRepository _repository;
            public TotalCountModelHandler(ILogger<TotalCountModelHandler> logger, IModelRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(TotalCountModelQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}