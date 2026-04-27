SELECT ANU.[Id], ANU.[FirstName], ANU.[LastName], ANU.[PermissionGroupName], ANU.[UserName], ANU.[NormalizedUserName], ANU.[Email], ANU.[NormalizedEmail], ANU.[EmailConfirmed], ANU.[PasswordHash], ANU.[SecurityStamp], ANU.[ConcurrencyStamp], ANU.[PhoneNumber], ANU.[PhoneNumberConfirmed], ANU.[TwoFactorEnabled], ANU.[LockoutEnd], ANU.[LockoutEnabled], ANU.[AccessFailedCount] 
FROM [AspNetUsers] ANU 
WHERE ANU.[Id] = @PRM_AspNetUser_Id 
ORDER BY ANU.[Id] 