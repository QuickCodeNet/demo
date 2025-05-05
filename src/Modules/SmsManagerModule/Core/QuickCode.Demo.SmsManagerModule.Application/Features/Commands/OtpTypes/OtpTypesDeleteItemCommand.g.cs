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
    public class OtpTypesDeleteItemCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public OtpTypesDeleteItemCommand(int id)
        {
            this.Id = id;
        }

        public class OtpTypesDeleteItemHandler : IRequestHandler<OtpTypesDeleteItemCommand, Response<bool>>
        {
            private readonly ILogger<OtpTypesDeleteItemHandler> _logger;
            private readonly IOtpTypesRepository _repository;
            public OtpTypesDeleteItemHandler(ILogger<OtpTypesDeleteItemHandler> logger, IOtpTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(OtpTypesDeleteItemCommand request, CancellationToken cancellationToken)
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