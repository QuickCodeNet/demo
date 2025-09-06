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
    public class OtpMessagesInsertCommand : IRequest<Response<OtpMessagesDto>>
    {
        public OtpMessagesDto request { get; set; }

        public OtpMessagesInsertCommand(OtpMessagesDto request)
        {
            this.request = request;
        }

        public class OtpMessagesInsertHandler : IRequestHandler<OtpMessagesInsertCommand, Response<OtpMessagesDto>>
        {
            private readonly ILogger<OtpMessagesInsertHandler> _logger;
            private readonly IOtpMessagesRepository _repository;
            public OtpMessagesInsertHandler(ILogger<OtpMessagesInsertHandler> logger, IOtpMessagesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<OtpMessagesDto>> Handle(OtpMessagesInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}