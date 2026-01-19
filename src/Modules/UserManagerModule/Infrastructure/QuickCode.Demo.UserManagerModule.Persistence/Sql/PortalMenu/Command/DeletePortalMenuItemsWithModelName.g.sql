DELETE FROM [PortalMenus] 
WHERE [Key] LIKE @PRM_PortalMenus_Key + '%' 
	AND [Name] = @PRM_PortalMenus_Name