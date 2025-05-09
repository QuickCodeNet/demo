using System.Linq;
using MediatR;
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
    public class InfoMessagesUpdateCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public InfoMessagesDto request { get; set; }

        public InfoMessagesUpdateCommand(int id, InfoMessagesDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class InfoMessagesUpdateHandler : IRequestHandler<InfoMessagesUpdateCommand, Response<bool>>
        {
            private readonly ILogger<InfoMessagesUpdateHandler> _logger;
            private readonly IInfoMessagesRepository _repository;
            public InfoMessagesUpdateHandler(ILogger<InfoMessagesUpdateHandler> logger, IInfoMessagesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(InfoMessagesUpdateCommand request, CancellationToken cancellationToken)
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