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
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Extensions;
using QuickCode.Demo.UserManagerModule.Persistence.Contexts;
using QuickCode.Demo.UserManagerModule.Application.Dtos;

namespace QuickCode.Demo.UserManagerModule.Persistence.Repositories
{
    public partial class AspNetUsersRepository : IAspNetUsersRepository
    {
        private readonly WriteDbContext _writeContext;
        private readonly ReadDbContext _readContext;
        private readonly ILogger<AspNetUsersRepository> _logger;
        public AspNetUsersRepository(ILogger<AspNetUsersRepository> logger, WriteDbContext writeContext, ReadDbContext readContext)
        {
            _writeContext = writeContext;
            _readContext = readContext;
            _logger = logger;
        }

        public async Task<RepoResponse<AspNetUsers>> InsertAsync(AspNetUsers value)
        {
            try
            {
                await _writeContext.AspNetUsers.AddAsync(value);
                await _writeContext.SaveChangesAsync();
                return new RepoResponse<AspNetUsers>(value, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<AspNetUsers>(ex, "AspNetUsers", "Insert");
            }
        }

        public async Task<RepoResponse<bool>> UpdateAsync(AspNetUsers value)
        {
            try
            {
                _writeContext.Set<AspNetUsers>().Update(value);
                await _writeContext.SaveChangesAsync();
                return new RepoResponse<bool>(true, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<bool>(ex, "AspNetUsers", "Update");
            }
        }

        public async Task<RepoResponse<bool>> DeleteAsync(AspNetUsers value)
        {
            try
            {
                _writeContext.AspNetUsers.Remove(value);
                await _writeContext.SaveChangesAsync();
                return new RepoResponse<bool>(true, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<bool>(ex, "AspNetUsers", "Delete");
            }
        }

        public async Task<RepoResponse<AspNetUsers>> GetByPkAsync(string id)
        {
            try
            {
                var result =
                    from asp_net_users in _readContext.AspNetUsers
                    where asp_net_users.Id.Equals(id)select asp_net_users;
                var response = await result.FirstOrDefaultAsync();
                return response == null ? new RepoResponse<AspNetUsers>
                {
                    Code = 404,
                    Message = "Not found in AspNetUsers"
                }

                : new RepoResponse<AspNetUsers>(response, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<AspNetUsers>(ex, "AspNetUsers", "GetByPk");
            }
        }

        public async Task<RepoResponse<List<AspNetUsers>>> ListAsync(int? pageNumber = null, int? pageSize = null)
        {
            try
            {
                if (pageNumber.HasValue && pageNumber < 1)
                {
                    return new RepoResponse<List<AspNetUsers>>
                    {
                        Code = 404,
                        Message = "Page Number must be greater than 1"
                    };
                }

                var query = _readContext.AspNetUsers.AsQueryable();
                if (pageNumber.HasValue && pageSize.HasValue)
                {
                    query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
                }

                return new RepoResponse<List<AspNetUsers>>(await query.ToListAsync(), "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<List<AspNetUsers>>(ex, "AspNetUsers", "List");
            }
        }

        public async Task<RepoResponse<int>> CountAsync()
        {
            try
            {
                return new RepoResponse<int>(await _readContext.AspNetUsers.CountAsync(), "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<int>(ex, "AspNetUsers", "Count");
            }
        }

        public async Task<RepoResponse<AspNetUsersGetUserResponseDto>> AspNetUsersGetUserAsync(string? aspNetUsersEmail)
        {
            try
            {
                var returnValue = new RepoResponse<AspNetUsersGetUserResponseDto>();
                var queryableResult =
                    from asp_net_users in _readContext.AspNetUsers
                    where asp_net_users.Email.Equals(aspNetUsersEmail)select new AspNetUsersGetUserResponseDto()
                    {
                        Id = asp_net_users.Id,
                        FirstName = asp_net_users.FirstName,
                        LastName = asp_net_users.LastName,
                        PermissionGroupId = asp_net_users.PermissionGroupId,
                        UserName = asp_net_users.UserName,
                        NormalizedUserName = asp_net_users.NormalizedUserName,
                        Email = asp_net_users.Email,
                        NormalizedEmail = asp_net_users.NormalizedEmail,
                        EmailConfirmed = asp_net_users.EmailConfirmed,
                        PasswordHash = asp_net_users.PasswordHash,
                        SecurityStamp = asp_net_users.SecurityStamp,
                        ConcurrencyStamp = asp_net_users.ConcurrencyStamp,
                        PhoneNumber = asp_net_users.PhoneNumber,
                        PhoneNumberConfirmed = asp_net_users.PhoneNumberConfirmed,
                        TwoFactorEnabled = asp_net_users.TwoFactorEnabled,
                        LockoutEnd = asp_net_users.LockoutEnd,
                        LockoutEnabled = asp_net_users.LockoutEnabled,
                        AccessFailedCount = asp_net_users.AccessFailedCount
                    };
                var result = await queryableResult.FirstOrDefaultAsync();
                if (result == null)
                {
                    returnValue.Code = 404;
                    returnValue.Message = $"Not found in AspNetUsers";
                }
                else
                {
                    returnValue.Value = result;
                }

                return returnValue;
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<AspNetUsersGetUserResponseDto>(ex, "AspNetUsers", "AspNetUsersGetUser");
            }
        }

        public async Task<RepoResponse<List<AspNetUsersAspNetUserRoles_RESTResponseDto>>> AspNetUsersAspNetUserRoles_RESTAsync(string aspNetUsersId)
        {
            try
            {
                var queryableResult =
                    from asp_net_user_roles in _readContext.AspNetUserRoles
                    join asp_net_users in _readContext.AspNetUsers on asp_net_user_roles.UserId equals asp_net_users.Id
                    where asp_net_users.Id.Equals(aspNetUsersId)select new AspNetUsersAspNetUserRoles_RESTResponseDto()
                    {
                        UserId = asp_net_user_roles.UserId,
                        RoleId = asp_net_user_roles.RoleId
                    };
                var result = await queryableResult.ToListAsync();
                return new RepoResponse<List<AspNetUsersAspNetUserRoles_RESTResponseDto>>(result, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<List<AspNetUsersAspNetUserRoles_RESTResponseDto>>(ex, "AspNetUsers", "AspNetUsersAspNetUserRoles_REST");
            }
        }

        public async Task<RepoResponse<AspNetUsersAspNetUserRoles_KEY_RESTResponseDto>> AspNetUsersAspNetUserRoles_KEY_RESTAsync(string aspNetUsersId, string aspNetUserRolesUserId)
        {
            try
            {
                var returnValue = new RepoResponse<AspNetUsersAspNetUserRoles_KEY_RESTResponseDto>();
                var queryableResult =
                    from asp_net_user_roles in _readContext.AspNetUserRoles
                    join asp_net_users in _readContext.AspNetUsers on asp_net_user_roles.UserId equals asp_net_users.Id
                    where asp_net_users.Id.Equals(aspNetUsersId) && asp_net_user_roles.UserId.Equals(aspNetUserRolesUserId)select new AspNetUsersAspNetUserRoles_KEY_RESTResponseDto()
                    {
                        UserId = asp_net_user_roles.UserId,
                        RoleId = asp_net_user_roles.RoleId
                    };
                var result = await queryableResult.FirstOrDefaultAsync();
                if (result == null)
                {
                    returnValue.Code = 404;
                    returnValue.Message = $"Not found in AspNetUsers";
                }
                else
                {
                    returnValue.Value = result;
                }

                return returnValue;
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<AspNetUsersAspNetUserRoles_KEY_RESTResponseDto>(ex, "AspNetUsers", "AspNetUsersAspNetUserRoles_KEY_REST");
            }
        }

        public async Task<RepoResponse<List<AspNetUsersAspNetUserClaims_RESTResponseDto>>> AspNetUsersAspNetUserClaims_RESTAsync(string aspNetUsersId)
        {
            try
            {
                var queryableResult =
                    from asp_net_user_claims in _readContext.AspNetUserClaims
                    join asp_net_users in _readContext.AspNetUsers on asp_net_user_claims.UserId equals asp_net_users.Id
                    where asp_net_users.Id.Equals(aspNetUsersId)select new AspNetUsersAspNetUserClaims_RESTResponseDto()
                    {
                        Id = asp_net_user_claims.Id,
                        UserId = asp_net_user_claims.UserId,
                        ClaimType = asp_net_user_claims.ClaimType,
                        ClaimValue = asp_net_user_claims.ClaimValue
                    };
                var result = await queryableResult.ToListAsync();
                return new RepoResponse<List<AspNetUsersAspNetUserClaims_RESTResponseDto>>(result, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<List<AspNetUsersAspNetUserClaims_RESTResponseDto>>(ex, "AspNetUsers", "AspNetUsersAspNetUserClaims_REST");
            }
        }

        public async Task<RepoResponse<AspNetUsersAspNetUserClaims_KEY_RESTResponseDto>> AspNetUsersAspNetUserClaims_KEY_RESTAsync(string aspNetUsersId, int aspNetUserClaimsId)
        {
            try
            {
                var returnValue = new RepoResponse<AspNetUsersAspNetUserClaims_KEY_RESTResponseDto>();
                var queryableResult =
                    from asp_net_user_claims in _readContext.AspNetUserClaims
                    join asp_net_users in _readContext.AspNetUsers on asp_net_user_claims.UserId equals asp_net_users.Id
                    where asp_net_users.Id.Equals(aspNetUsersId) && asp_net_user_claims.Id.Equals(aspNetUserClaimsId)select new AspNetUsersAspNetUserClaims_KEY_RESTResponseDto()
                    {
                        Id = asp_net_user_claims.Id,
                        UserId = asp_net_user_claims.UserId,
                        ClaimType = asp_net_user_claims.ClaimType,
                        ClaimValue = asp_net_user_claims.ClaimValue
                    };
                var result = await queryableResult.FirstOrDefaultAsync();
                if (result == null)
                {
                    returnValue.Code = 404;
                    returnValue.Message = $"Not found in AspNetUsers";
                }
                else
                {
                    returnValue.Value = result;
                }

                return returnValue;
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<AspNetUsersAspNetUserClaims_KEY_RESTResponseDto>(ex, "AspNetUsers", "AspNetUsersAspNetUserClaims_KEY_REST");
            }
        }

        public async Task<RepoResponse<List<AspNetUsersAspNetUserTokens_RESTResponseDto>>> AspNetUsersAspNetUserTokens_RESTAsync(string aspNetUsersId)
        {
            try
            {
                var queryableResult =
                    from asp_net_user_tokens in _readContext.AspNetUserTokens
                    join asp_net_users in _readContext.AspNetUsers on asp_net_user_tokens.UserId equals asp_net_users.Id
                    where asp_net_users.Id.Equals(aspNetUsersId)select new AspNetUsersAspNetUserTokens_RESTResponseDto()
                    {
                        UserId = asp_net_user_tokens.UserId,
                        LoginProvider = asp_net_user_tokens.LoginProvider,
                        Name = asp_net_user_tokens.Name,
                        Value = asp_net_user_tokens.Value
                    };
                var result = await queryableResult.ToListAsync();
                return new RepoResponse<List<AspNetUsersAspNetUserTokens_RESTResponseDto>>(result, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<List<AspNetUsersAspNetUserTokens_RESTResponseDto>>(ex, "AspNetUsers", "AspNetUsersAspNetUserTokens_REST");
            }
        }

        public async Task<RepoResponse<AspNetUsersAspNetUserTokens_KEY_RESTResponseDto>> AspNetUsersAspNetUserTokens_KEY_RESTAsync(string aspNetUsersId, string aspNetUserTokensUserId)
        {
            try
            {
                var returnValue = new RepoResponse<AspNetUsersAspNetUserTokens_KEY_RESTResponseDto>();
                var queryableResult =
                    from asp_net_user_tokens in _readContext.AspNetUserTokens
                    join asp_net_users in _readContext.AspNetUsers on asp_net_user_tokens.UserId equals asp_net_users.Id
                    where asp_net_users.Id.Equals(aspNetUsersId) && asp_net_user_tokens.UserId.Equals(aspNetUserTokensUserId)select new AspNetUsersAspNetUserTokens_KEY_RESTResponseDto()
                    {
                        UserId = asp_net_user_tokens.UserId,
                        LoginProvider = asp_net_user_tokens.LoginProvider,
                        Name = asp_net_user_tokens.Name,
                        Value = asp_net_user_tokens.Value
                    };
                var result = await queryableResult.FirstOrDefaultAsync();
                if (result == null)
                {
                    returnValue.Code = 404;
                    returnValue.Message = $"Not found in AspNetUsers";
                }
                else
                {
                    returnValue.Value = result;
                }

                return returnValue;
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<AspNetUsersAspNetUserTokens_KEY_RESTResponseDto>(ex, "AspNetUsers", "AspNetUsersAspNetUserTokens_KEY_REST");
            }
        }

        public async Task<RepoResponse<List<AspNetUsersAspNetUserLogins_RESTResponseDto>>> AspNetUsersAspNetUserLogins_RESTAsync(string aspNetUsersId)
        {
            try
            {
                var queryableResult =
                    from asp_net_user_logins in _readContext.AspNetUserLogins
                    join asp_net_users in _readContext.AspNetUsers on asp_net_user_logins.UserId equals asp_net_users.Id
                    where asp_net_users.Id.Equals(aspNetUsersId)select new AspNetUsersAspNetUserLogins_RESTResponseDto()
                    {
                        LoginProvider = asp_net_user_logins.LoginProvider,
                        ProviderKey = asp_net_user_logins.ProviderKey,
                        ProviderDisplayName = asp_net_user_logins.ProviderDisplayName,
                        UserId = asp_net_user_logins.UserId
                    };
                var result = await queryableResult.ToListAsync();
                return new RepoResponse<List<AspNetUsersAspNetUserLogins_RESTResponseDto>>(result, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<List<AspNetUsersAspNetUserLogins_RESTResponseDto>>(ex, "AspNetUsers", "AspNetUsersAspNetUserLogins_REST");
            }
        }

        public async Task<RepoResponse<AspNetUsersAspNetUserLogins_KEY_RESTResponseDto>> AspNetUsersAspNetUserLogins_KEY_RESTAsync(string aspNetUsersId, string aspNetUserLoginsLoginProvider)
        {
            try
            {
                var returnValue = new RepoResponse<AspNetUsersAspNetUserLogins_KEY_RESTResponseDto>();
                var queryableResult =
                    from asp_net_user_logins in _readContext.AspNetUserLogins
                    join asp_net_users in _readContext.AspNetUsers on asp_net_user_logins.UserId equals asp_net_users.Id
                    where asp_net_users.Id.Equals(aspNetUsersId) && asp_net_user_logins.LoginProvider.Equals(aspNetUserLoginsLoginProvider)select new AspNetUsersAspNetUserLogins_KEY_RESTResponseDto()
                    {
                        LoginProvider = asp_net_user_logins.LoginProvider,
                        ProviderKey = asp_net_user_logins.ProviderKey,
                        ProviderDisplayName = asp_net_user_logins.ProviderDisplayName,
                        UserId = asp_net_user_logins.UserId
                    };
                var result = await queryableResult.FirstOrDefaultAsync();
                if (result == null)
                {
                    returnValue.Code = 404;
                    returnValue.Message = $"Not found in AspNetUsers";
                }
                else
                {
                    returnValue.Value = result;
                }

                return returnValue;
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<AspNetUsersAspNetUserLogins_KEY_RESTResponseDto>(ex, "AspNetUsers", "AspNetUsersAspNetUserLogins_KEY_REST");
            }
        }

        public async Task<RepoResponse<List<AspNetUsersRefreshTokens_RESTResponseDto>>> AspNetUsersRefreshTokens_RESTAsync(string aspNetUsersId)
        {
            try
            {
                var queryableResult =
                    from refresh_tokens in _readContext.RefreshTokens
                    join asp_net_users in _readContext.AspNetUsers on refresh_tokens.UserId equals asp_net_users.Id
                    where asp_net_users.Id.Equals(aspNetUsersId)select new AspNetUsersRefreshTokens_RESTResponseDto()
                    {
                        Id = refresh_tokens.Id,
                        UserId = refresh_tokens.UserId,
                        Token = refresh_tokens.Token,
                        ExpiryDate = refresh_tokens.ExpiryDate,
                        CreatedDate = refresh_tokens.CreatedDate,
                        IsRevoked = refresh_tokens.IsRevoked
                    };
                var result = await queryableResult.ToListAsync();
                return new RepoResponse<List<AspNetUsersRefreshTokens_RESTResponseDto>>(result, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<List<AspNetUsersRefreshTokens_RESTResponseDto>>(ex, "AspNetUsers", "AspNetUsersRefreshTokens_REST");
            }
        }

        public async Task<RepoResponse<AspNetUsersRefreshTokens_KEY_RESTResponseDto>> AspNetUsersRefreshTokens_KEY_RESTAsync(string aspNetUsersId, int refreshTokensId)
        {
            try
            {
                var returnValue = new RepoResponse<AspNetUsersRefreshTokens_KEY_RESTResponseDto>();
                var queryableResult =
                    from refresh_tokens in _readContext.RefreshTokens
                    join asp_net_users in _readContext.AspNetUsers on refresh_tokens.UserId equals asp_net_users.Id
                    where asp_net_users.Id.Equals(aspNetUsersId) && refresh_tokens.Id.Equals(refreshTokensId)select new AspNetUsersRefreshTokens_KEY_RESTResponseDto()
                    {
                        Id = refresh_tokens.Id,
                        UserId = refresh_tokens.UserId,
                        Token = refresh_tokens.Token,
                        ExpiryDate = refresh_tokens.ExpiryDate,
                        CreatedDate = refresh_tokens.CreatedDate,
                        IsRevoked = refresh_tokens.IsRevoked
                    };
                var result = await queryableResult.FirstOrDefaultAsync();
                if (result == null)
                {
                    returnValue.Code = 404;
                    returnValue.Message = $"Not found in AspNetUsers";
                }
                else
                {
                    returnValue.Value = result;
                }

                return returnValue;
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<AspNetUsersRefreshTokens_KEY_RESTResponseDto>(ex, "AspNetUsers", "AspNetUsersRefreshTokens_KEY_REST");
            }
        }
    }
}