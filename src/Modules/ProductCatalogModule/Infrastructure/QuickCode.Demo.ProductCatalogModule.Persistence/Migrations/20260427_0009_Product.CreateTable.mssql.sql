IF OBJECT_ID(N'dbo.PRODUCTS', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[PRODUCTS] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [SELLER_ID] int NOT NULL,
        [BRAND_ID] int NOT NULL,
        [PRIMARY_CATEGORY_ID] int NOT NULL,
        [SKU] nvarchar(50) NOT NULL,
        [NAME] nvarchar(250) NOT NULL,
        [DESCRIPTION] nvarchar(1000) NOT NULL,
        [STATUS] nvarchar(max) NOT NULL DEFAULT 'DRAFT',
        [IS_FEATURED] bit NOT NULL DEFAULT 0,
        [CREATED_DATE] datetime2(7) NOT NULL,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_PRODUCTS] PRIMARY KEY ([ID])
    );
END;