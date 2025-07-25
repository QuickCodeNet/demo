using System;
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
    public class ContactsCheckContactByPhoneQuery : IRequest<Response<bool>>
    {
        public string ContactsPhone { get; set; }

        public ContactsCheckContactByPhoneQuery(string contactsPhone)
        {
            this.ContactsPhone = contactsPhone;
        }

        public class ContactsCheckContactByPhoneHandler : IRequestHandler<ContactsCheckContactByPhoneQuery, Response<bool>>
        {
            private readonly ILogger<ContactsCheckContactByPhoneHandler> _logger;
            private readonly IContactsRepository _repository;
            public ContactsCheckContactByPhoneHandler(ILogger<ContactsCheckContactByPhoneHandler> logger, IContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(ContactsCheckContactByPhoneQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ContactsCheckContactByPhoneAsync(request.ContactsPhone);
                return returnValue.ToResponse();
            }
        }
    }
}