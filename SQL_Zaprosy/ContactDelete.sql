CREATE PROCEDURE [dbo].[ContactDelete]
	@ContactID int
	AS
		DELETE Contact
		WHERE ContactID=@ContactID