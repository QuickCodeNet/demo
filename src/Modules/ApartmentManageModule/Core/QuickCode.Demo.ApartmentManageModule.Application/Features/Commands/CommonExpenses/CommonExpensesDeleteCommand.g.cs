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
    public class CommonExpensesDeleteCommand : IRequest<Response<bool>>
    {
        public CommonExpensesDto request { get; set; }

        public CommonExpensesDeleteCommand(CommonExpensesDto request)
        {
            this.request = request;
        }

        public class CommonExpensesDeleteHandler : IRequestHandler<CommonExpensesDeleteCommand, Response<bool>>
        {
            private readonly ILogger<CommonExpensesDeleteHandler> _logger;
            private readonly ICommonExpensesRepository _repository;
            public CommonExpensesDeleteHandler(ILogger<CommonExpensesDeleteHandler> logger, ICommonExpensesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(CommonExpensesDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}