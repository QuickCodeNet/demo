IF OBJECT_ID(N'dbo.ATTRIBUTE_DEFINITIONS', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[ATTRIBUTE_DEFINITIONS] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [NAME] nvarchar(50) NOT NULL,
        [CODE] nvarchar(50) NOT NULL,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_ATTRIBUTE_DEFINITIONS] PRIMARY KEY ([ID])
    );
END;