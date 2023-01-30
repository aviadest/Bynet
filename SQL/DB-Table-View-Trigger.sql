USE [master]
GO
CREATE DATABASE [Bynet]
GO

USE [Bynet]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[IdNumber] [varchar](9) NOT NULL ,
	[FirstName] [varchar](20) NOT NULL,
	[LastName] [varchar](20) NOT NULL,
	[Job] [varchar](20) NOT NULL,
	[ManagerId] [varchar](9) NULL,
 CONSTRAINT [PK__EmployeeId] PRIMARY KEY CLUSTERED 
( [IdNumber] ASC ))
GO

ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK__Employee__Manager] FOREIGN KEY([ManagerId])
REFERENCES [dbo].[Employees] ([IdNumber])
GO

ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK__Employee__Manager]
GO

USE [Bynet]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_Employees]
AS
SELECT 
e.IdNumber, 
e.FirstName, 
e.LastName, 
e.Role, 
e.ManagerId,
CONCAT(m.FirstName, ' ', m.LastName) AS ManagerName
FROM dbo.employees AS e 
LEFT OUTER JOIN dbo.employees AS m ON e.ManagerId = m.IdNumber
GO

CREATE TRIGGER Employees_OnDeleteSetNull
ON dbo.Employees
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.Employees
    SET ManagerId = NULL
    FROM dbo.Employees e
        INNER JOIN deleted d ON e.ManagerId = d.IdNumber;
    DELETE 
    FROM dbo.Employees
    FROM dbo.Employees cd
        INNER JOIN deleted d ON cd.IdNumber = d.IdNumber;
END
GO
