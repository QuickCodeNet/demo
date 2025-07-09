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
    public class InfoMessagesDeleteItemCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public InfoMessagesDeleteItemCommand(int id)
        {
            this.Id = id;
        }

        public class InfoMessagesDeleteItemHandler : IRequestHandler<InfoMessagesDeleteItemCommand, Response<bool>>
        {
            private readonly ILogger<InfoMessagesDeleteItemHandler> _logger;
            private readonly IInfoMessagesRepository _repository;
            public InfoMessagesDeleteItemHandler(ILogger<InfoMessagesDeleteItemHandler> logger, IInfoMessagesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(InfoMessagesDeleteItemCommand request, CancellationToken cancellationToken)
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