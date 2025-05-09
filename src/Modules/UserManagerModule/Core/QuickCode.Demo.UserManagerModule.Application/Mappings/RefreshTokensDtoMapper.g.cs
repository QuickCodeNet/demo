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
using System.ComponentModel;
using QuickCode.Demo.UserManagerModule.Application.Dtos;
using QuickCode.Demo.UserManagerModule.Domain.Entities;

namespace QuickCode.Demo.UserManagerModule.Application.Mappings
{
    public static class RefreshTokensMappings
    {
        public static List<RefreshTokensDto> ToDto(this IEnumerable<RefreshTokens> model)
        {
            return model.Select(u => u.ToDto()).ToList();
        }

        public static RefreshTokensDto ToDto(this RefreshTokens model)
        {
            return new RefreshTokensDto
            {
                Id = model.Id,
                UserId = model.UserId,
                Token = model.Token,
                ExpiryDate = model.ExpiryDate,
                CreatedDate = model.CreatedDate,
                IsRevoked = model.IsRevoked
            };
        }

        public static RefreshTokens ToModel(this RefreshTokensDto model)
        {
            return new RefreshTokens
            {
                Id = model.Id,
                UserId = model.UserId,
                Token = model.Token,
                ExpiryDate = model.ExpiryDate,
                CreatedDate = model.CreatedDate,
                IsRevoked = model.IsRevoked
            };
        }
    }
}