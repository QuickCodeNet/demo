IF OBJECT_ID(N'dbo.TRANSACTION_LEDGERS', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[TRANSACTION_LEDGERS] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [SELLER_ID] int NOT NULL,
        [TRANSACTION_TYPE] nvarchar(max) NOT NULL,
        [AMOUNT] decimal(18,2) NOT NULL,
        [REFERENCE_ID] nvarchar(50) NOT NULL,
        [DESCRIPTION] nvarchar(1000) NOT NULL,
        [TRANSACTION_DATE] datetime2(7) NOT NULL,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_TRANSACTION_LEDGERS] PRIMARY KEY ([ID])
    );
END;