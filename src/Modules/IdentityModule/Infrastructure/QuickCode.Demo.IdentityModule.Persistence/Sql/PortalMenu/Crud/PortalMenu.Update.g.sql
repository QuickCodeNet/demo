UPDATE [dbo].[PortalMenus]
SET
    [Name] = @PRM_PORTAL_MENU_NAME,
    [Text] = @PRM_PORTAL_MENU_TEXT,
    [Tooltip] = @PRM_PORTAL_MENU_TOOLTIP,
    [ActionName] = @PRM_PORTAL_MENU_ACTION_NAME,
    [OrderNo] = @PRM_PORTAL_MENU_ORDER_NO,
    [ParentName] = @PRM_PORTAL_MENU_PARENT_NAME
WHERE
    [Key] = @PRM_PORTAL_MENU_KEY;