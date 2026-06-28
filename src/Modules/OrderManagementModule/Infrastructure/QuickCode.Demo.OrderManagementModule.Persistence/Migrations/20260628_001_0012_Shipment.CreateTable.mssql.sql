IF OBJECT_ID(N'dbo.SHIPMENTS', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[SHIPMENTS] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [ORDER_ID] int NOT NULL,
        [SHIPPING_METHOD_ID] int NOT NULL,
        [TRACKING_NUMBER] nvarchar(50) NOT NULL,
        [STATUS] nvarchar(max) NOT NULL DEFAULT 'PENDING',
        [SHIPPED_DATE] datetime2(7) NOT NULL,
        [DELIVERED_DATE] datetime2(7) NOT NULL,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_SHIPMENTS] PRIMARY KEY ([ID])
    );
END;