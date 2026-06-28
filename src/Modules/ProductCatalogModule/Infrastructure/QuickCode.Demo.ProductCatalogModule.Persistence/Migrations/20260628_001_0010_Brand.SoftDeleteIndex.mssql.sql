IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.BRANDS', N'U') AND name = N'IX_BRANDS_IsDeleted')
BEGIN
    CREATE INDEX [IX_BRANDS_IsDeleted] ON [dbo].[BRANDS] ([IsDeleted]);
END