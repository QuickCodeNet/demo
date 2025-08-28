using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Features
{
    public class ApartmentFeePlansDeleteItemCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public ApartmentFeePlansDeleteItemCommand(int id)
        {
            this.Id = id;
        }

        public class ApartmentFeePlansDeleteItemHandler : IRequestHandler<ApartmentFeePlansDeleteItemCommand, Response<bool>>
        {
            private readonly ILogger<ApartmentFeePlansDeleteItemHandler> _logger;
            private readonly IApartmentFeePlansRepository _repository;
            public ApartmentFeePlansDeleteItemHandler(ILogger<ApartmentFeePlansDeleteItemHandler> logger, IApartmentFeePlansRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(ApartmentFeePlansDeleteItemCommand request, CancellationToken cancellationToken)
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