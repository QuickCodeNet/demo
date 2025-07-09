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
    public class EmailSendersInsertCommand : IRequest<Response<EmailSendersDto>>
    {
        public EmailSendersDto request { get; set; }

        public EmailSendersInsertCommand(EmailSendersDto request)
        {
            this.request = request;
        }

        public class EmailSendersInsertHandler : IRequestHandler<EmailSendersInsertCommand, Response<EmailSendersDto>>
        {
            private readonly ILogger<EmailSendersInsertHandler> _logger;
            private readonly IEmailSendersRepository _repository;
            public EmailSendersInsertHandler(ILogger<EmailSendersInsertHandler> logger, IEmailSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<EmailSendersDto>> Handle(EmailSendersInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}