IF OBJECT_ID(N'dbo.ORDERS', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[ORDERS] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [CUSTOMER_ID] int NOT NULL,
        [SELLER_ID] int NOT NULL,
        [ORDER_NUMBER] nvarchar(50) NOT NULL,
        [TOTAL_AMOUNT] decimal(18,2) NOT NULL,
        [SHIPPING_ADDRESS] nvarchar(1000) NOT NULL,
        [STATUS] nvarchar(max) NOT NULL DEFAULT 'PENDING_PAYMENT',
        [ORDER_DATE] datetime2(7) NOT NULL,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_ORDERS] PRIMARY KEY ([ID])
    );
END;