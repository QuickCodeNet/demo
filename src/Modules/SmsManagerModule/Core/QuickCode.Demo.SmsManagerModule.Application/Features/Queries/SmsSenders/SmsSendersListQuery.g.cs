using System.Linq;
using QuickCode.Demo.Common.Mediator;
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
    public class SmsSendersListQuery : IRequest<Response<List<SmsSendersDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public SmsSendersListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class SmsSendersListHandler : IRequestHandler<SmsSendersListQuery, Response<List<SmsSendersDto>>>
        {
            private readonly ILogger<SmsSendersListHandler> _logger;
            private readonly ISmsSendersRepository _repository;
            public SmsSendersListHandler(ILogger<SmsSendersListHandler> logger, ISmsSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<SmsSendersDto>>> Handle(SmsSendersListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}