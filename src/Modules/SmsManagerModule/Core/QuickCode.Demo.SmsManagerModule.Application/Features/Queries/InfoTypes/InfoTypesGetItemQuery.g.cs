using System.Linq;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.SmsManagerModule.Domain.Entities;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.SmsManagerModule.Application.Dtos;

namespace QuickCode.Demo.SmsManagerModule.Application.Features
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