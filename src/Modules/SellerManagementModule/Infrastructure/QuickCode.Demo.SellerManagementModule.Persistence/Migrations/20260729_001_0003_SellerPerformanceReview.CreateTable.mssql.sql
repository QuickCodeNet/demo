IF OBJECT_ID(N'dbo.SELLER_PERFORMANCE_REVIEWS', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[SELLER_PERFORMANCE_REVIEWS] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [SELLER_ID] int NOT NULL,
        [REVIEWER_ID] int NOT NULL,
        [RATING] tinyint NOT NULL,
        [COMMENT] nvarchar(1000) NOT NULL,
        [REVIEW_DATE] datetime2(7) NOT NULL,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_SELLER_PERFORMANCE_REVIEWS] PRIMARY KEY ([ID])
    );
END;