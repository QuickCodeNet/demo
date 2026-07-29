IF OBJECT_ID(N'dbo.PRODUCT_VARIANTS', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[PRODUCT_VARIANTS] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [PRODUCT_ID] int NOT NULL,
        [SKU] nvarchar(50) NOT NULL,
        [PRICE] decimal(18,2) NOT NULL,
        [STOCK_QUANTITY] int NOT NULL DEFAULT 0,
        [IS_ACTIVE] bit NOT NULL DEFAULT 1,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_PRODUCT_VARIANTS] PRIMARY KEY ([ID])
    );
END;