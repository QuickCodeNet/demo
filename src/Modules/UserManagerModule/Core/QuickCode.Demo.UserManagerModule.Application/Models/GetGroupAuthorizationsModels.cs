using System;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.UserManagerModule.Application.Dtos;

namespace QuickCode.Demo.UserManagerModule.Application.Models
{	
    public class PortalPermissionGroupList
    {
        public string PermissionGroupName { get; set; }
        public List<PortalPermissionItem> PortalPermissions { get; set; } = [];
    }

    public class PortalPermissionItem
    {
        public string PortalPermissionName { get; set; }
        public List<PortalPermissionTypeItem> PortalPermissionTypes { get; set; } = [];
    }

    public class PortalPermissionTypeItem
    {
        public int PortalPermissionTypeId { get; set; }
        public bool Value { get; set; }
    }

    public class UpdatePortalPermissionGroupRequest
    {
        public string PermissionGroupName{ get; set; }
        public string PortalPermissionName { get; set; }
        public int PortalPermissionTypeId { get; set; }
        public int Value { get; set; }
    }
	
    public class UpdateApiPermissionGroupRequest
    {
        public string PermissionGroupName { get; set; }
        public string ApiMethodDefinitionKey { get; set; }
        public int Value { get; set; }
    }
    
    public record ApiMethodDefinitionItem : ApiMethodDefinitionsDto
    {
        public bool Value { get; set; }
    }
	
    public class ApiModulePermissions
    {
        public string PermissionGroupName { get; set; }

        public Dictionary<string, Dictionary<string, List<ApiMethodDefinitionItem>>> ApiModulePermissionList { get; set; } = [];
    }
}