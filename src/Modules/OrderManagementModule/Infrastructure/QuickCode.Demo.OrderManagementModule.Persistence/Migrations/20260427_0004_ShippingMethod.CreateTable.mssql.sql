IF OBJECT_ID(N'dbo.SHIPPING_METHODS', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[SHIPPING_METHODS] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [NAME] nvarchar(250) NOT NULL,
        [COST] decimal(18,2) NOT NULL DEFAULT 0,
        [IS_ACTIVE] bit NOT NULL DEFAULT 1,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_SHIPPING_METHODS] PRIMARY KEY ([ID])
    );
END;