IF OBJECT_ID(N'dbo.SELLER_BANK_ACCOUNTS', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[SELLER_BANK_ACCOUNTS] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [SELLER_ID] int NOT NULL,
        [BANK_NAME] nvarchar(250) NOT NULL,
        [ACCOUNT_HOLDER_NAME] nvarchar(250) NOT NULL,
        [IBAN] nvarchar(50) NOT NULL,
        [IS_DEFAULT] bit NOT NULL DEFAULT 0,
        [IS_VERIFIED] bit NOT NULL DEFAULT 0,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_SELLER_BANK_ACCOUNTS] PRIMARY KEY ([ID])
    );
END;