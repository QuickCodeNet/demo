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
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Extensions;
using QuickCode.Demo.EmailManagerModule.Persistence.Contexts;
using QuickCode.Demo.EmailManagerModule.Application.Dtos;
using QuickCode.Demo.EmailManagerModule.Application.Mappings;

namespace QuickCode.Demo.EmailManagerModule.Persistence.Repositories
{
    public partial class InfoTypesRepository : BaseRepository, IInfoTypesRepository
    {
        private readonly WriteDbContext _writeContext;
        private readonly ReadDbContext _readContext;
        public InfoTypesRepository(ILogger<InfoTypesRepository> logger, WriteDbContext writeContext, ReadDbContext readContext) : base(logger, "InfoTypes")
        {
            _writeContext = writeContext;
            _readContext = readContext;
        }

        public async Task<RepoResponse<InfoTypesDto>> InsertAsync(InfoTypesDto value)
        {
            return await ExecuteWithExceptionHandling("Insert", async () =>
            {
                await _writeContext.InfoTypes.AddAsync(value.ToModel());
                await _writeContext.SaveChangesAsync();
                return new RepoResponse<InfoTypesDto>(value, "Not Defined");
            });
        }

        public async Task<RepoResponse<bool>> UpdateAsync(InfoTypesDto value)
        {
            return await ExecuteWithExceptionHandling("Update", async () =>
            {
                _writeContext.Set<InfoTypes>().Update(value.ToModel());
                await _writeContext.SaveChangesAsync();
                return new RepoResponse<bool>(true, "Success");
            });
        }

        public async Task<RepoResponse<bool>> DeleteAsync(InfoTypesDto value)
        {
            return await ExecuteWithExceptionHandling("Delete", async () =>
            {
                _writeContext.InfoTypes.Remove(value.ToModel());
                await _writeContext.SaveChangesAsync();
                return new RepoResponse<bool>(true, "Success");
            });
        }

        public async Task<RepoResponse<InfoTypesDto>> GetByPkAsync(int id)
        {
            return await ExecuteWithExceptionHandling("GetByPk", async () =>
            {
                var result =
                    from info_types in _readContext.InfoTypes
                    where info_types.Id.Equals(id)
                    select info_types;
                var response = await result.FirstOrDefaultAsync();
                return response == null ? new RepoResponse<InfoTypesDto>
                {
                    Code = 404,
                    Message = "Not found in InfoTypes"
                }

                : new RepoResponse<InfoTypesDto>(response.ToDto(), "Success");
            });
        }

        public async Task<RepoResponse<List<InfoTypesDto>>> ListAsync(int? pageNumber = null, int? pageSize = null)
        {
            const int MinPageNumber = 1;
            return await ExecuteWithExceptionHandling("Select", async () =>
            {
                if (pageNumber.HasValue && pageNumber < MinPageNumber)
                {
                    return new RepoResponse<List<InfoTypesDto>>
                    {
                        Code = 404,
                        Message = $"Page Number must be greater than {MinPageNumber}"};
                }

                var query = _readContext.InfoTypes.AsQueryable();
                if (pageNumber.HasValue && pageSize.HasValue)
                {
                    query = ApplyPagination(query, pageNumber.Value, pageSize.Value);
                }

                var result = await query.ToListAsync();
                return new RepoResponse<List<InfoTypesDto>>(result.ToDto(), "Success");
            });
        }

        public async Task<RepoResponse<int>> CountAsync()
        {
            return await ExecuteWithExceptionHandling("Count", async () =>
            {
                return new RepoResponse<int>(await _readContext.InfoTypes.CountAsync(), "Success");
            });
        }

        public async Task<RepoResponse<List<InfoTypesInfoMessagesRestResponseDto>>> InfoTypesInfoMessagesRestAsync(int infoTypesId)
        {
            return await ExecuteWithExceptionHandling("InfoTypesInfoMessagesRest", async () =>
            {
                var queryableResult =
                    from info_messages in _readContext.InfoMessages
                    join info_types in _readContext.InfoTypes on info_messages.InfoTypeId equals info_types.Id
                    where info_types.Id.Equals(infoTypesId)
                    select new InfoTypesInfoMessagesRestResponseDto()
                    {
                        Id = info_messages.Id,
                        EmailSenderId = info_messages.EmailSenderId,
                        InfoTypeId = info_messages.InfoTypeId,
                        GsmNumber = info_messages.GsmNumber,
                        Message = info_messages.Message,
                        MessageDate = info_messages.MessageDate,
                        MessageSid = info_messages.MessageSid,
                        DailyCounter = info_messages.DailyCounter
                    };
                var result = await queryableResult.ToListAsync();
                return new RepoResponse<List<InfoTypesInfoMessagesRestResponseDto>>(result, "Success");
            });
        }

        public async Task<RepoResponse<InfoTypesInfoMessagesKeyRestResponseDto>> InfoTypesInfoMessagesKeyRestAsync(int infoTypesId, int infoMessagesId)
        {
            return await ExecuteWithExceptionHandling("InfoTypesInfoMessagesKeyRest", async () =>
            {
                var returnValue = new RepoResponse<InfoTypesInfoMessagesKeyRestResponseDto>();
                var queryableResult =
                    from info_messages in _readContext.InfoMessages
                    join info_types in _readContext.InfoTypes on info_messages.InfoTypeId equals info_types.Id
                    where info_types.Id.Equals(infoTypesId) && info_messages.Id.Equals(infoMessagesId)
                    select new InfoTypesInfoMessagesKeyRestResponseDto()
                    {
                        Id = info_messages.Id,
                        EmailSenderId = info_messages.EmailSenderId,
                        InfoTypeId = info_messages.InfoTypeId,
                        GsmNumber = info_messages.GsmNumber,
                        Message = info_messages.Message,
                        MessageDate = info_messages.MessageDate,
                        MessageSid = info_messages.MessageSid,
                        DailyCounter = info_messages.DailyCounter
                    };
                var result = await queryableResult.FirstOrDefaultAsync();
                if (result == null)
                {
                    returnValue.Code = 404;
                    returnValue.Message = $"Not found in InfoTypes";
                }
                else
                {
                    returnValue.Value = result;
                }

                return returnValue;
            });
        }
    }
}