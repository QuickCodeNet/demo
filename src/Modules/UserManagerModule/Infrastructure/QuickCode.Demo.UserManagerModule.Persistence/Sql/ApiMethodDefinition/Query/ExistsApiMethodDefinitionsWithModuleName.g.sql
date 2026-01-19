SELECT CASE WHEN EXISTS (
SELECT 1 
FROM [ApiMethodDefinitions] 
WHERE [ModuleName] = @PRM_ApiMethodDefinitions_ModuleName ) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END