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
using QuickCode.Demo.UserManagerModule.Application.Dtos;
using System.Threading.Tasks;

namespace QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories
{
    public partial interface IAspNetUserTokensRepository : IBaseRepository<AspNetUserTokens>
    {
        Task<RepoResponse<AspNetUserTokens>> GetByPkAsync(string userId);
    }
}