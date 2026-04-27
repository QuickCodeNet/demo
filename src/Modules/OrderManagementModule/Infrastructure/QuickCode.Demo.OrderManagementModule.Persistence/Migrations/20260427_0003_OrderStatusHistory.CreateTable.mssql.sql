IF OBJECT_ID(N'dbo.ORDER_STATUS_HISTORIES', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[ORDER_STATUS_HISTORIES] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [ORDER_ID] int NOT NULL,
        [PREVIOUS_STATUS] nvarchar(max) NOT NULL,
        [NEW_STATUS] nvarchar(max) NOT NULL,
        [CHANGED_BY_USER_ID] int NOT NULL,
        [CHANGE_DATE] datetime2(7) NOT NULL,
        [NOTES] nvarchar(1000) NOT NULL,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_ORDER_STATUS_HISTORIES] PRIMARY KEY ([ID])
    );
END;