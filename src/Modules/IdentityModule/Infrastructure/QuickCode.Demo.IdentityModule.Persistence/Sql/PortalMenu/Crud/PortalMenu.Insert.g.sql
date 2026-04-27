INSERT INTO [dbo].[PortalMenus] (
    [Key],
    [Name],
    [Text],
    [Tooltip],
    [ActionName],
    [OrderNo],
    [ParentName]
)
OUTPUT INSERTED.*
VALUES (
    @PRM_PORTAL_MENU_KEY,
    @PRM_PORTAL_MENU_NAME,
    @PRM_PORTAL_MENU_TEXT,
    @PRM_PORTAL_MENU_TOOLTIP,
    @PRM_PORTAL_MENU_ACTION_NAME,
    @PRM_PORTAL_MENU_ORDER_NO,
    @PRM_PORTAL_MENU_PARENT_NAME
    );