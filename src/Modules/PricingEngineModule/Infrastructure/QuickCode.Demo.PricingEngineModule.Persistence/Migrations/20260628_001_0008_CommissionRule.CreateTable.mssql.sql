IF OBJECT_ID(N'dbo.COMMISSION_RULES', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[COMMISSION_RULES] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [COMMISSION_MODEL_ID] int NOT NULL,
        [SCOPE] nvarchar(max) NOT NULL,
        [SCOPE_ID] int NOT NULL,
        [COMMISSION_TYPE] nvarchar(max) NOT NULL,
        [PERCENTAGE_RATE] decimal(18,8) NOT NULL,
        [FIXED_AMOUNT] decimal(18,2) NOT NULL,
        [IS_ACTIVE] bit NOT NULL DEFAULT 1,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_COMMISSION_RULES] PRIMARY KEY ([ID])
    );
END;