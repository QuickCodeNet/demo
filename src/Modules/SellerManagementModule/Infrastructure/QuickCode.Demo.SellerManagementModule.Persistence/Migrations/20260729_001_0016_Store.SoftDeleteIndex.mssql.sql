IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.STORES', N'U') AND name = N'IX_STORES_IsDeleted')
BEGIN
    CREATE INDEX [IX_STORES_IsDeleted] ON [dbo].[STORES] ([IsDeleted]);
END