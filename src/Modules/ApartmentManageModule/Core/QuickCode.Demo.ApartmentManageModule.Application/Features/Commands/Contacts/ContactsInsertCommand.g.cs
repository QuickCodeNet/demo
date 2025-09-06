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
    public class ContactsInsertCommand : IRequest<Response<ContactsDto>>
    {
        public ContactsDto request { get; set; }

        public ContactsInsertCommand(ContactsDto request)
        {
            this.request = request;
        }

        public class ContactsInsertHandler : IRequestHandler<ContactsInsertCommand, Response<ContactsDto>>
        {
            private readonly ILogger<ContactsInsertHandler> _logger;
            private readonly IContactsRepository _repository;
            public ContactsInsertHandler(ILogger<ContactsInsertHandler> logger, IContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ContactsDto>> Handle(ContactsInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}