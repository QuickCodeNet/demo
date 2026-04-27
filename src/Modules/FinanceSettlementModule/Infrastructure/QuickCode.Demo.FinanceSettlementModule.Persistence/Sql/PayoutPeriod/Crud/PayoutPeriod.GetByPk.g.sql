SELECT
    [ID],
    [START_DATE],
    [END_DATE],
    [IS_CLOSED]
FROM [dbo].[PAYOUT_PERIODS]
WHERE
    [ID] = @PRM_PAYOUT_PERIOD_ID
    AND [IsDeleted] = 0;