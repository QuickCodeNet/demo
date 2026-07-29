IF OBJECT_ID(N'dbo.COMMISSION_MODELS', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[COMMISSION_MODELS] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [NAME] nvarchar(250) NOT NULL,
        [IS_ACTIVE] bit NOT NULL DEFAULT 1,
        [DESCRIPTION] nvarchar(1000) NOT NULL,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_COMMISSION_MODELS] PRIMARY KEY ([ID])
    );
END;