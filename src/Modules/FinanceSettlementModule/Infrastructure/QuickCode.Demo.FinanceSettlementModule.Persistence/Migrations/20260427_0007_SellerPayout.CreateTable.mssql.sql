IF OBJECT_ID(N'dbo.SELLER_PAYOUTS', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[SELLER_PAYOUTS] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [SELLER_ID] int NOT NULL,
        [PAYOUT_PERIOD_ID] int NOT NULL,
        [TOTAL_EARNINGS] decimal(18,2) NOT NULL,
        [TOTAL_COMMISSION] decimal(18,2) NOT NULL,
        [NET_PAYOUT_AMOUNT] decimal(18,2) NOT NULL,
        [STATUS] nvarchar(max) NOT NULL DEFAULT 'PENDING',
        [REQUESTED_DATE] datetime2(7) NOT NULL,
        [PROCESSED_DATE] datetime2(7) NOT NULL,
        [TRANSACTION_ID] nvarchar(50) NOT NULL,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_SELLER_PAYOUTS] PRIMARY KEY ([ID])
    );
END;