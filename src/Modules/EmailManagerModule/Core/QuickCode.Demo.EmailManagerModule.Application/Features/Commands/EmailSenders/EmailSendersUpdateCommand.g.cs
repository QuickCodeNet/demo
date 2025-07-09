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
    public class EmailSendersUpdateCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public EmailSendersDto request { get; set; }

        public EmailSendersUpdateCommand(int id, EmailSendersDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class EmailSendersUpdateHandler : IRequestHandler<EmailSendersUpdateCommand, Response<bool>>
        {
            private readonly ILogger<EmailSendersUpdateHandler> _logger;
            private readonly IEmailSendersRepository _repository;
            public EmailSendersUpdateHandler(ILogger<EmailSendersUpdateHandler> logger, IEmailSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(EmailSendersUpdateCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.Id);
                if (updateItem.Code == 404)
                {
                    return new Response<bool>()
                    {
                        Code = 404,
                        Value = false
                    };
                }

                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}