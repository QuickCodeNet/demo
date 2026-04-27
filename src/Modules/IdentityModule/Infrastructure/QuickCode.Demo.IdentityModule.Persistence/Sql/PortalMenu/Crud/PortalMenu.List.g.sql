SELECT
    [Key],
    [Name],
    [Text],
    [Tooltip],
    [ActionName],
    [OrderNo],
    [ParentName]
FROM [dbo].[PortalMenus]
ORDER BY
    [Key]
OFFSET @PRM_QC_LIST_OFFSET ROWS
FETCH NEXT @PRM_QC_LIST_PAGE_SIZE ROWS ONLY;