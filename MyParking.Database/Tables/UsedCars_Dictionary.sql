CREATE TABLE [dbo].[UsedCars_Dictionary]
(
	[DicID] INT NOT NULL PRIMARY KEY, 
    [DicName_Key] VARCHAR(100) NOT NULL, 
    [DicName_Default] NVARCHAR(200) NOT NULL, 
    [ParentID] INT NOT NULL, 
    [IsAcitve] BIT NOT NULL, 
    [SeqNo] INT NOT NULL
)
