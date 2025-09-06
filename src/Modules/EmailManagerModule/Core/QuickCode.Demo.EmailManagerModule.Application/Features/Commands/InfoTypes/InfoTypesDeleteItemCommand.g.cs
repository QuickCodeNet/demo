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
    public class InfoTypesDeleteItemCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public InfoTypesDeleteItemCommand(int id)
        {
            this.Id = id;
        }

        public class InfoTypesDeleteItemHandler : IRequestHandler<InfoTypesDeleteItemCommand, Response<bool>>
        {
            private readonly ILogger<InfoTypesDeleteItemHandler> _logger;
            private readonly IInfoTypesRepository _repository;
            public InfoTypesDeleteItemHandler(ILogger<InfoTypesDeleteItemHandler> logger, IInfoTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(InfoTypesDeleteItemCommand request, CancellationToken cancellationToken)
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