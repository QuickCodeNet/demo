IF OBJECT_ID(N'dbo.ORDER_ITEMS', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[ORDER_ITEMS] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [ORDER_ID] int NOT NULL,
        [PRODUCT_VARIANT_ID] int NOT NULL,
        [PRODUCT_NAME] nvarchar(250) NOT NULL,
        [SKU] nvarchar(50) NOT NULL,
        [QUANTITY] int NOT NULL,
        [UNIT_PRICE] decimal(18,2) NOT NULL,
        [TOTAL_PRICE] decimal(18,2) NOT NULL,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_ORDER_ITEMS] PRIMARY KEY ([ID])
    );
END;