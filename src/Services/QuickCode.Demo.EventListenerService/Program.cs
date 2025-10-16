using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Nswag.Extensions;
using QuickCode.Demo.Common.Workflows;
using QuickCode.Demo.EventListenerService;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
const string inputJsonContent = """
{
   "RequestInfo": {
      "Path": "/api/quick-code-module/db-types/mssql",
      "Method": "PUT",
      "Headers": {
         "Accept": "application/json",
         "Host": "quickcode-quickcode-gateway-api",
         "Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJmOWJmZmEwOS1kYWU3LTRmZTItYTE0NS04OTExMGJiODQ2MTEiLCJlbWFpbCI6InV6ZXlpcmFwYXlkaW5AZ21haWwuY29tIiwiUGVybWlzc2lvbkdyb3VwSWQiOiIxIiwianRpIjoiYzg2MzdjMzQtZDZhNC00MDMyLWEwYjAtOGU5MWFkYzhlNjA5IiwiZXhwIjoxNzMwMzY1MzEyLCJpc3MiOiJodHRwczovL3F1aWNrY29kZS1hcGkucXVpY2tjb2RlLm5ldCIsImF1ZCI6InF1aWNrY29kZS1xdWlja2NvZGUtY2xpZW50LWlkIn0.TY4kaTHxRVZnnmiTi504kx5zWQ3TPE9hhPyvEqUr-6w",
         "Content-Type": "application/json",
         "traceparent": "00-f7e1f1ba4dadc720456b015f6fe202d0-30b1e7060b038df3-00",
         "Content-Length": "126",
         "X-Api-Key": "721de9b0-0def-41af-aa4e-66e454c92190"
      },
      "Body": {
         "key": "mssql",
         "name": "Microsoft SQL Server",
         "description": "Microsoft SQL Server",
         "iconUrl": "~/img/skins/sql_server_logo.png"
      }
   },
   "ResponseInfo": {
      "StatusCode": 200,
      "Headers": {
         "Content-Type": "application/json; charset=utf-8",
         "Date": "Thu, 31 Oct 2024 08:58:41 GMT",
         "Server": "Kestrel"
      },
      "Body": {
         "value": true
      }
   },
   "OrderId": 5,
   "ExceptionMessage": null,
   "ElapsedMilliseconds": 7846,
   "Timestamp": "2024-10-31T08:58:44.8656194Z"
}
""";
const string yamlContent = """
name: 'Order Processing Workflow'
version: '1.0.0'
description: 'A workflow for processing customer orders'

variables:
  retryCount:
    type: 'int'
    value: '3'
  retryType:
    type: 'string'
    value: 'hello'

steps:
  validateOrder:
    url: '{{QuickcodeClients.QuickCodeModuleApi}}/api/quick-code-module/db-types'
    method: 'POST'
    headers:
      X-Api-Key: '{{QuickcodeApiKeys.QuickCodeModuleApiKey}}'
    body:
      key: 'newOrder'
      name: 'Shoe Order'
      description: 'Black Nike Shoes'
      iconUrl: 'black-shoe.png'
    timeoutSeconds: 30
    transitions:
      - condition: 'validateOrder.statusCode == 200'
        action: 'processPayment'
      - condition: 'response.isValid == false'
        action: 'processRollbackPayment'
      - condition: 'default'
        action: 'handleInvalidOrder'

  processPayment:
    url: '{{QuickcodeClients.QuickCodeModuleApi}}/api/quick-code-module/payment'
    method: 'POST'
    headers:
      X-Api-Key: '{{QuickcodeApiKeys.QuickCodeModuleApiKey}}'
    body:
      paymentId: '123233'
      paymentAmount: '12.33'
    timeoutSeconds: 30
    transitions:
      - condition: 'processPayment.statusCode == 200'
        action: 'completeOrder'

  processRollbackPayment:
    url: '{{QuickcodeClients.QuickCodeModuleApi}}/api/quick-code-module/payment-rollback'
    method: 'POST'
    headers:
      X-Api-Key: '{{QuickcodeApiKeys.QuickCodeModuleApiKey}}'
    body:
      paymentId: '123233'
    timeoutSeconds: 30
    transitions:
      - condition: 'processRollbackPayment.statusCode == 200'
        action: 'completeOrder'
      - condition: 'processRollbackPayment.statusCode == 404'
        action: 'processPayment'

  completeOrder:
    url: '{{QuickcodeClients.QuickCodeModuleApi}}/api/quick-code-module/payment-complete'
    method: 'POST'
    headers:
      X-Api-Key: '{{QuickcodeApiKeys.QuickCodeModuleApiKey}}'
    body:
      paymentId: '123233'
      orderId: '1123323'
    timeoutSeconds: 30
    transitions:
      - condition: 'completeOrder.statusCode == 200'
        action: 'success'

  handleInvalidOrder:
    url: '{{QuickcodeClients.QuickCodeModuleApi}}/api/quick-code-module/payment'
    method: 'POST'
    headers:
      X-Api-Key: '{{QuickcodeApiKeys.QuickCodeModuleApiKey}}'
    body:
      paymentId: '123233'
      orderId: '1123323'
    timeoutSeconds: 30
    transitions:
      - condition: 'handleInvalidOrder.statusCode == 200'
        action: 'success'
""";

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
Log.Information($"Started({environmentName})...");
builder.Configuration.UpdateConfigurationFromEnv();

var useHealthCheck = builder.Configuration.GetSection("AppSettings:UseHealthCheck").Get<bool>();
var kafkaBootstrapServers = builder.Configuration.GetValue<string>("Kafka:BootstrapServers");

builder.Services.AddLogger(builder.Configuration);
Log.Information($"{builder.Configuration["Logging:ApiName"]} Started.");

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddNswagServiceClient(builder.Configuration, typeof(QuickCodeBaseApiController));
builder.Services.AddHostedService<DynamicKafkaBackgroundService>();
builder.Services.AddControllers();
builder.Services.AddHealthChecks().AddCheck("kafka", new KafkaHealthCheck(kafkaBootstrapServers!));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (useHealthCheck)
{
    app.UseHealthChecks("/hc", new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

var apiGroup = app.MapGroup("/api");

apiGroup.MapGet("/", () => "Kafka Background Event Listener Service is running...").WithOpenApi();
apiGroup.MapPost("/set-topic-refresh-interval", ([FromBody] int seconds) =>
{
    if (seconds > 30)
    {
        DynamicKafkaBackgroundService.SetTopicRefreshInterval(seconds);
    }
    return $"Topic refresh interval set to {DynamicKafkaBackgroundService.GetTopicRefreshInterval()} seconds";
}).WithOpenApi();

apiGroup.MapPost("/set-topic-listener-interval", ([FromBody] int seconds) =>
{
    if (seconds > 30)
    {
        DynamicKafkaBackgroundService.SetTopicListenerInterval(seconds);
    }
    return $"Topic listen interval set to {DynamicKafkaBackgroundService.GetTopicListenerInterval()} seconds";
}).WithOpenApi();

apiGroup.MapGet("/get-topic-refresh-interval", () => DynamicKafkaBackgroundService.GetTopicRefreshInterval()).WithOpenApi();
apiGroup.MapGet("/get-topic-listener-interval", () => DynamicKafkaBackgroundService.GetTopicListenerInterval()).WithOpenApi();

apiGroup.MapPost("/execute-workflow", async (HttpContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<Program> logger) =>
{
    try
    {
        var executor = new WorkflowExecutor(yamlContent, inputJsonContent, httpClientFactory.CreateClient(), logger, configuration);
        var results = await executor.ExecuteWorkflow();
        return Results.Ok(results);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Workflow execution failed");
        return Results.Problem("Workflow execution failed", statusCode: 500);
    }
}).WithOpenApi();

apiGroup.MapGet("/diagram-test", async (IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IMemoryCache cache) =>
{
    try
    {
        var cacheKey = "diagram_test";

        if (cache.TryGetValue(cacheKey, out byte[] cachedImage))
            return Results.Bytes(cachedImage, "image/png");

        var plantUmlBaseUrl = "https://www.plantuml.com/plantuml/png/";
        var workflow = WorkflowDeserializer.ParseWorkflow(yamlContent);

        var client = httpClientFactory.CreateClient();
        var encodedUml = workflow.GetEncodedPlantUml();
        var diagramUrl = $"{plantUmlBaseUrl}{encodedUml}";
        var response = await client.GetAsync(diagramUrl);

        if (!response.IsSuccessStatusCode)
            return Results.StatusCode((int)response.StatusCode);

        var imageBytes = await response.Content.ReadAsByteArrayAsync();
        cache.Set(cacheKey, imageBytes, TimeSpan.FromHours(1));

        return Results.File(imageBytes, "image/png");
    }
    catch (Exception ex)
    {
        return Results.Problem($"An error occurred: {ex.Message}");
    }
}).WithOpenApi();

await app.RunAsync();