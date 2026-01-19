using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.SmsManagerModule.Domain.Entities;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.SmsManagerModule.Application.Dtos.Sender;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.Sender
{
    public class DeleteItemSenderCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public DeleteItemSenderCommand(int id)
        {
            this.Id = id;
        }

        public class DeleteItemSenderHandler : IRequestHandler<DeleteItemSenderCommand, Response<bool>>
        {
            private readonly ILogger<DeleteItemSenderHandler> _logger;
            private readonly ISenderRepository _repository;
            public DeleteItemSenderHandler(ILogger<DeleteItemSenderHandler> logger, ISenderRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteItemSenderCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.Id);
                if (deleteItem.Code == 404)
                    return Response<bool>.NotFound();
                var returnValue = await _repository.DeleteAsync(deleteItem.Value);
                return returnValue.ToResponse();
            }
        }
    }
}