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
    public partial class ColumnTypesRepository : IColumnTypesRepository
    {
        private readonly WriteDbContext _writeContext;
        private readonly ReadDbContext _readContext;
        private readonly ILogger<ColumnTypesRepository> _logger;
        public ColumnTypesRepository(ILogger<ColumnTypesRepository> logger, WriteDbContext writeContext, ReadDbContext readContext)
        {
            _writeContext = writeContext;
            _readContext = readContext;
            _logger = logger;
        }

        public async Task<RepoResponse<ColumnTypes>> InsertAsync(ColumnTypes value)
        {
            try
            {
                await _writeContext.ColumnTypes.AddAsync(value);
                await _writeContext.SaveChangesAsync();
                return new RepoResponse<ColumnTypes>(value, "Not Defined");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<ColumnTypes>(ex, "ColumnTypes", "Insert");
            }
        }

        public async Task<RepoResponse<bool>> UpdateAsync(ColumnTypes value)
        {
            try
            {
                _writeContext.Set<ColumnTypes>().Update(value);
                await _writeContext.SaveChangesAsync();
                return new RepoResponse<bool>(true, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<bool>(ex, "ColumnTypes", "Update");
            }
        }

        public async Task<RepoResponse<bool>> DeleteAsync(ColumnTypes value)
        {
            try
            {
                _writeContext.ColumnTypes.Remove(value);
                await _writeContext.SaveChangesAsync();
                return new RepoResponse<bool>(true, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<bool>(ex, "ColumnTypes", "Delete");
            }
        }

        public async Task<RepoResponse<ColumnTypes>> GetByPkAsync(int id)
        {
            try
            {
                var result =
                    from column_types in _readContext.ColumnTypes
                    where column_types.Id.Equals(id)select column_types;
                var response = await result.FirstOrDefaultAsync();
                return response == null ? new RepoResponse<ColumnTypes>
                {
                    Code = 404,
                    Message = "Not found in ColumnTypes"
                }

                : new RepoResponse<ColumnTypes>(response, "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<ColumnTypes>(ex, "ColumnTypes", "GetByPk");
            }
        }

        public async Task<RepoResponse<List<ColumnTypes>>> ListAsync(int? pageNumber = null, int? pageSize = null)
        {
            try
            {
                if (pageNumber.HasValue && pageNumber < 1)
                {
                    return new RepoResponse<List<ColumnTypes>>
                    {
                        Code = 404,
                        Message = "Page Number must be greater than 1"
                    };
                }

                var query = _readContext.ColumnTypes.AsQueryable();
                if (pageNumber.HasValue && pageSize.HasValue)
                {
                    query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
                }

                return new RepoResponse<List<ColumnTypes>>(await query.ToListAsync(), "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<List<ColumnTypes>>(ex, "ColumnTypes", "List");
            }
        }

        public async Task<RepoResponse<int>> CountAsync()
        {
            try
            {
                return new RepoResponse<int>(await _readContext.ColumnTypes.CountAsync(), "Success");
            }
            catch (Exception ex)
            {
                return _logger.LogExceptionAndCreateResponse<int>(ex, "ColumnTypes", "Count");
            }
        }
    }
}