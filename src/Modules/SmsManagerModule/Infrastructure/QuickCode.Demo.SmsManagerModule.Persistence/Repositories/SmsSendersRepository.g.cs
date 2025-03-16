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
    public partial class SmsSendersRepository : ISmsSendersRepository
    {
        private readonly WriteDbContext _writeContext;
        private readonly ReadDbContext _readContext;
        private readonly ILogger<SmsSendersRepository> _logger;
        public SmsSendersRepository(ILogger<SmsSendersRepository> logger, WriteDbContext writeContext, ReadDbContext readContext)
        {
            _writeContext = writeContext;
            _readContext = readContext;
            _logger = logger;
        }

        public async Task<RepoResponse<SmsSenders>> InsertAsync(SmsSenders value)
        {
            try
            {
                await _writeContext.SmsSenders.AddAsync(value);
                await _writeContext.SaveChangesAsync();
                return new RepoResponse<SmsSenders>(value, "Not Defined");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<SmsSenders>(ex, "SmsSenders", "Insert");
            }
        }

        public async Task<RepoResponse<bool>> UpdateAsync(SmsSenders value)
        {
            try
            {
                _writeContext.Set<SmsSenders>().Update(value);
                await _writeContext.SaveChangesAsync();
                return new RepoResponse<bool>(true, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<bool>(ex, "SmsSenders", "Update");
            }
        }

        public async Task<RepoResponse<bool>> DeleteAsync(SmsSenders value)
        {
            try
            {
                _writeContext.SmsSenders.Remove(value);
                await _writeContext.SaveChangesAsync();
                return new RepoResponse<bool>(true, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<bool>(ex, "SmsSenders", "Delete");
            }
        }

        public async Task<RepoResponse<SmsSenders>> GetByPkAsync(int id)
        {
            try
            {
                var result =
                    from sms_senders in _readContext.SmsSenders
                    where sms_senders.Id.Equals(id)select sms_senders;
                var response = await result.FirstOrDefaultAsync();
                return response == null ? new RepoResponse<SmsSenders>
                {
                    Code = 404,
                    Message = "Not found in SmsSenders"
                }

                : new RepoResponse<SmsSenders>(response, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<SmsSenders>(ex, "SmsSenders", "GetByPk");
            }
        }

        public async Task<RepoResponse<List<SmsSenders>>> ListAsync(int? pageNumber = null, int? pageSize = null)
        {
            try
            {
                if (pageNumber.HasValue && pageNumber < 1)
                {
                    return new RepoResponse<List<SmsSenders>>
                    {
                        Code = 404,
                        Message = "Page Number must be greater than 1"
                    };
                }

                var query = _readContext.SmsSenders.AsQueryable();
                if (pageNumber.HasValue && pageSize.HasValue)
                {
                    query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
                }

                return new RepoResponse<List<SmsSenders>>(await query.ToListAsync(), "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<List<SmsSenders>>(ex, "SmsSenders", "List");
            }
        }

        public async Task<RepoResponse<int>> CountAsync()
        {
            try
            {
                return new RepoResponse<int>(await _readContext.SmsSenders.CountAsync(), "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<int>(ex, "SmsSenders", "Count");
            }
        }

        public async Task<RepoResponse<List<SmsSendersInfoMessagesRestResponseDto>>> SmsSendersInfoMessagesRestAsync(int smsSendersId)
        {
            try
            {
                var queryableResult =
                    from info_messages in _readContext.InfoMessages
                    join sms_senders in _readContext.SmsSenders on info_messages.SmsSenderId equals sms_senders.Id
                    where sms_senders.Id.Equals(smsSendersId)select new SmsSendersInfoMessagesRestResponseDto()
                    {
                        Id = info_messages.Id,
                        SmsSenderId = info_messages.SmsSenderId,
                        InfoTypeId = info_messages.InfoTypeId,
                        GsmNumber = info_messages.GsmNumber,
                        Message = info_messages.Message,
                        MessageDate = info_messages.MessageDate,
                        MessageSid = info_messages.MessageSid,
                        DailyCounter = info_messages.DailyCounter
                    };
                var result = await queryableResult.ToListAsync();
                return new RepoResponse<List<SmsSendersInfoMessagesRestResponseDto>>(result, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<List<SmsSendersInfoMessagesRestResponseDto>>(ex, "SmsSenders", "SmsSendersInfoMessagesRest");
            }
        }

        public async Task<RepoResponse<SmsSendersInfoMessagesKeyRestResponseDto>> SmsSendersInfoMessagesKeyRestAsync(int smsSendersId, int infoMessagesId)
        {
            try
            {
                var returnValue = new RepoResponse<SmsSendersInfoMessagesKeyRestResponseDto>();
                var queryableResult =
                    from info_messages in _readContext.InfoMessages
                    join sms_senders in _readContext.SmsSenders on info_messages.SmsSenderId equals sms_senders.Id
                    where sms_senders.Id.Equals(smsSendersId) && info_messages.Id.Equals(infoMessagesId)select new SmsSendersInfoMessagesKeyRestResponseDto()
                    {
                        Id = info_messages.Id,
                        SmsSenderId = info_messages.SmsSenderId,
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
                    returnValue.Message = $"Not found in SmsSenders";
                }
                else
                {
                    returnValue.Value = result;
                }

                return returnValue;
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<SmsSendersInfoMessagesKeyRestResponseDto>(ex, "SmsSenders", "SmsSendersInfoMessagesKeyRest");
            }
        }

        public async Task<RepoResponse<List<SmsSendersOtpMessagesRestResponseDto>>> SmsSendersOtpMessagesRestAsync(int smsSendersId)
        {
            try
            {
                var queryableResult =
                    from otp_messages in _readContext.OtpMessages
                    join sms_senders in _readContext.SmsSenders on otp_messages.SmsSenderId equals sms_senders.Id
                    where sms_senders.Id.Equals(smsSendersId)select new SmsSendersOtpMessagesRestResponseDto()
                    {
                        Id = otp_messages.Id,
                        SmsSenderId = otp_messages.SmsSenderId,
                        OtpTypeId = otp_messages.OtpTypeId,
                        GsmNumber = otp_messages.GsmNumber,
                        OtpCode = otp_messages.OtpCode,
                        Message = otp_messages.Message,
                        ExpireSeconds = otp_messages.ExpireSeconds,
                        MessageDate = otp_messages.MessageDate,
                        MessageSid = otp_messages.MessageSid,
                        DailyCounter = otp_messages.DailyCounter
                    };
                var result = await queryableResult.ToListAsync();
                return new RepoResponse<List<SmsSendersOtpMessagesRestResponseDto>>(result, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<List<SmsSendersOtpMessagesRestResponseDto>>(ex, "SmsSenders", "SmsSendersOtpMessagesRest");
            }
        }

        public async Task<RepoResponse<SmsSendersOtpMessagesKeyRestResponseDto>> SmsSendersOtpMessagesKeyRestAsync(int smsSendersId, int otpMessagesId)
        {
            try
            {
                var returnValue = new RepoResponse<SmsSendersOtpMessagesKeyRestResponseDto>();
                var queryableResult =
                    from otp_messages in _readContext.OtpMessages
                    join sms_senders in _readContext.SmsSenders on otp_messages.SmsSenderId equals sms_senders.Id
                    where sms_senders.Id.Equals(smsSendersId) && otp_messages.Id.Equals(otpMessagesId)select new SmsSendersOtpMessagesKeyRestResponseDto()
                    {
                        Id = otp_messages.Id,
                        SmsSenderId = otp_messages.SmsSenderId,
                        OtpTypeId = otp_messages.OtpTypeId,
                        GsmNumber = otp_messages.GsmNumber,
                        OtpCode = otp_messages.OtpCode,
                        Message = otp_messages.Message,
                        ExpireSeconds = otp_messages.ExpireSeconds,
                        MessageDate = otp_messages.MessageDate,
                        MessageSid = otp_messages.MessageSid,
                        DailyCounter = otp_messages.DailyCounter
                    };
                var result = await queryableResult.FirstOrDefaultAsync();
                if (result == null)
                {
                    returnValue.Code = 404;
                    returnValue.Message = $"Not found in SmsSenders";
                }
                else
                {
                    returnValue.Value = result;
                }

                return returnValue;
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<SmsSendersOtpMessagesKeyRestResponseDto>(ex, "SmsSenders", "SmsSendersOtpMessagesKeyRest");
            }
        }

        public async Task<RepoResponse<List<SmsSendersCampaignMessagesRestResponseDto>>> SmsSendersCampaignMessagesRestAsync(int smsSendersId)
        {
            try
            {
                var queryableResult =
                    from campaign_messages in _readContext.CampaignMessages
                    join sms_senders in _readContext.SmsSenders on campaign_messages.SmsSenderId equals sms_senders.Id
                    where sms_senders.Id.Equals(smsSendersId)select new SmsSendersCampaignMessagesRestResponseDto()
                    {
                        Id = campaign_messages.Id,
                        SmsSenderId = campaign_messages.SmsSenderId,
                        CampaignTypeId = campaign_messages.CampaignTypeId,
                        GsmNumber = campaign_messages.GsmNumber,
                        Message = campaign_messages.Message,
                        MessageDate = campaign_messages.MessageDate,
                        MessageSid = campaign_messages.MessageSid,
                        DailyCounter = campaign_messages.DailyCounter
                    };
                var result = await queryableResult.ToListAsync();
                return new RepoResponse<List<SmsSendersCampaignMessagesRestResponseDto>>(result, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<List<SmsSendersCampaignMessagesRestResponseDto>>(ex, "SmsSenders", "SmsSendersCampaignMessagesRest");
            }
        }

        public async Task<RepoResponse<SmsSendersCampaignMessagesKeyRestResponseDto>> SmsSendersCampaignMessagesKeyRestAsync(int smsSendersId, int campaignMessagesId)
        {
            try
            {
                var returnValue = new RepoResponse<SmsSendersCampaignMessagesKeyRestResponseDto>();
                var queryableResult =
                    from campaign_messages in _readContext.CampaignMessages
                    join sms_senders in _readContext.SmsSenders on campaign_messages.SmsSenderId equals sms_senders.Id
                    where sms_senders.Id.Equals(smsSendersId) && campaign_messages.Id.Equals(campaignMessagesId)select new SmsSendersCampaignMessagesKeyRestResponseDto()
                    {
                        Id = campaign_messages.Id,
                        SmsSenderId = campaign_messages.SmsSenderId,
                        CampaignTypeId = campaign_messages.CampaignTypeId,
                        GsmNumber = campaign_messages.GsmNumber,
                        Message = campaign_messages.Message,
                        MessageDate = campaign_messages.MessageDate,
                        MessageSid = campaign_messages.MessageSid,
                        DailyCounter = campaign_messages.DailyCounter
                    };
                var result = await queryableResult.FirstOrDefaultAsync();
                if (result == null)
                {
                    returnValue.Code = 404;
                    returnValue.Message = $"Not found in SmsSenders";
                }
                else
                {
                    returnValue.Value = result;
                }

                return returnValue;
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<SmsSendersCampaignMessagesKeyRestResponseDto>(ex, "SmsSenders", "SmsSendersCampaignMessagesKeyRest");
            }
        }
    }
}