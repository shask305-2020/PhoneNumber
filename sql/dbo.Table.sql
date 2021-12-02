CREATE TABLE [dbo].[Contact]
(
	[ContactID] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NULL, 
    [MobileNumber] NVARCHAR(50) NULL, 
    [Address] NVARCHAR(MAX) NULL
)
