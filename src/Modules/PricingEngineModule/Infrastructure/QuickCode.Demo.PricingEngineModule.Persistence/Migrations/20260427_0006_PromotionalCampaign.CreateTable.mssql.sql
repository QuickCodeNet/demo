IF OBJECT_ID(N'dbo.PROMOTIONAL_CAMPAIGNS', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[PROMOTIONAL_CAMPAIGNS] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [NAME] nvarchar(250) NOT NULL,
        [START_DATE] datetime2(7) NOT NULL,
        [END_DATE] datetime2(7) NOT NULL,
        [DISCOUNT_PERCENTAGE] decimal(18,8) NOT NULL,
        [IS_ACTIVE] bit NOT NULL DEFAULT 1,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_PROMOTIONAL_CAMPAIGNS] PRIMARY KEY ([ID])
    );
END;