IF OBJECT_ID(N'dbo.CATEGORIES', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[CATEGORIES] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [PARENT_CATEGORY_ID] int NOT NULL,
        [NAME] nvarchar(250) NOT NULL,
        [SLUG] nvarchar(50) NOT NULL,
        [IS_ACTIVE] bit NOT NULL DEFAULT 1,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_CATEGORIES] PRIMARY KEY ([ID])
    );
END;