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
    public class SmsSendersInsertCommand : IRequest<Response<SmsSendersDto>>
    {
        public SmsSendersDto request { get; set; }

        public SmsSendersInsertCommand(SmsSendersDto request)
        {
            this.request = request;
        }

        public class SmsSendersInsertHandler : IRequestHandler<SmsSendersInsertCommand, Response<SmsSendersDto>>
        {
            private readonly ILogger<SmsSendersInsertHandler> _logger;
            private readonly ISmsSendersRepository _repository;
            public SmsSendersInsertHandler(ILogger<SmsSendersInsertHandler> logger, ISmsSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<SmsSendersDto>> Handle(SmsSendersInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}