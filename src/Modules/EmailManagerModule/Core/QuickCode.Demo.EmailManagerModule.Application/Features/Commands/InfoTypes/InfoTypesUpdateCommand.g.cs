using System.Linq;
using MediatR;
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
    public class InfoTypesUpdateCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public InfoTypesDto request { get; set; }

        public InfoTypesUpdateCommand(int id, InfoTypesDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class InfoTypesUpdateHandler : IRequestHandler<InfoTypesUpdateCommand, Response<bool>>
        {
            private readonly ILogger<InfoTypesUpdateHandler> _logger;
            private readonly IInfoTypesRepository _repository;
            public InfoTypesUpdateHandler(ILogger<InfoTypesUpdateHandler> logger, IInfoTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(InfoTypesUpdateCommand request, CancellationToken cancellationToken)
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