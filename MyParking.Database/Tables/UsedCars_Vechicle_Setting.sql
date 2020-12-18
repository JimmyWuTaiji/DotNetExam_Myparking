CREATE TABLE [dbo].[UsedCars_Vechicle_Setting]
(
	[VechicleSettingID] INT NOT NULL PRIMARY KEY, 
    [VehicleID] INT NOT NULL, 
    [SettingName] NVARCHAR(100) NOT NULL, 
    [ProductionYear] INT NOT NULL, 
    [Price] INT NOT NULL
)
