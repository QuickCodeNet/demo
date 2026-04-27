UPDATE [dbo].[SHIPMENTS]
SET [IsDeleted] = 1, [DeletedOnUtc] = SYSUTCDATETIME()
WHERE
    [ID] = @PRM_SHIPMENT_ID
    AND [IsDeleted] = 0;