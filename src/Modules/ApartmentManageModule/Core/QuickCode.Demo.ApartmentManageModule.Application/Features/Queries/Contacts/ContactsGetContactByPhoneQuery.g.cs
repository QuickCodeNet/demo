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
    public class ContactsGetContactByPhoneQuery : IRequest<Response<ContactsGetContactByPhoneResponseDto>>
    {
        public string ContactsPhone { get; set; }

        public ContactsGetContactByPhoneQuery(string contactsPhone)
        {
            this.ContactsPhone = contactsPhone;
        }

        public class ContactsGetContactByPhoneHandler : IRequestHandler<ContactsGetContactByPhoneQuery, Response<ContactsGetContactByPhoneResponseDto>>
        {
            private readonly ILogger<ContactsGetContactByPhoneHandler> _logger;
            private readonly IContactsRepository _repository;
            public ContactsGetContactByPhoneHandler(ILogger<ContactsGetContactByPhoneHandler> logger, IContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ContactsGetContactByPhoneResponseDto>> Handle(ContactsGetContactByPhoneQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ContactsGetContactByPhoneAsync(request.ContactsPhone);
                return returnValue.ToResponse();
            }
        }
    }
}