IF OBJECT_ID(N'dbo.SELLERS', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[SELLERS] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [USER_ID] int NOT NULL,
        [COMPANY_NAME] nvarchar(250) NOT NULL,
        [CONTACT_EMAIL] nvarchar(500) NOT NULL,
        [CONTACT_PHONE] nvarchar(50) NOT NULL,
        [STATUS] nvarchar(max) NOT NULL DEFAULT 'PENDING_VERIFICATION',
        [SELLER_TIER_ID] int NOT NULL,
        [JOINED_DATE] datetime2(7) NOT NULL,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_SELLERS] PRIMARY KEY ([ID])
    );
END;