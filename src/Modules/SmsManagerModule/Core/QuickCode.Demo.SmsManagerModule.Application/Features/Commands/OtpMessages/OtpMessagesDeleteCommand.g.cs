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
    public class OtpMessagesDeleteCommand : IRequest<Response<bool>>
    {
        public OtpMessagesDto request { get; set; }

        public OtpMessagesDeleteCommand(OtpMessagesDto request)
        {
            this.request = request;
        }

        public class OtpMessagesDeleteHandler : IRequestHandler<OtpMessagesDeleteCommand, Response<bool>>
        {
            private readonly ILogger<OtpMessagesDeleteHandler> _logger;
            private readonly IOtpMessagesRepository _repository;
            public OtpMessagesDeleteHandler(ILogger<OtpMessagesDeleteHandler> logger, IOtpMessagesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(OtpMessagesDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}