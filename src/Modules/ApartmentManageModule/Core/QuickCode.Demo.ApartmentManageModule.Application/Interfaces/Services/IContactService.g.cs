using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.Contact;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Services.Contact
{
    public partial interface IContactService
    {
        Task<Response<ContactDto>> InsertAsync(ContactDto request);
        Task<Response<bool>> DeleteAsync(ContactDto request);
        Task<Response<bool>> UpdateAsync(int id, ContactDto request);
        Task<Response<List<ContactDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<ContactDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetActiveContactsResponseDto>>> GetActiveContactsAsync(bool contactsIsActive);
        Task<Response<GetContactByIdResponseDto>> GetContactByIdAsync(int contactsId);
        Task<Response<GetContactByPhoneResponseDto>> GetContactByPhoneAsync(string contactsPhone);
        Task<Response<GetContactByEmailResponseDto>> GetContactByEmailAsync(string? contactsEmail);
        Task<Response<GetContactByIdentityResponseDto>> GetContactByIdentityAsync(string? contactsIdentityNumber);
        Task<Response<bool>> CheckContactByPhoneAsync(string contactsPhone);
        Task<Response<bool>> CheckContactByEmailAsync(string? contactsEmail);
        Task<Response<long>> GetActiveContactsCountAsync(bool contactsIsActive);
        Task<Response<List<GetContactsWithPagerResponseDto>>> GetContactsWithPagerAsync(bool contactsIsActive, int? page, int? size);
        Task<Response<List<GetFlatContactsForContactsResponseDto>>> GetFlatContactsForContactsAsync(int contactsId);
        Task<Response<GetFlatContactsForContactsResponseDto>> GetFlatContactsForContactsDetailsAsync(int contactsId, int flatContactsId);
        Task<Response<List<GetSiteManagersForContactsResponseDto>>> GetSiteManagersForContactsAsync(int contactsId);
        Task<Response<GetSiteManagersForContactsResponseDto>> GetSiteManagersForContactsDetailsAsync(int contactsId, int siteManagersId);
    }
}