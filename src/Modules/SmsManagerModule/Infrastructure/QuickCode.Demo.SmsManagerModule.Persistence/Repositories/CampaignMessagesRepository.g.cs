//------------------------------------------------------------------------------ 
// <auto-generated> 
// This code was generated by QuickCode. 
// Runtime Version:1.0
// 
// Changes to this file may cause incorrect behavior and will be lost if 
// the code is regenerated. 
// </auto-generated> 
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.SmsManagerModule.Domain.Entities;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Extensions;
using QuickCode.Demo.SmsManagerModule.Persistence.Contexts;
using QuickCode.Demo.SmsManagerModule.Application.Dtos;

namespace QuickCode.Demo.SmsManagerModule.Persistence.Repositories
{
    public partial class CampaignMessagesRepository : ICampaignMessagesRepository
    {
        private readonly WriteDbContext _writeContext;
        private readonly ReadDbContext _readContext;
        private readonly ILogger<CampaignMessagesRepository> _logger;
        public CampaignMessagesRepository(ILogger<CampaignMessagesRepository> logger, WriteDbContext writeContext, ReadDbContext readContext)
        {
            _writeContext = writeContext;
            _readContext = readContext;
            _logger = logger;
        }

        public async Task<RepoResponse<CampaignMessages>> InsertAsync(CampaignMessages value)
        {
            try
            {
                await _writeContext.CampaignMessages.AddAsync(value);
                await _writeContext.SaveChangesAsync();
                return new RepoResponse<CampaignMessages>(value, "Not Defined");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<CampaignMessages>(ex, "CampaignMessages", "Insert");
            }
        }

        public async Task<RepoResponse<bool>> UpdateAsync(CampaignMessages value)
        {
            try
            {
                _writeContext.Set<CampaignMessages>().Update(value);
                await _writeContext.SaveChangesAsync();
                return new RepoResponse<bool>(true, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<bool>(ex, "CampaignMessages", "Update");
            }
        }

        public async Task<RepoResponse<bool>> DeleteAsync(CampaignMessages value)
        {
            try
            {
                _writeContext.CampaignMessages.Remove(value);
                await _writeContext.SaveChangesAsync();
                return new RepoResponse<bool>(true, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<bool>(ex, "CampaignMessages", "Delete");
            }
        }

        public async Task<RepoResponse<CampaignMessages>> GetByPkAsync(int id)
        {
            try
            {
                var result =
                    from campaign_messages in _readContext.CampaignMessages
                    where campaign_messages.Id.Equals(id)select campaign_messages;
                var response = await result.FirstOrDefaultAsync();
                return response == null ? new RepoResponse<CampaignMessages>
                {
                    Code = 404,
                    Message = "Not found in CampaignMessages"
                }

                : new RepoResponse<CampaignMessages>(response, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<CampaignMessages>(ex, "CampaignMessages", "GetByPk");
            }
        }

        public async Task<RepoResponse<List<CampaignMessages>>> ListAsync(int? pageNumber = null, int? pageSize = null)
        {
            try
            {
                if (pageNumber.HasValue && pageNumber < 1)
                {
                    return new RepoResponse<List<CampaignMessages>>
                    {
                        Code = 404,
                        Message = "Page Number must be greater than 1"
                    };
                }

                var query = _readContext.CampaignMessages.AsQueryable();
                if (pageNumber.HasValue && pageSize.HasValue)
                {
                    query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
                }

                return new RepoResponse<List<CampaignMessages>>(await query.ToListAsync(), "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<List<CampaignMessages>>(ex, "CampaignMessages", "List");
            }
        }

        public async Task<RepoResponse<int>> CountAsync()
        {
            try
            {
                return new RepoResponse<int>(await _readContext.CampaignMessages.CountAsync(), "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<int>(ex, "CampaignMessages", "Count");
            }
        }
    }
}