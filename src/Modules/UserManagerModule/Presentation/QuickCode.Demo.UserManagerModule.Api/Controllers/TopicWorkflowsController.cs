using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using QuickCode.Demo.Common.Workflows;
using QuickCode.Demo.UserManagerModule.Application.Features.TopicWorkflow;

namespace QuickCode.Demo.UserManagerModule.Api.Controllers
{
    public partial class TopicWorkflowsController 
    {
        private IHttpClientFactory HttpClientFactory => serviceProvider.GetRequiredService<IHttpClientFactory>();
        private IMemoryCache Cache => serviceProvider.GetRequiredService<IMemoryCache>();

        [HttpGet("{id:int}/uml")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<string> GetItemUmlAsync(int id)
        {
            try
            {
                var response = await mediator.Send(new GetItemTopicWorkflowQuery(id));
                var workflow = WorkflowDeserializer.ParseWorkflow(response.Value.WorkflowContent);
                var diagramUml = workflow.GenerateSequenceDiagram();

                return diagramUml;
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }
        }


        [HttpGet("{id:int}/diagram")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemDiagramAsync(int id)
        {
            try
            {
                var cacheKey = $"diagram_{id}";
                if (Cache.TryGetValue(cacheKey, out byte[] cachedImage))
                    return File(cachedImage, "image/svg+xml");

                var workflowsDto =  await mediator.Send(new GetItemTopicWorkflowQuery(id));
                var plantUmlBaseUrl = "https://www.plantuml.com/plantuml/svg/";
                var workflow = WorkflowDeserializer.ParseWorkflow(workflowsDto.Value.WorkflowContent);

                var client = HttpClientFactory.CreateClient();
                var encodedUml = workflow.GetEncodedPlantUml();
                var diagramUrl = $"{plantUmlBaseUrl}{encodedUml}";
                var response = await client.GetAsync(diagramUrl);

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode);

                var imageBytes = await response.Content.ReadAsByteArrayAsync();
                Cache.Set(cacheKey, imageBytes, TimeSpan.FromHours(1));

                return File(imageBytes, "image/svg+xml");
            }
            catch (Exception ex)
            {
                return Problem($"An error occurred: {ex.Message}");
            }
        }

    }
}