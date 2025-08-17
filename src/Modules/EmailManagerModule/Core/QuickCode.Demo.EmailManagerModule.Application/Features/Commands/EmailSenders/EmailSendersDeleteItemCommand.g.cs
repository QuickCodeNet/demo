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
    public class EmailSendersDeleteItemCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public EmailSendersDeleteItemCommand(int id)
        {
            this.Id = id;
        }

        public class EmailSendersDeleteItemHandler : IRequestHandler<EmailSendersDeleteItemCommand, Response<bool>>
        {
            private readonly ILogger<EmailSendersDeleteItemHandler> _logger;
            private readonly IEmailSendersRepository _repository;
            public EmailSendersDeleteItemHandler(ILogger<EmailSendersDeleteItemHandler> logger, IEmailSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(EmailSendersDeleteItemCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.Id);
                if (deleteItem.Code == 404)
                {
                    return new Response<bool>()
                    {
                        Code = 404,
                        Value = false
                    };
                }

                var returnValue = await _repository.DeleteAsync(deleteItem.Value);
                return returnValue.ToResponse();
            }
        }
    }
}