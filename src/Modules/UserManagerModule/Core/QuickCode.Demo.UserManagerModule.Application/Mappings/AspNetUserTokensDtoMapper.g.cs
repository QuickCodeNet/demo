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
    public static class AspNetUserTokensMappings
    {
        public static List<AspNetUserTokensDto> ToDto(this IEnumerable<AspNetUserTokens> model)
        {
            return model.Select(u => u.ToDto()).ToList();
        }

        public static AspNetUserTokensDto ToDto(this AspNetUserTokens model)
        {
            return new AspNetUserTokensDto
            {
                UserId = model.UserId,
                LoginProvider = model.LoginProvider,
                Name = model.Name,
                Value = model.Value
            };
        }

        public static AspNetUserTokens ToModel(this AspNetUserTokensDto model)
        {
            return new AspNetUserTokens
            {
                UserId = model.UserId,
                LoginProvider = model.LoginProvider,
                Name = model.Name,
                Value = model.Value
            };
        }
    }
}