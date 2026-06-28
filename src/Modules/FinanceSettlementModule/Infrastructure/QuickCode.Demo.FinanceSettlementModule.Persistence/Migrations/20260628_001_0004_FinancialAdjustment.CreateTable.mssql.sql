IF OBJECT_ID(N'dbo.FINANCIAL_ADJUSTMENTS', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[FINANCIAL_ADJUSTMENTS] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [SELLER_ID] int NOT NULL,
        [AMOUNT] decimal(18,2) NOT NULL,
        [REASON] nvarchar(max) NOT NULL,
        [NOTES] nvarchar(1000) NOT NULL,
        [CREATED_BY_USER_ID] int NOT NULL,
        [CREATED_DATE] datetime2(7) NOT NULL,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_FINANCIAL_ADJUSTMENTS] PRIMARY KEY ([ID])
    );
END;