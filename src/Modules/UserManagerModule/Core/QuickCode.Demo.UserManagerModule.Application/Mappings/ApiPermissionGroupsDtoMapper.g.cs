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
    public static class ApiPermissionGroupsMappings
    {
        public static List<ApiPermissionGroupsDto> ToDto(this IEnumerable<ApiPermissionGroups> model)
        {
            return model.Select(u => u.ToDto()).ToList();
        }

        public static ApiPermissionGroupsDto ToDto(this ApiPermissionGroups model)
        {
            return new ApiPermissionGroupsDto
            {
                Id = model.Id,
                PermissionGroupId = model.PermissionGroupId,
                ApiMethodDefinitionId = model.ApiMethodDefinitionId
            };
        }

        public static ApiPermissionGroups ToModel(this ApiPermissionGroupsDto model)
        {
            return new ApiPermissionGroups
            {
                Id = model.Id,
                PermissionGroupId = model.PermissionGroupId,
                ApiMethodDefinitionId = model.ApiMethodDefinitionId
            };
        }
    }
}