IF OBJECT_ID(N'dbo.RETURN_REQUESTS', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[RETURN_REQUESTS] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [ORDER_ID] int NOT NULL,
        [ORDER_ITEM_ID] int NOT NULL,
        [REASON] nvarchar(1000) NOT NULL,
        [STATUS] nvarchar(max) NOT NULL DEFAULT 'REQUESTED',
        [REQUESTED_DATE] datetime2(7) NOT NULL,
        [UPDATED_DATE] datetime2(7) NOT NULL,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_RETURN_REQUESTS] PRIMARY KEY ([ID])
    );
END;