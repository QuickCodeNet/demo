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
    public class InfoTypesListQuery : IRequest<Response<List<InfoTypesDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public InfoTypesListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class InfoTypesListHandler : IRequestHandler<InfoTypesListQuery, Response<List<InfoTypesDto>>>
        {
            private readonly ILogger<InfoTypesListHandler> _logger;
            private readonly IInfoTypesRepository _repository;
            public InfoTypesListHandler(ILogger<InfoTypesListHandler> logger, IInfoTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<InfoTypesDto>>> Handle(InfoTypesListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}