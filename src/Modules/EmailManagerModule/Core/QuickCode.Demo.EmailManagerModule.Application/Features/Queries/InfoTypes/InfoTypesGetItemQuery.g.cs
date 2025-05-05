using System.Linq;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos;

namespace QuickCode.Demo.EmailManagerModule.Application.Features
{
    public class InfoTypesGetItemQuery : IRequest<Response<InfoTypesDto>>
    {
        public int Id { get; set; }

        public InfoTypesGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class InfoTypesGetItemHandler : IRequestHandler<InfoTypesGetItemQuery, Response<InfoTypesDto>>
        {
            private readonly ILogger<InfoTypesGetItemHandler> _logger;
            private readonly IInfoTypesRepository _repository;
            public InfoTypesGetItemHandler(ILogger<InfoTypesGetItemHandler> logger, IInfoTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<InfoTypesDto>> Handle(InfoTypesGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}