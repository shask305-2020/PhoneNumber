CREATE PROCEDURE [dbo].[ContactAddOrEdit]
	@mode nvarchar(10),
	@ContactID int,
	@Name nvarchar(50),
	@MobileNumber nvarchar(50),
	@Address nvarchar(MAX)

AS
	IF @mode='Add'
	BEGIN
	INSERT INTO Contact
	(
	Name,
	MobileNumber,
	Address
	)
	VALUES
	(
	@Name,
	@MobileNumber,
	@Address
	)
	END
