IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.ORDERS', N'U') AND name = N'IX_ORDERS_IsDeleted')
BEGIN
    CREATE INDEX [IX_ORDERS_IsDeleted] ON [dbo].[ORDERS] ([IsDeleted]);
END