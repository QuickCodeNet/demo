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
    public partial class ContactService : IContactService
    {
        private readonly ILogger<ContactService> _logger;
        private readonly IContactRepository _repository;
        public ContactService(ILogger<ContactService> logger, IContactRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<ContactDto>> InsertAsync(ContactDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(ContactDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, ContactDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<ContactDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<ContactDto>> GetItemAsync(int id)
        {
            var returnValue = await _repository.GetByPkAsync(id);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteItemAsync(int id)
        {
            var deleteItem = await _repository.GetByPkAsync(id);
            if (deleteItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.DeleteAsync(deleteItem.Value);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> TotalItemCountAsync()
        {
            var returnValue = await _repository.CountAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetActiveContactsResponseDto>>> GetActiveContactsAsync(bool contactsIsActive)
        {
            var returnValue = await _repository.GetActiveContactsAsync(contactsIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetContactByIdResponseDto>> GetContactByIdAsync(int contactsId)
        {
            var returnValue = await _repository.GetContactByIdAsync(contactsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetContactByPhoneResponseDto>> GetContactByPhoneAsync(string contactsPhone)
        {
            var returnValue = await _repository.GetContactByPhoneAsync(contactsPhone);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetContactByEmailResponseDto>> GetContactByEmailAsync(string? contactsEmail)
        {
            var returnValue = await _repository.GetContactByEmailAsync(contactsEmail);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetContactByIdentityResponseDto>> GetContactByIdentityAsync(string? contactsIdentityNumber)
        {
            var returnValue = await _repository.GetContactByIdentityAsync(contactsIdentityNumber);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> CheckContactByPhoneAsync(string contactsPhone)
        {
            var returnValue = await _repository.CheckContactByPhoneAsync(contactsPhone);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> CheckContactByEmailAsync(string? contactsEmail)
        {
            var returnValue = await _repository.CheckContactByEmailAsync(contactsEmail);
            return returnValue.ToResponse();
        }

        public async Task<Response<long>> GetActiveContactsCountAsync(bool contactsIsActive)
        {
            var returnValue = await _repository.GetActiveContactsCountAsync(contactsIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetContactsWithPagerResponseDto>>> GetContactsWithPagerAsync(bool contactsIsActive, int? page, int? size)
        {
            var returnValue = await _repository.GetContactsWithPagerAsync(contactsIsActive, page, size);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatContactsForContactsResponseDto>>> GetFlatContactsForContactsAsync(int contactsId)
        {
            var returnValue = await _repository.GetFlatContactsForContactsAsync(contactsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetFlatContactsForContactsResponseDto>> GetFlatContactsForContactsDetailsAsync(int contactsId, int flatContactsId)
        {
            var returnValue = await _repository.GetFlatContactsForContactsDetailsAsync(contactsId, flatContactsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetSiteManagersForContactsResponseDto>>> GetSiteManagersForContactsAsync(int contactsId)
        {
            var returnValue = await _repository.GetSiteManagersForContactsAsync(contactsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetSiteManagersForContactsResponseDto>> GetSiteManagersForContactsDetailsAsync(int contactsId, int siteManagersId)
        {
            var returnValue = await _repository.GetSiteManagersForContactsDetailsAsync(contactsId, siteManagersId);
            return returnValue.ToResponse();
        }
    }
}