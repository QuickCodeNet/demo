using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.IdentityModule.Domain.Entities;
using QuickCode.Demo.IdentityModule.Application.Interfaces.Repositories;
using QuickCode.Demo.IdentityModule.Application.Dtos.ColumnType;
using QuickCode.Demo.IdentityModule.Domain.Enums;

namespace QuickCode.Demo.IdentityModule.Application.Features.ColumnType
{
    public class DeleteItemColumnTypeCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public DeleteItemColumnTypeCommand(int id)
        {
            this.Id = id;
        }

        public class DeleteItemColumnTypeHandler : IRequestHandler<DeleteItemColumnTypeCommand, Response<bool>>
        {
            private readonly ILogger<DeleteItemColumnTypeHandler> _logger;
            private readonly IColumnTypeRepository _repository;
            public DeleteItemColumnTypeHandler(ILogger<DeleteItemColumnTypeHandler> logger, IColumnTypeRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteItemColumnTypeCommand request, CancellationToken cancellationToken)
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