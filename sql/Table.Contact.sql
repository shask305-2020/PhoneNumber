CREATE TABLE [dbo].[Contact] (
    [ContactID]    INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50)  NULL,
    [MobileNumber] NVARCHAR (50)  NULL,
    [Address]      NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([ContactID] ASC)
);

