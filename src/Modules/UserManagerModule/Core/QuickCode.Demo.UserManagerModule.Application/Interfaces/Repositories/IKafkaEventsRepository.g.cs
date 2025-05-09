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
    public partial interface IKafkaEventsRepository : IBaseRepository<KafkaEventsDto>
    {
        Task<RepoResponse<KafkaEventsDto>> GetByPkAsync(string topicName);
        Task<RepoResponse<List<KafkaEventsGetKafkaEventsResponseDto>>> KafkaEventsGetKafkaEventsAsync();
        Task<RepoResponse<List<KafkaEventsGetActiveKafkaEventsResponseDto>>> KafkaEventsGetActiveKafkaEventsAsync();
        Task<RepoResponse<List<KafkaEventsGetTopicWorkflowsResponseDto>>> KafkaEventsGetTopicWorkflowsAsync(string kafkaEventsTopicName, string apiMethodDefinitionsHttpMethod);
        Task<RepoResponse<List<KafkaEventsTopicWorkflows_RESTResponseDto>>> KafkaEventsTopicWorkflows_RESTAsync(string kafkaEventsTopicName);
        Task<RepoResponse<KafkaEventsTopicWorkflows_KEY_RESTResponseDto>> KafkaEventsTopicWorkflows_KEY_RESTAsync(string kafkaEventsTopicName, int topicWorkflowsId);
    }
}