SELECT
    [ID],
    [SELLER_ID],
    [BRAND_ID],
    [PRIMARY_CATEGORY_ID],
    [SKU],
    [NAME],
    [DESCRIPTION],
    [STATUS],
    [IS_FEATURED],
    [CREATED_DATE]
FROM [dbo].[PRODUCTS]
WHERE
    [ID] = @PRM_PRODUCT_ID
    AND [IsDeleted] = 0;