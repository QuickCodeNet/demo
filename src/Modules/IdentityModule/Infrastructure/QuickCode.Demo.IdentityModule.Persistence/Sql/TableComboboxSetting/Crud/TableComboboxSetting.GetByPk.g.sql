SELECT
    [TableName],
    [IdColumn],
    [TextColumns],
    [StringFormat]
FROM [dbo].[TableComboboxSettings]
WHERE
    [TableName] = @PRM_TABLE_COMBOBOX_SETTING_TABLE_NAME;