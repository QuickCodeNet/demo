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
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Extensions;
using QuickCode.Demo.EmailManagerModule.Persistence.Contexts;
using QuickCode.Demo.EmailManagerModule.Application.Dtos;

namespace QuickCode.Demo.EmailManagerModule.Persistence.Repositories
{
    public partial class InfoMessagesRepository : IInfoMessagesRepository
    {
        private readonly WriteDbContext _writeContext;
        private readonly ReadDbContext _readContext;
        private readonly ILogger<InfoMessagesRepository> _logger;
        public InfoMessagesRepository(ILogger<InfoMessagesRepository> logger, WriteDbContext writeContext, ReadDbContext readContext)
        {
            _writeContext = writeContext;
            _readContext = readContext;
            _logger = logger;
        }

        public async Task<RepoResponse<InfoMessages>> InsertAsync(InfoMessages value)
        {
            try
            {
                await _writeContext.InfoMessages.AddAsync(value);
                await _writeContext.SaveChangesAsync();
                return new RepoResponse<InfoMessages>(value, "Not Defined");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<InfoMessages>(ex, "InfoMessages", "Insert");
            }
        }

        public async Task<RepoResponse<bool>> UpdateAsync(InfoMessages value)
        {
            try
            {
                _writeContext.Set<InfoMessages>().Update(value);
                await _writeContext.SaveChangesAsync();
                return new RepoResponse<bool>(true, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<bool>(ex, "InfoMessages", "Update");
            }
        }

        public async Task<RepoResponse<bool>> DeleteAsync(InfoMessages value)
        {
            try
            {
                _writeContext.InfoMessages.Remove(value);
                await _writeContext.SaveChangesAsync();
                return new RepoResponse<bool>(true, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<bool>(ex, "InfoMessages", "Delete");
            }
        }

        public async Task<RepoResponse<InfoMessages>> GetByPkAsync(int id)
        {
            try
            {
                var result =
                    from info_messages in _readContext.InfoMessages
                    where info_messages.Id.Equals(id)select info_messages;
                var response = await result.FirstOrDefaultAsync();
                return response == null ? new RepoResponse<InfoMessages>
                {
                    Code = 404,
                    Message = "Not found in InfoMessages"
                }

                : new RepoResponse<InfoMessages>(response, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<InfoMessages>(ex, "InfoMessages", "GetByPk");
            }
        }

        public async Task<RepoResponse<List<InfoMessages>>> ListAsync(int? pageNumber = null, int? pageSize = null)
        {
            try
            {
                if (pageNumber.HasValue && pageNumber < 1)
                {
                    return new RepoResponse<List<InfoMessages>>
                    {
                        Code = 404,
                        Message = "Page Number must be greater than 1"
                    };
                }

                var query = _readContext.InfoMessages.AsQueryable();
                if (pageNumber.HasValue && pageSize.HasValue)
                {
                    query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
                }

                return new RepoResponse<List<InfoMessages>>(await query.ToListAsync(), "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<List<InfoMessages>>(ex, "InfoMessages", "List");
            }
        }

        public async Task<RepoResponse<int>> CountAsync()
        {
            try
            {
                return new RepoResponse<int>(await _readContext.InfoMessages.CountAsync(), "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<int>(ex, "InfoMessages", "Count");
            }
        }
    }
}