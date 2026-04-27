SELECT
    [TableName],
    [IdColumn],
    [TextColumns],
    [StringFormat]
FROM [dbo].[TableComboboxSettings]
ORDER BY
    [TableName]
OFFSET @PRM_QC_LIST_OFFSET ROWS
FETCH NEXT @PRM_QC_LIST_PAGE_SIZE ROWS ONLY;