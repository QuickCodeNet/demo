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

namespace QuickCode.Demo.UserManagerModule.Application.Dtos
{
    public record PermissionGroupsAspNetUsers_KEY_RESTResponseDto
    {
        public string Id { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public int PermissionGroupId { get; init; }
        public string? UserName { get; init; }
        public string? NormalizedUserName { get; init; }
        public string? Email { get; init; }
        public string? NormalizedEmail { get; init; }
        public bool EmailConfirmed { get; init; }
        public string? PasswordHash { get; init; }
        public string? SecurityStamp { get; init; }
        public string? ConcurrencyStamp { get; init; }
        public string? PhoneNumber { get; init; }
        public bool PhoneNumberConfirmed { get; init; }
        public bool TwoFactorEnabled { get; init; }
        public DateTimeOffset? LockoutEnd { get; init; }
        public bool LockoutEnabled { get; init; }
        public int AccessFailedCount { get; init; }
    }
}