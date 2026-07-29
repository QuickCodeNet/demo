IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.SELLERS', N'U') AND name = N'IX_SELLERS_IsDeleted')
BEGIN
    CREATE INDEX [IX_SELLERS_IsDeleted] ON [dbo].[SELLERS] ([IsDeleted]);
END