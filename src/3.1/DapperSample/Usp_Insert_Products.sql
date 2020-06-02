USE Northwind
GO

IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'Usp_Insert_Products'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.Usp_Insert_Products
GO

CREATE PROCEDURE dbo.Usp_Insert_Products
    @ProductName nvarchar(40)
    , @Discontinued int
AS
BEGIN

    INSERT INTO [dbo].[Products]
        (
            [ProductName],[Discontinued]
        )
    VALUES
        (
            @ProductName, @Discontinued
        )
    RETURN 0
END
GO

-- Test
--EXECUTE dbo.Usp_Insert_Products "Test", 0
--GO