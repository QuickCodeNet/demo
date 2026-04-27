IF OBJECT_ID(N'dbo.PAYOUT_LINE_ITEMS', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[PAYOUT_LINE_ITEMS] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [PAYOUT_ID] int NOT NULL,
        [ORDER_ID] int NOT NULL,
        [ITEM_AMOUNT] decimal(18,2) NOT NULL,
        [COMMISSION_AMOUNT] decimal(18,2) NOT NULL,
        [NET_AMOUNT] decimal(18,2) NOT NULL,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_PAYOUT_LINE_ITEMS] PRIMARY KEY ([ID])
    );
END;