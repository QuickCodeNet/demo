using System.Linq;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class ColumnTypesDeleteItemCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public ColumnTypesDeleteItemCommand(int id)
        {
            this.Id = id;
        }

        public class ColumnTypesDeleteItemHandler : IRequestHandler<ColumnTypesDeleteItemCommand, Response<bool>>
        {
            private readonly ILogger<ColumnTypesDeleteItemHandler> _logger;
            private readonly IColumnTypesRepository _repository;
            public ColumnTypesDeleteItemHandler(ILogger<ColumnTypesDeleteItemHandler> logger, IColumnTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(ColumnTypesDeleteItemCommand request, CancellationToken cancellationToken)
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