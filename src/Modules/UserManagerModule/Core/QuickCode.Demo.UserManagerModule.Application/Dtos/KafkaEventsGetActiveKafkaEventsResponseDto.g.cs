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
    public record KafkaEventsGetActiveKafkaEventsResponseDto
    {
        public string TopicName { get; init; }
        public int ApiMethodDefinitionId { get; init; }
        public bool IsActive { get; init; }
        public string HttpMethod { get; init; }
        public string ControllerName { get; init; }
        public string UrlPath { get; init; }
    }
}