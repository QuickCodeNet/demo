SELECT
    COUNT(*)
FROM [dbo].[ORDER_ITEMS]
WHERE [IsDeleted] = 0;