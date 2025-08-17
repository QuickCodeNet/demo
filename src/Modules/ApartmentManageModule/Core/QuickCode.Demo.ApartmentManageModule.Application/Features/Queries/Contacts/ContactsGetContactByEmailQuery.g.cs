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
    public class ContactsGetContactByEmailQuery : IRequest<Response<ContactsGetContactByEmailResponseDto>>
    {
        public string? ContactsEmail { get; set; }

        public ContactsGetContactByEmailQuery(string? contactsEmail)
        {
            this.ContactsEmail = contactsEmail;
        }

        public class ContactsGetContactByEmailHandler : IRequestHandler<ContactsGetContactByEmailQuery, Response<ContactsGetContactByEmailResponseDto>>
        {
            private readonly ILogger<ContactsGetContactByEmailHandler> _logger;
            private readonly IContactsRepository _repository;
            public ContactsGetContactByEmailHandler(ILogger<ContactsGetContactByEmailHandler> logger, IContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ContactsGetContactByEmailResponseDto>> Handle(ContactsGetContactByEmailQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ContactsGetContactByEmailAsync(request.ContactsEmail);
                return returnValue.ToResponse();
            }
        }
    }
}