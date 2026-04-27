SELECT
    COUNT(*)
FROM [dbo].[PRODUCT_VARIANT_ATTRIBUTES]
WHERE EXISTS (
    SELECT 1
    FROM [dbo].[PRODUCT_VARIANTS] qc_sd_p
    WHERE qc_sd_p.[ID] = [dbo].[PRODUCT_VARIANT_ATTRIBUTES].[VARIANT_ID]
      AND qc_sd_p.[IsDeleted] = 0)
    AND EXISTS (
    SELECT 1
    FROM [dbo].[ATTRIBUTE_VALUES] qc_sd_p
    WHERE qc_sd_p.[ID] = [dbo].[PRODUCT_VARIANT_ATTRIBUTES].[ATTRIBUTE_VALUE_ID]
      AND qc_sd_p.[IsDeleted] = 0);