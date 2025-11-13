SELECT A.[Key], A.[HttpMethod], A.[ControllerName], A.[UrlPath] 
FROM [KafkaEvents] K 
	INNER JOIN [ApiMethodDefinitions] A 
			ON K.[ApiMethodDefinitionKey] = A.[Key] 
WHERE A.[Key] = @PRM_ApiMethodDefinitions_Key 
ORDER BY A.[Key] 