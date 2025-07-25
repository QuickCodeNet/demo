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
    public class ContactsCheckContactByEmailQuery : IRequest<Response<bool>>
    {
        public string? ContactsEmail { get; set; }

        public ContactsCheckContactByEmailQuery(string? contactsEmail)
        {
            this.ContactsEmail = contactsEmail;
        }

        public class ContactsCheckContactByEmailHandler : IRequestHandler<ContactsCheckContactByEmailQuery, Response<bool>>
        {
            private readonly ILogger<ContactsCheckContactByEmailHandler> _logger;
            private readonly IContactsRepository _repository;
            public ContactsCheckContactByEmailHandler(ILogger<ContactsCheckContactByEmailHandler> logger, IContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(ContactsCheckContactByEmailQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ContactsCheckContactByEmailAsync(request.ContactsEmail);
                return returnValue.ToResponse();
            }
        }
    }
}