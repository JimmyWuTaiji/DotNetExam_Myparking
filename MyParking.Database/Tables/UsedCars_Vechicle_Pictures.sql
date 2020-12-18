CREATE TABLE [dbo].[UsedCars_Vechicle_Pictures]
(
	[VechicleSettingID] INT NOT NULL , 
    [SeqNo] INT NOT NULL, 
    [PictureURL] NVARCHAR(500) NULL, 
    PRIMARY KEY ([VechicleSettingID], [SeqNo])
)
