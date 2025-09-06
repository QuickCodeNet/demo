using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;
using QuickCode.Demo.Common.Nswag.Clients.UserManagerModuleApi.Contracts;

namespace QuickCode.Demo.Portal.Models
{
	public class GetPortalPermissionGroupData
	{
		public PortalPermissionGroupList Items { get; set; }

		public string SelectedGroupName { get; set; }

		public Dictionary<string, Dictionary<string,string>> ComboList = new Dictionary<string, Dictionary<string,string>>();
	}

	
	public class UpdateGroupAuthorizationRequestData : UpdatePortalPermissionGroupRequest
	{
		public string Key
		{
			get
			{
				var keyList = new object[] { PermissionGroupName, PortalPermissionName, PortalPermissionTypeId };
				return string.Join("|", keyList.Select(i => i.AsString()));
			}
		}
	}
	
	public class UpdateKafkaEvent
	{
		public string TopicName { get; set; }
		public string EventName { get; set; }
		public int Value { get; set; }
	}

	public class GetKafkaEventsData
	{
		public Dictionary<string, Dictionary<string, List<KafkaEventsGetKafkaEventsResponseDto>>> Items { get; set; }
	}

	
	public class GetApiPermissionGroupData
	{
		public ApiModulePermissions Items { get; set; }

		public string SelectedGroupName { get; set; }

		public Dictionary<string, Dictionary<string,string>> ComboList = new Dictionary<string, Dictionary<string,string>>();
	}

	
	public class UpdateGroupAuthorizationApiRequestData : UpdateApiPermissionGroupRequest
	{
		public string Key
		{
			get
			{
				var keyList = new object[] { PermissionGroupName, ApiMethodDefinitionKey };
				return string.Join("|", keyList.Select(i => i.AsString()));
			}
		}
	}
}

