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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.OtpMessage;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.OtpMessage
{
    public class DeleteItemOtpMessageCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public DeleteItemOtpMessageCommand(int id)
        {
            this.Id = id;
        }

        public class DeleteItemOtpMessageHandler : IRequestHandler<DeleteItemOtpMessageCommand, Response<bool>>
        {
            private readonly ILogger<DeleteItemOtpMessageHandler> _logger;
            private readonly IOtpMessageRepository _repository;
            public DeleteItemOtpMessageHandler(ILogger<DeleteItemOtpMessageHandler> logger, IOtpMessageRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteItemOtpMessageCommand request, CancellationToken cancellationToken)
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