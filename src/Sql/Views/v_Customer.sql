CREATE VIEW [dbo].[v_Customer] AS 
SELECT
    [Id],
    [Name],
    JSON_QUERY((
	    SELECT
			[Address].[Id],
		    [Address].[Line],
		    [Address].[Suburb],
		    [Address].[City],
		    [Address].[Province],
		    [Address].[Code]
        FROM
            [Address]
        WHERE
            [Address].[CustomerId] = [Customer].[Id]
	    FOR JSON PATH
    )) AS [Addresses]
FROM
    [Customer]