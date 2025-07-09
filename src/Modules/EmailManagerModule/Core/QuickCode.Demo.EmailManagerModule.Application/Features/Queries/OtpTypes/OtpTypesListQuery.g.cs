using System.Linq;
using QuickCode.Demo.Common.Mediator;
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
    public class OtpTypesListQuery : IRequest<Response<List<OtpTypesDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public OtpTypesListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class OtpTypesListHandler : IRequestHandler<OtpTypesListQuery, Response<List<OtpTypesDto>>>
        {
            private readonly ILogger<OtpTypesListHandler> _logger;
            private readonly IOtpTypesRepository _repository;
            public OtpTypesListHandler(ILogger<OtpTypesListHandler> logger, IOtpTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<OtpTypesDto>>> Handle(OtpTypesListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}