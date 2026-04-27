IF OBJECT_ID(N'dbo.SELLER_DOCUMENTS', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[SELLER_DOCUMENTS] (
        [ID] int IDENTITY(1,1) NOT NULL,
        [SELLER_ID] int NOT NULL,
        [DOCUMENT_TYPE] nvarchar(max) NOT NULL,
        [FILE_URL] nvarchar(1000) NOT NULL,
        [STATUS] nvarchar(max) NOT NULL DEFAULT 'PENDING',
        [UPLOADED_DATE] datetime2(7) NOT NULL,
        [REVIEWED_DATE] datetime2(7) NOT NULL,
        [REJECTION_REASON] nvarchar(1000) NOT NULL,
        [IsDeleted] bit NOT NULL DEFAULT 0,
        [DeletedOnUtc] datetime2(7) NULL,
        CONSTRAINT [PK_SELLER_DOCUMENTS] PRIMARY KEY ([ID])
    );
END;