CREATE PROCEDURE [dbo].[ContactViewOrSearch]
	@ContactName nvarchar(50)
	AS
	SELECT *
	FROM Contact
	WHERE name LIKE @ContactName+'%'