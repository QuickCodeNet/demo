IF OBJECT_ID(N'dbo.STORES', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[STORES] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [SELLER_ID] int NOT NULL,
        [NAME] nvarchar(250) NOT NULL,
        [SLUG] nvarchar(50) NOT NULL,
        [DESCRIPTION] nvarchar(1000) NOT NULL,
        [LOGO_URL] nvarchar(500) NOT NULL,
        [IS_ACTIVE] bit NOT NULL DEFAULT 1,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_STORES] PRIMARY KEY ([ID])
    );
END;