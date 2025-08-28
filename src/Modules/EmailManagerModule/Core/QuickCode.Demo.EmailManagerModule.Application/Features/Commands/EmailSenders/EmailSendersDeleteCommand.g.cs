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
    public class EmailSendersDeleteCommand : IRequest<Response<bool>>
    {
        public EmailSendersDto request { get; set; }

        public EmailSendersDeleteCommand(EmailSendersDto request)
        {
            this.request = request;
        }

        public class EmailSendersDeleteHandler : IRequestHandler<EmailSendersDeleteCommand, Response<bool>>
        {
            private readonly ILogger<EmailSendersDeleteHandler> _logger;
            private readonly IEmailSendersRepository _repository;
            public EmailSendersDeleteHandler(ILogger<EmailSendersDeleteHandler> logger, IEmailSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(EmailSendersDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}