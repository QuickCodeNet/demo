SELECT
    [Key],
    [ModuleName],
    [ModelName],
    [HttpMethod],
    [ControllerName],
    [MethodName],
    [UrlPath]
FROM [dbo].[ApiMethodDefinitions]
ORDER BY
    [Key]
OFFSET @PRM_QC_LIST_OFFSET ROWS
FETCH NEXT @PRM_QC_LIST_PAGE_SIZE ROWS ONLY;