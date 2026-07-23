IF OBJECT_ID(N'dbo.ATTRIBUTE_VALUES', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[ATTRIBUTE_VALUES] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [ATTRIBUTE_ID] int NOT NULL,
        [VALUE] nvarchar(250) NOT NULL,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_ATTRIBUTE_VALUES] PRIMARY KEY ([ID])
    );
END;