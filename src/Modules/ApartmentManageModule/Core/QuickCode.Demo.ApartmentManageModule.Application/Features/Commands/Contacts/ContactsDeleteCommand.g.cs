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
    public class ContactsDeleteCommand : IRequest<Response<bool>>
    {
        public ContactsDto request { get; set; }

        public ContactsDeleteCommand(ContactsDto request)
        {
            this.request = request;
        }

        public class ContactsDeleteHandler : IRequestHandler<ContactsDeleteCommand, Response<bool>>
        {
            private readonly ILogger<ContactsDeleteHandler> _logger;
            private readonly IContactsRepository _repository;
            public ContactsDeleteHandler(ILogger<ContactsDeleteHandler> logger, IContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(ContactsDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}