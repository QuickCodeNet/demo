UPDATE [USERS] 
	SET [IS_ACTIVE] = false 
WHERE [IsDeleted] = 0 
	AND [ID] = @PRM_USERS_ID