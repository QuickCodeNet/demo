SELECT CASE WHEN EXISTS (
SELECT 1 
FROM [ApiMethodDefinitions] 
WHERE [ModelName] = @PRM_ApiMethodDefinitions_ModelName ) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END