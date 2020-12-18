CREATE TABLE [dbo].[UsedCars_Vechicle_Setting_Details]
(
	[VechicleSettingID] INT NOT NULL PRIMARY KEY, 
    [SettingDetailID] INT NOT NULL, 
    [SettingValue] NVARCHAR(100) NOT NULL, 
    [IsRequired] BIT NOT NULL, 
    [IsOptional] BIT NOT NULL
)
