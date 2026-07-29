IF OBJECT_ID(N'dbo.COMMISSION_ENTRIES', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[COMMISSION_ENTRIES] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [ORDER_ID] int NOT NULL,
        [ORDER_ITEM_ID] int NOT NULL,
        [SELLER_ID] int NOT NULL,
        [COMMISSION_MODEL_ID] int NOT NULL,
        [COMMISSION_RULE_ID] int NOT NULL,
        [ITEM_PRICE] decimal(18,2) NOT NULL,
        [COMMISSION_AMOUNT] decimal(18,2) NOT NULL,
        [CALCULATED_DATE] datetime2(7) NOT NULL,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_COMMISSION_ENTRIES] PRIMARY KEY ([ID])
    );
END;