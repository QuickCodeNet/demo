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
    public class ContactsGetSiteManagersForContactsDetailsQuery : IRequest<Response<ContactsGetSiteManagersForContactsResponseDto>>
    {
        public int ContactsId { get; set; }
        public int SiteManagersId { get; set; }

        public ContactsGetSiteManagersForContactsDetailsQuery(int contactsId, int siteManagersId)
        {
            this.ContactsId = contactsId;
            this.SiteManagersId = siteManagersId;
        }

        public class ContactsGetSiteManagersForContactsDetailsHandler : IRequestHandler<ContactsGetSiteManagersForContactsDetailsQuery, Response<ContactsGetSiteManagersForContactsResponseDto>>
        {
            private readonly ILogger<ContactsGetSiteManagersForContactsDetailsHandler> _logger;
            private readonly IContactsRepository _repository;
            public ContactsGetSiteManagersForContactsDetailsHandler(ILogger<ContactsGetSiteManagersForContactsDetailsHandler> logger, IContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ContactsGetSiteManagersForContactsResponseDto>> Handle(ContactsGetSiteManagersForContactsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ContactsGetSiteManagersForContactsDetailsAsync(request.ContactsId, request.SiteManagersId);
                return returnValue.ToResponse();
            }
        }
    }
}