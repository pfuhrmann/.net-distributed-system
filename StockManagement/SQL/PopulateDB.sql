USE comp1690;

-- http://stackoverflow.com/questions/155246/how-do-you-truncate-all-tables-in-a-database-using-tsql
-- disable all constraints
EXEC sp_msforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all"
-- delete data in all tables
EXEC sp_MSForEachTable "DELETE FROM ?"
-- enable all constraints
EXEC sp_msforeachtable "ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all"

INSERT INTO [dbo].[Products]
 ([Name]
 ,[Price]
 ,[Weight]
 ,[BoxItemsAmount]
 ,[QuantityTotal])
VALUES 
	('Product Name 1' ,18.50, 300, 20, 0),
	('Product Name 2' ,25.50, 200, 15, 0)
GO