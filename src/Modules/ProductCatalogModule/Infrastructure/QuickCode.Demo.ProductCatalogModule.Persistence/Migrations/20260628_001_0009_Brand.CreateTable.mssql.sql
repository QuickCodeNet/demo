IF OBJECT_ID(N'dbo.BRANDS', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[BRANDS] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [NAME] nvarchar(250) NOT NULL,
        [LOGO_URL] nvarchar(500) NOT NULL,
        [IS_ACTIVE] bit NOT NULL DEFAULT 1,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_BRANDS] PRIMARY KEY ([ID])
    );
END;