SELECT
    [Key],
    [Name],
    [Text],
    [Tooltip],
    [ActionName],
    [OrderNo],
    [ParentName]
FROM [dbo].[PortalMenus]
WHERE
    [Key] = @PRM_PORTAL_MENU_KEY;