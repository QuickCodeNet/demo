IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.PRODUCTS', N'U') AND name = N'IX_PRODUCTS_IsDeleted')
BEGIN
    CREATE INDEX [IX_PRODUCTS_IsDeleted] ON [dbo].[PRODUCTS] ([IsDeleted]);
END