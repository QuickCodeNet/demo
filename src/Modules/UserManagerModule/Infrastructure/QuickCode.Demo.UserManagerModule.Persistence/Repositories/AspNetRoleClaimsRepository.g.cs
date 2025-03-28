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
    public partial class AspNetRoleClaimsRepository : IAspNetRoleClaimsRepository
    {
        private readonly WriteDbContext _writeContext;
        private readonly ReadDbContext _readContext;
        private readonly ILogger<AspNetRoleClaimsRepository> _logger;
        public AspNetRoleClaimsRepository(ILogger<AspNetRoleClaimsRepository> logger, WriteDbContext writeContext, ReadDbContext readContext)
        {
            _writeContext = writeContext;
            _readContext = readContext;
            _logger = logger;
        }

        public async Task<RepoResponse<AspNetRoleClaims>> InsertAsync(AspNetRoleClaims value)
        {
            try
            {
                await _writeContext.AspNetRoleClaims.AddAsync(value);
                await _writeContext.SaveChangesAsync();
                return new RepoResponse<AspNetRoleClaims>(value, "Not Defined");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<AspNetRoleClaims>(ex, "AspNetRoleClaims", "Insert");
            }
        }

        public async Task<RepoResponse<bool>> UpdateAsync(AspNetRoleClaims value)
        {
            try
            {
                _writeContext.Set<AspNetRoleClaims>().Update(value);
                await _writeContext.SaveChangesAsync();
                return new RepoResponse<bool>(true, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<bool>(ex, "AspNetRoleClaims", "Update");
            }
        }

        public async Task<RepoResponse<bool>> DeleteAsync(AspNetRoleClaims value)
        {
            try
            {
                _writeContext.AspNetRoleClaims.Remove(value);
                await _writeContext.SaveChangesAsync();
                return new RepoResponse<bool>(true, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<bool>(ex, "AspNetRoleClaims", "Delete");
            }
        }

        public async Task<RepoResponse<AspNetRoleClaims>> GetByPkAsync(int id)
        {
            try
            {
                var result =
                    from asp_net_role_claims in _readContext.AspNetRoleClaims
                    where asp_net_role_claims.Id.Equals(id)select asp_net_role_claims;
                var response = await result.FirstOrDefaultAsync();
                return response == null ? new RepoResponse<AspNetRoleClaims>
                {
                    Code = 404,
                    Message = "Not found in AspNetRoleClaims"
                }

                : new RepoResponse<AspNetRoleClaims>(response, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<AspNetRoleClaims>(ex, "AspNetRoleClaims", "GetByPk");
            }
        }

        public async Task<RepoResponse<List<AspNetRoleClaims>>> ListAsync(int? pageNumber = null, int? pageSize = null)
        {
            try
            {
                if (pageNumber.HasValue && pageNumber < 1)
                {
                    return new RepoResponse<List<AspNetRoleClaims>>
                    {
                        Code = 404,
                        Message = "Page Number must be greater than 1"
                    };
                }

                var query = _readContext.AspNetRoleClaims.AsQueryable();
                if (pageNumber.HasValue && pageSize.HasValue)
                {
                    query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
                }

                return new RepoResponse<List<AspNetRoleClaims>>(await query.ToListAsync(), "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<List<AspNetRoleClaims>>(ex, "AspNetRoleClaims", "List");
            }
        }

        public async Task<RepoResponse<int>> CountAsync()
        {
            try
            {
                return new RepoResponse<int>(await _readContext.AspNetRoleClaims.CountAsync(), "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<int>(ex, "AspNetRoleClaims", "Count");
            }
        }
    }
}