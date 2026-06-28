IF OBJECT_ID(N'dbo.SELLER_TIERS', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[SELLER_TIERS] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [NAME] nvarchar(250) NOT NULL,
        [MIN_SALES_VOLUME] decimal(18,2) NOT NULL,
        [BENEFITS_DESCRIPTION] nvarchar(1000) NOT NULL,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_SELLER_TIERS] PRIMARY KEY ([ID])
    );
END;